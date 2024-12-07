## Steps to Run the application
# Requirnement
1. Dotnet core sdk 8.0
2. Docker

# Setup database

1. ` docker build -t enpal-coding-challenge-db . `
2. ` docker run--name enpal-coding-challenge-db-p 5432:5432-d enpal-coding-challenge-db `
3. Start the Api Project from root directory ` dotnet run --project=src/Enpal.AppointmentBooking.Api/Enpal.AppointmentBooking.Api.csproj `
4. Run unittest from root directory ` dotnet test test/Enpal.AppointmentBooking.Unittests/Enpal.AppointmentBooking.Unittests.csproj `