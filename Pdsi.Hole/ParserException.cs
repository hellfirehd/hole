using System;

namespace Pdsi.Hole
{
	internal class ParserException : Exception
	{
		public ParserException()
		{
		}

		public ParserException(String message) : base(message)
		{
		}

		public ParserException(String message, Exception innerException) : base(message, innerException)
		{
		}
	}
}