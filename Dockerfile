FROM microsoft/aspnetcore-build:latest AS build-env
WORKDIR /src

# Copy everything
COPY . ./

# Restore and publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:latest
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]