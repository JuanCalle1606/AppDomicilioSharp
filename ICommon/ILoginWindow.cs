using ICommon.Bases;

namespace ICommon
{
	public interface ILoginWindow : IWindow
	{
		IPage LoginPage { get; }

		IPage RegisterPage { get; }
	}
}