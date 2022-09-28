FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# # # Download PwC certificates.
# WORKDIR /opt/scripts
# COPY ["scripts/get_pwc_certs.sh", "."]
# RUN set -e; \
#     apt-get update; \
#     apt-get install -y curl; \
#     ./get_pwc_certs.sh

# # # Install PwC certificates.
# RUN set -e; \
#     update-ca-certificates; \
#     useradd -m appuser;

WORKDIR /app
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "CommandsService.dll" ]