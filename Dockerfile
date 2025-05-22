# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Streamphony.sln ./
COPY Streamphony.WebAPI/*.csproj ./Streamphony.WebAPI/
COPY Streamphony.Infrastructure/*.csproj ./Streamphony.Infrastructure/
COPY Streamphony.Application/*.csproj ./Streamphony.Application/
COPY Streamphony.Domain/*.csproj ./Streamphony.Domain/

RUN dotnet restore

COPY . ./
WORKDIR /app/Streamphony.WebAPI
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Streamphony.WebAPI.dll"]
