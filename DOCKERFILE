# Usar a imagem do SDK para a versão mais recente que suporte .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar o .csproj
COPY [GitHubReposAPI.csproj", "GitHubReposAPI/"]
RUN dotnet restore "GitHubReposAPI.csproj"

# Copiar os arquivos restantes
COPY . .
WORKDIR "/src"
RUN dotnet build "GitHubReposAPI.csproj" -c Release -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish "GitHubReposAPI.csproj" -c Release -o /app/publish

# Usar a imagem base para execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GitHubReposAPI.dll"]
