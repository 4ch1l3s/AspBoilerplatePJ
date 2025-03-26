FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/internPJ3.Web.Mvc/internPJ3.Web.Mvc.csproj", "src/internPJ3.Web.Mvc/"]
COPY ["src/internPJ3.Web.Core/internPJ3.Web.Core.csproj", "src/internPJ3.Web.Core/"]
COPY ["src/internPJ3.Application/internPJ3.Application.csproj", "src/internPJ3.Application/"]
COPY ["src/internPJ3.Core/internPJ3.Core.csproj", "src/internPJ3.Core/"]
COPY ["src/internPJ3.EntityFrameworkCore/internPJ3.EntityFrameworkCore.csproj", "src/internPJ3.EntityFrameworkCore/"]
WORKDIR "/src/src/internPJ3.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/internPJ3.Web.Mvc", "src/internPJ3.Web.Mvc"]
COPY ["src/internPJ3.Web.Core", "src/internPJ3.Web.Core"]
COPY ["src/internPJ3.Application", "src/internPJ3.Application"]
COPY ["src/internPJ3.Core", "src/internPJ3.Core"]
COPY ["src/internPJ3.EntityFrameworkCore", "src/internPJ3.EntityFrameworkCore"]
WORKDIR "/src/src/internPJ3.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "internPJ3.Web.Mvc.dll"]
