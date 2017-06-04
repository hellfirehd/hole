using System;

namespace Pdsi.Hole
{
	public class Token
	{
		readonly TokenType _type;
		readonly Int32? _value;
		readonly Int32 _position;

		public Token(Int32 position, TokenType type)
			: this(position, type, null)
		{
		}

		public Token(Int32 position, TokenType type, Int32? value)
		{
			_position = position;
			_type = type;
			_value = value;
		}

		public TokenType TokenType => _type;
		public Int32 Value => _value.Value;
		public override String ToString() => _value.HasValue ? $"Token({TokenType}, {_value}) at {_position}" : $"Token({TokenType}) at {_position}";
	}
}