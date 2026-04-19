using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.API
{
	internal class CalculatorAPI 
	{
		private const string DLL_PATH = "calc-api.dll";
		[DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		public static extern double Add(double a, double b);

		[DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		public static extern double Subtract(double a, double b);

		[DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		public static extern double Multiply(double a, double b);
	
		[DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		public static extern double Divide(double a, double b);
	}

}