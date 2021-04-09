using System;
using KYLib.MathFn;

namespace ICommon
{
	public interface IFactory
	{
		IApp CreateApp();

		ILoginWindow CreateLoginWindow();


	}
}
