dotnet tool install -g dotnet-ef

dotnet ef dbcontext scaffold "$env:CONN_STR" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir ./Entities --context-dir . --context TodoDbContext --no-onconfiguring --namespace efscaffold.Entities --context-namespace efscaffold --schema todo --force