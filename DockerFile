# =========================
# Base runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solução e projetos
COPY Translator-Ai.sln .
COPY Translator-Ai/ Translator-Ai/
COPY Translator-Ai.Infraestructure/ Translator-Ai.Infraestructure/

# Restaura pacotes
RUN dotnet restore "Translator-Ai.sln"

# Build do projeto principal
WORKDIR "/src/Translator-Ai"
RUN dotnet build "Translator-Ai.csproj" -c Release -o /app/build

# =========================
# Publish stage
# =========================
FROM build AS publish
WORKDIR "/src/Translator-Ai"
RUN dotnet publish "Translator-Ai.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# Final stage
# =========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Translator-Ai.dll"]
