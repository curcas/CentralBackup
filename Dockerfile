FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./src/CentralBackup.Core/CentralBackup.Core.csproj ./CentralBackup.Core/
RUN dotnet restore ./CentralBackup.Core/

COPY ./src/CentralBackup.Commands/CentralBackup.Commands.csproj ./CentralBackup.Commands/
RUN dotnet restore ./CentralBackup.Commands/

COPY ./src/CentralBackup.Migrations/CentralBackup.Migrations.csproj ./CentralBackup.Migrations/
RUN dotnet restore ./CentralBackup.Migrations/

COPY ./src/CentralBackup.Web/CentralBackup.Web.csproj ./CentralBackup.Web/
RUN dotnet restore ./CentralBackup.Web/

# Copy everything else and build
COPY ./src/CentralBackup.Core/ ./CentralBackup.Core/
COPY ./src/CentralBackup.Commands/ ./CentralBackup.Commands/
COPY ./src/CentralBackup.Migrations/ ./CentralBackup.Migrations/
COPY ./src/CentralBackup.Web/ ./CentralBackup.Web/

RUN dotnet publish ./CentralBackup.Web/ -c Release -o ../out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CentralBackup.Web.dll"]

ENV "ConnectionStrings:Default" = "Server=localhost;Database=CentralBackup;User Id=central-backup;Password=Passw0rd!"
EXPOSE 80