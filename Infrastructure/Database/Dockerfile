FROM mcr.microsoft.com/mssql/server:2022-latest

WORKDIR /usr/src/app

COPY Feedboard.Database/Tables/*.sql ./
COPY Infrastructure/Database/sql/*.sql ./

COPY --chmod=755 Infrastructure/Database/shell/MSSQLEntrypoint.sh /usr/src/app/MSSQLEntrypoint.sh

EXPOSE 1433

ENTRYPOINT ["/bin/bash", "/usr/src/app/MSSQLEntrypoint.sh"]