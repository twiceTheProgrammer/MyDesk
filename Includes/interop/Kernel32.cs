using System;
using System.Runtime.InteropServices;

namespace Win32.Interop
{
	public static class Kernel32
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr GetModuleHandle(string? lpModuleName);
	}
}