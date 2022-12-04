namespace Api.Configuration;

public class LoggingId
{
    public string UnicId { get; }

    public LoggingId()
    {
        UnicId = Guid.NewGuid().ToString();
    }
}
