# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos (inclui todos os projetos)
COPY . .

# Vai para a pasta da API (onde está o .csproj principal)
WORKDIR /src/DesafioMirante.API

# Restaura dependências
RUN dotnet restore

# Publica o projeto
RUN dotnet publish -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DesafioMirante.API.dll"]
