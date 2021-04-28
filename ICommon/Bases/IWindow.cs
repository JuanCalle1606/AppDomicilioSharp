using KYLib.Interfaces;

namespace ICommon.Bases
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Una ventana del sistema.
	/// </summary>
	public interface IWindow : INameable
	{
		/// <summary>
		/// Muestra la ventana.
		/// </summary>
		void Show();
	}
}
