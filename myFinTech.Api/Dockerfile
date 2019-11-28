FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["myFinTech.Api/myFinTech.Api.csproj", "myFinTech.Api/"]
RUN dotnet restore "myFinTech.Api/myFinTech.Api.csproj"
COPY . .
WORKDIR "/src/myFinTech.Api"
RUN dotnet build "myFinTech.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "myFinTech.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "myFinTech.Api.dll"]