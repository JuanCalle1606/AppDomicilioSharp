using System;
using ICommon;
using ICommon.Bases;
using KYLib.ConsoleUtils;
using KYLib.MathFn;
using UmlBased;

namespace Terminal
{
	public class TerminalApp : ConsoleApp, IApp
	{
		public TerminalApp(string appName) : base(appName)
		{
			//esto es solo para probar el funcionamiento.
			DomiciliosApp.Instance.Compradores.Add(
				new Comprador()
				{
					Id = DomiciliosApp.Instance.NextUserId++,
					Name = "Juan Pablo",
					Direccion = "Medellin",
					Creacion = DateTime.Now,
					Clave = "12345678",
					Telefono = "3124307723",
					Correo = "juanp.calle@upb.edu.co"
				}
			);
		}

		public bool AddWindow(IWindow window)
		{

			return true;
		}

		public Int StartApp()
		{
			Start();
			return 0;
		}
	}
}