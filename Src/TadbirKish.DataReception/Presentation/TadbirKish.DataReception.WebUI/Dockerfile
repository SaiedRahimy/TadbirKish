# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

# Use the official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore project dependencies
COPY ["TadbirKish.DataReception.WebUI.csproj", "TadbirKish.DataReception.WebUI/"]
RUN dotnet restore "TadbirKish.DataReception.WebUI.csproj"

# Copy everything and build
COPY . .
WORKDIR "/Src/TadbirKish.DataReception/TadbirKish.DataReception"
RUN dotnet publish -c Release -o /app/publish

# Final stage: Run the app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TadbirKish.DataReception.WebUI.dll"]
