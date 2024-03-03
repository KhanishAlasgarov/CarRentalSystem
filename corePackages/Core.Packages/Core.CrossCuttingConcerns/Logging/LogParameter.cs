namespace Core.CrossCuttingConcerns.Logging;

public class LogParameter
{
    public string Name { get; set; }
    public object Value { get; set; }
    public string Type { get; set; }

    public LogParameter()
    {
        this.Name = String.Empty;
        this.Value = String.Empty;
        this.Type = String.Empty;
    }

    public LogParameter(string name, object value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }
}
public class LogDetailWithException : LogDetail
{
    public LogDetailWithException()
    {
        ExceptionMessage = String.Empty;
    }

    public string ExceptionMessage { get; set; }
    public LogDetailWithException(string fullname, string methodName, string user,
        List<LogParameter> parameters,string exceptionMessage) : base(fullname, methodName, user, parameters)
    {
        this.ExceptionMessage = exceptionMessage;
    }
}
