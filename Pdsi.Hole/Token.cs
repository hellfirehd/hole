namespace Pdsi.Hole;

public class Token(Int32 position, TokenType type, Int32? value)
{
	private readonly Int32? _value = value;
	private readonly Int32 _position = position;

	public Token(Int32 position, TokenType type)
		: this(position, type, null)
	{
	}

	public TokenType TokenType { get; } = type;
	public Int32 Value => _value!.Value;
	public override String ToString() => _value.HasValue ? $"Token({TokenType}, {_value}) at {_position}" : $"Token({TokenType}) at {_position}";
}