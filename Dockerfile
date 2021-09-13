# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Sample.Core/*.csproj src/Sample.Core/
COPY src/Sample.Data/*.csproj src/Sample.Data/
COPY src/Sample.Services/*.csproj src/Sample.Services/
COPY src/Sample.Web/*.csproj src/Sample.Web/
COPY src/Sample.Web.Core/*.csproj src/Sample.Web.Core/
RUN dotnet restore

# copy everything else and build app
COPY src/Sample.Core/. src/Sample.Core/
COPY src/Sample.Data/. src/Sample.Data/
COPY src/Sample.Services/. src/Sample.Services/
COPY src/Sample.Web/. src/Sample.Web/
COPY src/Sample.Web.Core/. src/Sample.Web.Core/
# RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
# RUN rm -r -f .\src\Sample.Core\obj\ & rm -r -f .\src\Sample.Core\bin\ & rm -r -f .\src\Sample.Data\obj\ & rm -r -f .\src\Sample.Data\bin\ & rm -r -f .\src\Sample.Services\obj\ & rm -r -f .\src\Sample.Services\bin\ & rm -r -f .\src\Sample.Web\obj\ & rm -r -f .\src\Sample.Web\bin\ & rm -r -f .\src\Sample.Web.Core\obj\ & rm -r -f .\src\Sample.Web.Core\bin\
WORKDIR /source/src/Sample.Web
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Sample.Web.dll"]
#ENTRYPOINT ["tail", "-f", "/dev/null"]