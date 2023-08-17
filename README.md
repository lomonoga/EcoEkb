# EcoEkb

## Hackathon project "Код города 300"

* Для создания базы данных PostgreSQL с помощью докер контейнера выполните это:

```
docker-compose build
```
```
docker-compose up
```

* После проведите миграции:
```
dotnet ef migrations add Init --context EcoEkbDbContext --project EcoEkb.Backend.DataAccess --startup-project EcoEkb.Backend.Api
```
* И обновите базу данных:
```
dotnet ef database update --context EcoNotificationsDbContext --project EcoEkb.Backend.DataAccess --startup-project EcoEkb.Backend.Api
```

База данных готова к использованию! 
