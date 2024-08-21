# feedboard.API

# Environment Variables

```env
GITHUB_CLIENT_ID=YOUR_CLIENT_ID
GITHUB_CLIENT_SECRET==YOUR_CLIENT_SECRET

AZURE_CLIENT_ID=YOUR_CLIENT_ID
AZURE_TENANT_ID=YOUR_TENANT_ID
AZURE_CLIENT_SECRET=YOUR_CLIENT_SECRET_VALUE

DATABASE_HOST=YOUR_HOST
DATABASE_PORT=YOUR_PORT (optional)
DATABASE_USERNANE=YOUR_USERNAME
DATABASE_PASSWORD=YOUR_PASSWORD
DATABASE_DATABASE_NAME=YOUR_DATABASE_NAME
```

# Docker Compose

```yaml
version: "3.8"

services:
  feedboard-api:
    image: ghcr.io/feedboards/feedboard-api:latest
    container_name: feedboard-api
    environment:
      - GITHUB_CLIENT_ID=YOUR_CLIENT_ID
      - GITHUB_CLIENT_SECRET==YOUR_CLIENT_SECRET

      - AZURE_CLIENT_ID=YOUR_CLIENT_ID
      - AZURE_TENANT_ID=YOUR_TENANT_ID
      - AZURE_CLIENT_SECRET=YOUR_CLIENT_SECRET_VALUE

      - DATABASE_HOST=YOUR_HOST
      - DATABASE_PORT=YOUR_PORT (optional)
      - DATABASE_USERNANE=YOUR_USERNAME
      - DATABASE_PASSWORD=YOUR_PASSWORD
      - DATABASE_DATABASE_NAME=YOUR_DATABASE_NAME
    ports:
      - "80:80"
      - "443:443"
    networks:
      - feedboard-network
    depends_on:
      - feedboard-database

  feedboard-database:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      ACCEPT_EULA: "Y"
      SA_PASSWORD: "YOUR_PASSWORD"
    ports:
      - "1433:1433"
    volumes:
      - database_data:/var/opt/mssql
    networks:
      - feedboard-network

networks:
  feedboard-network:

volumes:
  database_data:
    driver: local
```
