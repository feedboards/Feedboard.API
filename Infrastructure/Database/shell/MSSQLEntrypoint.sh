#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start up
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -Q "SELECT 1"; do
    echo "SQL Server not ready yet..."
    sleep 5
done

echo "SQL Server is ready. Starting the restore process."

# Run the CreateMSSQLDatabase.sql script
if [ -f "/usr/src/app/CreateMSSQLDatabase.sql" ]; then
    echo "Creating the database Feedboard using /usr/src/app/CreateMSSQLDatabase.sql..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -i /usr/src/app/CreateMSSQLDatabase.sql
    if [ $? -ne 0 ]; then
        echo "Failed to run CreateMSSQLDatabase.sql"
        exit 1
    fi
else
    echo "/usr/src/app/CreateMSSQLDatabase.sql not found!"
    exit 1
fi

# Wait for the Feedboard database to be fully created
echo "Waiting for the Feedboard database to be fully created..."
sleep 10  # Adding an extra delay to ensure the database is created

# Wait for the Feedboard database to be fully ready
echo "Checking if the Feedboard database is ready..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d Feedboard -Q "SELECT 1"; do
    echo "Feedboard database not ready yet..."
    sleep 5
done

# Proceed to run other SQL scripts
for script in /usr/src/app/*.sql; do
    echo "Proceed to run other SQL scripts: $script"

    if [ "$script" != "/usr/src/app/CreateMSSQLDatabase.sql" ]; then
        echo "Running script $script..."
        /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d Feedboard -i "$script"
        if [ $? -ne 0 ]; then
            echo "Failed to run $script"
            exit 1
        fi
    else
        sleep 10
    fi
done

echo "Database restored successfully."

# Keep the container running
tail -f /dev/null
