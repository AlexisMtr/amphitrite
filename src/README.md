>NOTE: The project is built on .NET5, be sure to have the SDK on your machine, or [install it](https://dotnet.microsoft.com/download/dotnet/5.0)
# Configure your dev machine

## Environment variables
| name | description |
| ---- | ----------- |
| ConnectionStrings__DefaultConnection | DB ConnectionString |
| IssuerSigningKey__SigningKey | Key used to sign JWT |

## VisualStudio / VSCode
Use `secret manager` tool to set the 2 variables above
```shell
$ dotnet user-secrets set "ConnectionsStrings:DefaultConnection" "<sql_connection_string>"
$ dotnet user-secrets set "IssuerSigningKey:SigningKey" "<issuer_key>"
```
> In VisualStudio, you can right click on the project and select `manage user secrets`

# Build
## Using CLI
```shell
$ dotnet build -c <Debug|Release> -o build Athena.csproj
```
## Using Docker
```shell
$ docker build -t amphitrite .
```
# Run
## using CLI
```shell
$ dotnet run
```
## using Docker
```shell
$ docker run -it --rm --name amphitrite \
> -e "ConnectionStrings__DefaultConnection=<your_db_connection_string>" \
> -e "IssuerSigningKey__SigningKey=<issuer_key>" \
> -p 8080:80 amphitrite
```