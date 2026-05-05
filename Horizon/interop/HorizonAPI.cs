using System;
using System.Runtime.InteropServices;

namespace Horizon.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct HorizonAPI
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
		[DllImport("Horizonhub.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr IHorizonAPI();
		public static HorizonAPI Load()
		{
			IntPtr api = IHorizonAPI();
			return Marshal.PtrToStructure<HorizonAPI>(api);
		}
	}
}