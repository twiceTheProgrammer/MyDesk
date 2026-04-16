using System;
using System.Runtime.InteropServices;

public static class Win32
{
	// Window 
	public const int WS_CHILD = 0X40000000;
	public const int WS_VISIBLE = 0x10000000;
	public const int WS_BORDER = 0x00800000;

	// Buttons
	public const int BS_DEFPUSHBUTTON = 0x00000001;
	// Messages
	public const int WM_COMMAND = 0X0111;
	public const int WM_CLOSE = 0x0010;

	// keyboard
	public const int WM_KEYDOWN = 0x0100;
	public const int WM_KEYUP = 0x0101;
	public const int WM_CHAR = 0x0102; // character input , after translation.

	// common virtual-key codes
	public const int VK_RETURN = 0x0D;
	[StructLayout(LayoutKind.Sequential)]
	public struct MSG
	{
		public IntPtr hwnd;    // handle of the window the message is for.
		public uint  message;  // type of message (WM_COMMAND, WM_CLOSE)
		public IntPtr wParam;  // extra info (like which button was clicked)
		public IntPtr lParam;  // more info (like mouse position, control handle)
		public uint time;   // timestamp
		public int pt_x;  // mouse x position
		public int pt_y;  // mouse y position
	}

	[DllImport("user32.dll", SetLastError =true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
	[DllImport("user32.dll", SetLastError =true, CharSet = CharSet.Auto)]
	public static extern IntPtr CreateWindowEx(
		int dwExStyle,
		string lpClassName,
		string lpWindowName,
		int dwStyle,
		int x, int y,
		int nWidth, int nHeight,
		IntPtr hWndParent,
		IntPtr hMenu,
		IntPtr hInstance,
		IntPtr lpParam);

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct WNDCLASS
	{
		public uint style;           // style flags (e.g redraw on resize)
		public IntPtr lpfnWndProc;   // pointer to WndProc function
		public int cbClsExtra;       // extra memory for class
		public int cbWndExtra;       // extra memory for each window
		public IntPtr hInstance;     // app instance handle
		public IntPtr hIcon;         // icon for the window
		public IntPtr hCursor;       // cursor when hovering over window
		public IntPtr hbrBackground; // background brush (color/pattern)
		public string lpszMenuName;  // menu resource name
		public string lpszClassName; // name of this window class
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
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
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern bool SetWindowText(IntPtr hWnd, string lpString);
}