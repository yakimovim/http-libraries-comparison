cd /d ".\Service"

start dotnet run --urls=http://localhost:5001/ & start dotnet run --urls=http://localhost:5002/

cd /d ".."