FROM microsoft/windowsservercore AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Topic.Recieve/QuickBook.Topic.Recieve.csproj Topic.Recieve/
RUN dotnet restore Topic.Recieve/QuickBook.Topic.Recieve.csproj
COPY . .
WORKDIR /src/Topic.Recieve
RUN dotnet build QuickBook.Topic.Recieve.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish QuickBook.Topic.Recieve.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QuickBook.Topic.Recieve.dll"]
