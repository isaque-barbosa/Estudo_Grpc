#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Trabalho.Grpc.Cliente/Trabalho.Grpc.Cliente.csproj", "Trabalho.Grpc.Cliente/"]
RUN dotnet restore "Trabalho.Grpc.Cliente/Trabalho.Grpc.Cliente.csproj"
COPY . .
WORKDIR "/src/Trabalho.Grpc.Cliente"
RUN dotnet build "Trabalho.Grpc.Cliente.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trabalho.Grpc.Cliente.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trabalho.Grpc.Cliente.dll"]