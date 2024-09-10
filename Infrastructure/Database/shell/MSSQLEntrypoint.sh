#!/bin/bash

$name = "Feedboard"

/opt/mssql/bin/sqlservr &

echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -Q "SELECT 1"; do
    echo "SQL Server not ready yet..."
    sleep 5
done

echo "SQL Server is ready. Starting the restore process."

if [ -f "/usr/src/app/CreateMSSQLDatabase.sql" ]; then
    echo "Creating the database $name using /usr/src/app/CreateMSSQLDatabase.sql..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -i /usr/src/app/CreateMSSQLDatabase.sql
    if [ $? -ne 0 ]; then
        echo "Failed to run CreateMSSQLDatabase.sql"
        exit 1
    fi
else
    echo "/usr/src/app/CreateMSSQLDatabase.sql not found!"
    exit 1
fi

echo "Waiting for the $name database to be fully created..."
sleep 10

echo "Checking if the $name database is ready..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d $name -Q "SELECT 1"; do
    echo "$name database not ready yet..."
    sleep 5
done

for script in /usr/src/app/*.sql; do
    echo "Proceed to run other SQL scripts: $script"

    if [ "$script" != "/usr/src/app/CreateMSSQLDatabase.sql" ]; then
        echo "Running script $script..."
        /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d $name -i "$script"
        if [ $? -ne 0 ]; then
            echo "Failed to run $script"
            exit 1
        fi
    else
        sleep 10
    fi
done

echo "Database restored successfully."

tail -f /dev/null
