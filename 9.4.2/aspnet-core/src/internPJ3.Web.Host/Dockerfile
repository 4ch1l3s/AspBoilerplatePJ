FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/internPJ3.Web.Host/internPJ3.Web.Host.csproj", "src/internPJ3.Web.Host/"]
COPY ["src/internPJ3.Web.Core/internPJ3.Web.Core.csproj", "src/internPJ3.Web.Core/"]
COPY ["src/internPJ3.Application/internPJ3.Application.csproj", "src/internPJ3.Application/"]
COPY ["src/internPJ3.Core/internPJ3.Core.csproj", "src/internPJ3.Core/"]
COPY ["src/internPJ3.EntityFrameworkCore/internPJ3.EntityFrameworkCore.csproj", "src/internPJ3.EntityFrameworkCore/"]
WORKDIR "/src/src/internPJ3.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/internPJ3.Web.Host", "src/internPJ3.Web.Host"]
COPY ["src/internPJ3.Web.Core", "src/internPJ3.Web.Core"]
COPY ["src/internPJ3.Application", "src/internPJ3.Application"]
COPY ["src/internPJ3.Core", "src/internPJ3.Core"]
COPY ["src/internPJ3.EntityFrameworkCore", "src/internPJ3.EntityFrameworkCore"]
WORKDIR "/src/src/internPJ3.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "internPJ3.Web.Host.dll"]
