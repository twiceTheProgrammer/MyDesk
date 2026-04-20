using System;
using System.Runtime.InteropServices;

public static class Win32
{
	public const int WS_CHILD = 0X40000000;
	public const int WS_VISIBLE = 0x10000000;
	public const int WS_BORDER = 0x00800000;
	public const int WS_OVERLAPPED = 0x00000000;

	public const int WS_OVERLAPPEDWINDOW = 0x00CF0000;

	[StructLayout(LayoutKind.Sequential)]
	public struct WNDCLASS
	{
		public uint style;
		public IntPtr lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszMenuName;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszClassName;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MSG
	{
		public IntPtr hwnd;
		public uint message;
		public IntPtr wParam;
		public IntPtr lParam;
		public uint time;
		public int pt_x;
		public int pt_y;
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern ushort RegisterClass(ref WNDCLASS lpWndClass);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
	[DllImport("user32.dll")]
	public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
	[DllImport("user32.dll")]
	public static extern bool UpdateWindow(IntPtr hWnd);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
	[DllImport("user32.dll")]
	public static extern bool TranslateMessage([In] ref MSG lpMsg);
	[DllImport("user32.dll")]
	public static extern IntPtr DispatchMessage([In] ref MSG lpMsg); 
	[DllImport("user32.dll")]
	public static extern IntPtr SetWindowText(IntPtr hWnd, string lpString);
}