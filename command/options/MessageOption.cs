using System.CommandLine;

namespace Alert4U.options;

internal class MessageOption : Option<string>
{
	public MessageOption()
		: base(name: "--message", description: "Message to be sent.", getDefaultValue: () => "New alert!")
	{
		IsRequired = true;
		AddAlias("-m");
    }
}
