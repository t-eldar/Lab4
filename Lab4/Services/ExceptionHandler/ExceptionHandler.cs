using Lab4.Exceptions;

namespace Lab4.Services;

public sealed class ExceptionHandler : IExceptionHandler
{
	public void Handle(Action? action, Action<ExceptionType, string>? onException)
	{
		try
		{
			action?.Invoke();
		}
		catch(ServiceException ex)
		{
			onException?.Invoke(ex.ExceptionType, ex.Message);
		}
		catch (Exception ex)
		{
			onException?.Invoke(ExceptionType.InternalError, ex.Message);
		}
	}
	public async Task HandleAsync(Func<Task> action, Action<ExceptionType, string>? onException)
	{
		try
		{
			await action();
		}
		catch (ServiceException ex)
		{
			onException?.Invoke(ex.ExceptionType, ex.Message);
		}
		catch (Exception ex)
		{
			onException?.Invoke(ExceptionType.InternalError, ex.Message);
		}
	}
}
