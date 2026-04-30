using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.API
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct CalculatorAPI
	{
		public uint version;
		public uint size;
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate double CalcOp(double a, double b);
		public CalcOp Add;
		public CalcOp Subtract;
		public CalcOp Multiply;
		public CalcOp Divide;
	}

	internal class Native
	{
		[DllImport("calc-api.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ICalculatorAPI();
		public static CalculatorAPI Load()
		{
			IntPtr api = ICalculatorAPI();
			return Marshal.PtrToStructure<CalculatorAPI>(api);
		}
	}
}