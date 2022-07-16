# Article-Api Challange
### Requisites
* [Dotnet Core SDK 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)
* [Docker](https://docs.docker.com/get-docker/)

### Using containerized database

1. Run `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Admin1234" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest`
    * If there is some change on the previous command update the connectionString on [appsettings.json](../master/article-api.WebApi/appsettings.json)

### Running the application

1. Open Visual Studio
2. Open Package Manager Console
3. Set **Default project: article-api.DataAccess**
4. Run `PM> Update-Database`
5. Run the application

### Notes

I took some assumptions that maybe are wrong, for example
- The delete operation is a **hard** delete, not **logical**
- I added the service layer, to decouple the domain model, with the dto.
- Also changed the IRepository to IArticlesRepository because the methods where tied to the Article, so the name was misleading

Also there are some considerations unfinished
- The Integration Tests need a revision on the use of the inMemory Database, lacks use of a setup and dispose method for each test
- Maybe I would take an approach of Vertical Slice, but I have more experience on Clean Architecture layered, so I choose the last
- It will be good, if I could add some example to the swagger documentation
- I usually wrap projects on folders, for example, "1. WebApi" "2. BusinessLogic"...etc, so when you code, can see in order the flow of the application
