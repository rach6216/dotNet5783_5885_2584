﻿
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
    public override string ToString()
    {

        return $@"
        ERROR - ExceptionInvalidInput:
        {Message}
        ";
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
    public override string ToString()
    {

        return $@"
        ERROR - ExceptionDeleteEntityDependence:
        {Message}
        ";
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
    public override string ToString()
    {

        return $@"
        ERROR - ExceptionProductOutOfStock:
        {Message}
        ";
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
    public override string ToString()
    {
        string iException = (InnerException != null) ? "Inner exception:" + InnerException : "";
        return $@"
        ERROR - ExceptionEntityNotFound:
        {Message}
        {iException}
        ";
    }
}

public class ExceptionCannotCreateItem : Exception
{
    public ExceptionCannotCreateItem(string msg) : base(msg)
    {
    }
    public ExceptionCannotCreateItem() : base()
    {
    }
    public ExceptionCannotCreateItem(string msg, Exception exp) : base(msg, exp)
    {
    }
}

public class ExceptionNullDal : Exception
{
    public ExceptionNullDal(string msg) : base(msg)
    {
    }
    public ExceptionNullDal() : base()
    {
    }
    public ExceptionNullDal(string msg, Exception exp) : base(msg, exp)
    {
    }
}
public class ExceptionNullBl : Exception
{
    public ExceptionNullBl(string msg) : base(msg)
    {
    }
    public ExceptionNullBl() : base()
    {
    }
    public ExceptionNullBl(string msg, Exception exp) : base(msg, exp)
    {
    }
}