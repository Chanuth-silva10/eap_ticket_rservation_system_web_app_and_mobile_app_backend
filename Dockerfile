FROM mcr.microsoft.com/dotnet/aspnet:7.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["eap_ticket_rservation_system_web_app_and_mobile_app_backend.csproj", "./"]
RUN dotnet restore "eap_ticket_rservation_system_web_app_and_mobile_app_backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "eap_ticket_rservation_system_web_app_and_mobile_app_backend.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "eap_ticket_rservation_system_web_app_and_mobile_app_backend.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eap_ticket_rservation_system_web_app_and_mobile_app_backend.dll"]
