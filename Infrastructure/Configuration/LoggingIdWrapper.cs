namespace Infrastructure.Configuration;

public class LoggingIdWrapper
{
    public string UnicId { get; }

    public LoggingIdWrapper()
    {
        UnicId = Guid.NewGuid().ToString();
    }
}
