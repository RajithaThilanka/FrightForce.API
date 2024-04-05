using System.Collections;

namespace FrightForce.Application.Common.Exceptions;

public class BusinessException: Exception
{
    public int? ErrorCode { get; }
    public new IDictionary Data { get; }

    public BusinessException(string message) : base(message)
    {
        Data = new Hashtable();
    }
    
    public BusinessException(string message, int errorCode) : base(message)
    {
        ErrorCode = errorCode;
        Data = new Hashtable();
    }

    public BusinessException(string message, int errorCode, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
        Data = new Hashtable();
    }
    public void AddData(string key, object value)
    {
        Data.Add(key, value);
    }
    public class ValueAlreadyExistException : BusinessException
    {
        public ValueAlreadyExistException(string message) : base(message)
        { }
    }
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message) : base(message) { }
    }
}