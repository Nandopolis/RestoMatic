### Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy Projects
COPY WebApi/*.csproj WebApi/
COPY src/Inventory/Application/*.csproj src/Inventory/Application/
COPY src/Inventory/Domain/*.csproj src/Inventory/Domain/
COPY src/Inventory/Infrastructure/*.csproj src/Inventory/Infrastructure/

# .NET Core Restore
RUN dotnet restore WebApi/WebApi.csproj

# Copy All Files
COPY WebApi/ ./WebApi/
COPY src ./src/

# .NET Core Build
WORKDIR /app/WebApi
RUN dotnet build -c Release --no-restore


### Publish Stage
FROM build AS publish
RUN dotnet publish -c Release --no-build -o /publish


# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=publish /publish ./
ENTRYPOINT ["dotnet", "WebApi.dll"]
