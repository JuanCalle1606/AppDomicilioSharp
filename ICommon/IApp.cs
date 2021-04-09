using System;
using ICommon.Bases;
using KYLib.MathFn;

namespace ICommon
{
	public interface IApp
	{
		Int AddWindow(IWindow window);

		Int Start();
	}
}
