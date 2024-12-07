namespace Enpal.AppointmentBooking.Infrastructure.Configurations;

public class DatabaseSetting
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DatabaseName { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public string GetConnectionString()
    {
        return $"Host={Host};Port={Port};Database={DatabaseName};Username={User};Password={Password}";
    }
}
