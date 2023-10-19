FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["WebAPI/ComputerStore.Services.CS.Api.csproj", "WebAPI/"]
COPY ["BusinessLogic/ComputerStore.Services.CS.BusinessLogic.csproj", "BusinessLogic/"]
COPY ["DataAccess/ComputerStore.Services.CS.DataAccess.csproj", "DataAccess/"]
COPY ["Shared/ComputerStore.Services.CS.Shared.csproj", "Shared/"]
RUN dotnet restore "WebAPI/ComputerStore.Services.CS.Api.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ComputerStore.Services.CS.Api.dll"]