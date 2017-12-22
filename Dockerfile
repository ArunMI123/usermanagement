FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

RUN apt update
RUN apt install curl -y
# build runtime image
FROM registry.k8s/corefb
WORKDIR /app
COPY --from=build-env /app/out .
COPY filebeat.yml /etc/filebeat/filebeat.yml
COPY startup.sh /usr/bin
RUN chmod +x /usr/bin/startup.sh
ENTRYPOINT ["/usr/bin/startup.sh"]