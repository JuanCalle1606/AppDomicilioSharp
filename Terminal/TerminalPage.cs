using ICommon.Bases;
using KYLib.ConsoleUtils;

namespace Terminal
{
	public class TerminalPage : ConsoleItem
	{
		public TerminalPage(string title)
		{
			Name = title;
			InstaOption = true;
		}
	}
}