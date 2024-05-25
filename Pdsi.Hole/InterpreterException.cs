namespace Pdsi.Hole;

public class InterpreterException : Exception
{
	public InterpreterException()
	{
	}

	public InterpreterException(String message) : base(message)
	{
	}

	public InterpreterException(String message, Exception innerException) : base(message, innerException)
	{
	}
}