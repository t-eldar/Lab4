namespace Lab4.Exceptions;

public class ServiceException : Exception
{
	public ExceptionType ExceptionType { get; set; }
	public ServiceException(ExceptionType exceptionType) : base() 
		=> ExceptionType = exceptionType;
	public ServiceException(ExceptionType exceptionType, string? message) : base(message)
		=> ExceptionType = exceptionType;
	public ServiceException(ExceptionType exceptionType, string? message, Exception innerException)
		: base(message, innerException) => ExceptionType = exceptionType;
}
