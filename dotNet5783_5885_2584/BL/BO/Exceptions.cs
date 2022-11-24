

namespace BO;

public class ExceptionInvalidInput : Exception
{
    public ExceptionInvalidInput(string msg):base(msg)
    {
    }
    public ExceptionInvalidInput() : base()
    {
    }
    public ExceptionInvalidInput(string msg,Exception exp):base(msg,exp)
    {
    }
}
public class ExceptionDeleteEntityDependence : Exception
{
    public ExceptionDeleteEntityDependence(string msg) : base(msg)
    {
    }
    public ExceptionDeleteEntityDependence() : base()
    {
    }
    public ExceptionDeleteEntityDependence(string msg, Exception exp) : base(msg, exp)
    {
    }
}

public class ExceptionProductOutOfStock : Exception
{
    public ExceptionProductOutOfStock(string msg) : base(msg)
    {
    }
    public ExceptionProductOutOfStock() : base()
    {
    }
    public ExceptionProductOutOfStock(string msg, Exception exp) : base(msg, exp)
    {
    }
}

public class ExceptionEntityNotFound : Exception
{
    public ExceptionEntityNotFound(string msg) : base(msg)
    {
    }
    public ExceptionEntityNotFound() : base()
    {
    }
    public ExceptionEntityNotFound(string msg, Exception exp) : base(msg, exp)
    {
    }
}
