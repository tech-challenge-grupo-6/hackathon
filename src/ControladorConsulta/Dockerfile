FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/ControladorConsulta/ControladorConsulta.csproj", "src/ControladorConsulta/"]
RUN dotnet restore "src/ControladorConsulta/ControladorConsulta.csproj"
COPY . .
WORKDIR "/src/src/ControladorConsulta"
RUN dotnet build "ControladorConsulta.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ControladorConsulta.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControladorConsulta.dll"]
