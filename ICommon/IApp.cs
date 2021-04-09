using System;
using ICommon.Bases;
using KYLib.MathFn;

namespace ICommon
{
	public interface IApp
	{
		bool AddWindow(IWindow window);

		Int StartApp();
	}
}
