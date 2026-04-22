using System;
using System.Runtime.InteropServices;

namespace Win32.Interop
{
	public static class User32
	{
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr CreateWindowEx(
			int dwExStyle,
			string lpClassName,
			string lpWindowName,
			uint dwStyle,
			int x, int y,
			int nWidth, int nHeight,
			IntPtr hWndParent,
			IntPtr hMenu,
			IntPtr hInstance,
			IntPtr lpParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern ushort RegisterClass(ref WNDCLASS lpWndClass);
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern bool UpdateWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		public static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
		[DllImport("user32.dll")]
		public static extern bool TranslateMessage([In] ref MSG lpMsg);
		[DllImport("user32.dll")]
		public static extern IntPtr DispatchMessage([In] ref MSG lpMsg);
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern bool SetWindowText(IntPtr hWnd, string lpString);
		[DllImport("user32.dll")]
		public static extern int GetClientRec(IntPtr hWnd, out RECT lpRect);
		[DllImport("user32.dll")]
		public static extern int FillRect(IntPtr hdc, ref RECT lprc, IntPtr hbr);
	}
}