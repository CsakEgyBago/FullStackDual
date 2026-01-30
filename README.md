# FullStackDual
dotnet tool uninstall --global dotnet-ef

dotnet tool install --global dotnet-ef --version 8.*

DELETE FROM Users WHERE Id > 1

dotnet ef migrations add Initial --project Eszi.Demo.Database --startup-project Eszi.Demo.Database --context CoreDbContext

--project Eszi.Demo.Database
--startup-project Dual.Server
--context CoreDbContext
