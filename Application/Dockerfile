FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore Domain/Domain.csproj
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore Data/Data.csproj
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore Services/Services.csproj
COPY ["PtcChallenge/PtcChallenge.csproj", "PtcChallenge/"]
RUN dotnet restore "PtcChallenge/PtcChallenge.csproj"
COPY . .
WORKDIR "/src/PtcChallenge"
RUN dotnet build "PtcChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PtcChallenge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PtcChallenge.dll"]