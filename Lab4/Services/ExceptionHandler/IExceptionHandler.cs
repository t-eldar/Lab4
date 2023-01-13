using Lab4.Exceptions;

namespace Lab4.Services;

public interface IExceptionHandler
{
	void Handle(Action? action, Action<ExceptionType, string>? onException);
	Task HandleAsync(Func<Task> action, Action<ExceptionType, string>? onException);
}
