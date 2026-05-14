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
		public delegate int FPTR_EstimateBricks(int cementBags);
		public delegate int FPTR_SandRequired(int cementBags);
		public delegate int FPTR_CementRequiredForWall(int bricks);
		public delegate int FPTR_BricksForWall(int length, int height, int width);

		public FPTR_EstimateBricks EstimateBricks;
		public FPTR_SandRequired SandRequired;
		public FPTR_CementRequiredForWall CementRequiredForWall;
		public FPTR_BricksForWall BricksForWall;
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