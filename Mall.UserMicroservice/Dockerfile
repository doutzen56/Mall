#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Mall.UserMicroservice/Mall.UserMicroservice.csproj", "Mall.UserMicroservice/"]
COPY ["Mall.Common/Mall.Common.csproj", "Mall.Common/"]
COPY ["Mall.WebCore/Mall.WebCore.csproj", "Mall.WebCore/"]
COPY ["Mall.Service/Mall.Service.csproj", "Mall.Service/"]
COPY ["Mall.Core/Mall.Core.csproj", "Mall.Core/"]
COPY ["Mall.Interface/Mall.Interface.csproj", "Mall.Interface/"]
COPY ["Mall.Model/Mall.Model.csproj", "Mall.Model/"]
RUN dotnet restore "Mall.UserMicroservice/Mall.UserMicroservice.csproj"
COPY . .
WORKDIR "/src/Mall.UserMicroservice"
RUN dotnet build "Mall.UserMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mall.UserMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mall.UserMicroservice.dll"]