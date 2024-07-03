# Utiliser l'image SDK pour construire l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers csproj et restaurer les dépendances
COPY Ressources.Back.Api/*.csproj Ressources.Back.Api/
COPY Ressources.Back.Data/*.csproj Ressources.Back.Data/
RUN dotnet restore Ressources.Back.Api/*.csproj

# Copier le reste des fichiers et construire l'application
COPY Ressources.Back.Api/. Ressources.Back.Api/
COPY Ressources.Back.Data/. Ressources.Back.Data/
WORKDIR /app/Ressources.Back.Api
RUN dotnet publish -c Release -o out

# Utiliser l'image runtime pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Ressources.Back.Api/out .

# Exposer le port sur lequel l'application écoute
EXPOSE 24000

# Commande pour démarrer l'application
ENTRYPOINT ["dotnet", "Ressources.Back.Api.dll"]
