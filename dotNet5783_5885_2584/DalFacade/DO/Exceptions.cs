namespace DO;

/// <summary>
/// Exception if the id not exist
/// </summary>
public class ExceptionEntityNotFound : Exception
{
    public ExceptionEntityNotFound() : base()
    {
    }
    public ExceptionEntityNotFound(string msg) : base(msg)
    {
    }
}

/// <summary>
/// Exception for throwing when the id already exist
/// </summary>
public class ExceptionIdAlreadyExist : Exception
{
    public ExceptionIdAlreadyExist() : base()
    {
    }
    public ExceptionIdAlreadyExist(string msg) : base(msg)
    {
    }
}
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
