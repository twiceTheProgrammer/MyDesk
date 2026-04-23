using System;
using System.Runtime.InteropServices;

namespace Win32.Interop
{
	public static class Gdi32
	{
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateSolidBrush(int color);
		[DllImport("gdi32.dll")]
		public static extern IntPtr SetTextColor(IntPtr hdc, int color);
		[DllImport("gdi32.dll")]
		public static extern int SetBkMode(IntPtr hdc, int mode);
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr CreateFont(
					int nHeight, int nWidth, int nEscapement, int nOrientation,
					uint fnWeight, uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut,
					uint fdwCharSet, uint fdwOutputPrecision, uint fdwClipPrecision,
					uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);
	}
}