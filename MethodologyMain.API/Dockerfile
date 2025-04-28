FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["MethodologyMain.API/MethodologyMain.API.csproj", "MethodologyMain.API/"]
COPY ["MethodologyMain.Application/MethodologyMain.Application.csproj","MethodologyMain.Application/"]
COPY ["MethodologyMain.Infrastructure/MethodologyMain.Infrastructure.csproj","MethodologyMain.Infrastructure/"]
COPY ["MethodologyMain.Logic/MethodologyMain.Logic.csproj","MethodologyMain.Logic/"]
COPY ["MethodologyMain.Persistence/MethodologyMain.Persistence.csproj","MethodologyMain.Persistence/"]

RUN dotnet restore "MethodologyMain.API/MethodologyMain.API.csproj"


COPY . .

WORKDIR "/src/MethodologyMain.API"
RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MethodologyMain.API.dll"]