# ClothingShop
[![Build and publish releases](https://github.com/kudima03/ClothingShop/actions/workflows/publish-release.yml/badge.svg)](https://github.com/kudima03/ClothingShop/actions/workflows/publish-release.yml)

https://github.com/kudima03/ClothingShop/assets/93078951/7d44e2a7-2a5f-4751-84d4-a775914e295e

## Running project
#### Running the project locally
All of the project's functionality works with just the web application running. However, project requires connections to Postgres(optionally from v1.0.14) and Redis.
By default in the development mode project uses in-memory database, if you want to use real Postgres database you should override parameter in appsettings.Development.json

   ```json
   "UseInMemoryDatabase":  true,
   ```
to false.
Then you should override your authorization secrets and connection strings by your own values:
  ```json
  "ConnectionStrings": {
    "ShopConnection":
      "Host=<YOUR-HOST>;Port=<YOUR-PORT>;Database=<YOUR-DATABASE-NAME>;Username=<YOUR-USERNAME>;Password=<YOUR-PASSWORD>;",
    "IdentityConnection":
      "Host=<YOUR-HOST>;Port=<YOUR-PORT>;Database=<YOUR-DATABASE-NAME>;Username=<YOUR-USERNAME>;Password=<YOUR-PASSWORD>;",
    "RedisConnection": "redis://localhost:6379" //Default redis docker port
  },
  "JwtSettings": {
    "Issuer": "<YOUR-VALUE>",
    "Audience": "<YOUR-VALUE>",
    "SecretKey": "<YOUR-VALUE>",
    "TokenLifetimeMinutes": 2880 //48 hours
  },
  ```
Then you can access `localhost:7068/account/login`.

#### Docker Compose
You can simply run docker-compose in project which will automatically download, configure and start all dependencies with command
   ```json
   docker-compose -f "docker-compose.yml" up -d
   ```
Then you can access `localhost:5001/account/login`.