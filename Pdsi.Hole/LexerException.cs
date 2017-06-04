using System;

namespace Pdsi.Hole
{
	internal class LexerException : Exception
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