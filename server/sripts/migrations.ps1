$migration_name = Read-Host "Input migration name"
cd ..\TimLearning\TimLearning.Infrastructure.Implementation
dotnet ef --startup-project ..\TimLearning.Host migrations add $migration_name -o "Db/Migrations"
Read-Host "Press Enter to exit"