using System;

namespace Pdsi.Hole
{
	public class LexerException : Exception
	{
		public LexerException()
		{
		}

		public LexerException(String message) : base(message)
		{
		}

		public LexerException(String message, Exception innerException) : base(message, innerException)
		{
		}
	}
}