using System;
using System.Runtime.InteropServices;

public static class Win32
{
	// Window 
	public const int WS_CHILD = 0X40000000;
	public const int WS_VISIBLE = 0x10000000;
	public const int WS_BORDER = 0x00800000;
	public const int WS_OVERLAPPED = 0x00000000;
	public const int WS_POPUP = unchecked((int) 0x80000000);
	public const int WS_MINIMIZE = 0x20000000;
	public const int WS_CLIPSIBLINGS = 0x04000000;
	public const int WS_CLIPCHILDREN = 0x02000000;
	public const int WS_MAXIMIZE = 0x01000000;
	public const int WS_CAPTION = 0x00C00000;
	public const int WS_DLGFRAME = 0x00400000;
	public const int WS_SYSMENU = 0x00080000;
	public const int WS_THICKFRAME = 0x00040000;
	public const int WS_MINIMIZEBOX = 0x00020000;
	public const int WS_MAXIMIZEBOX = 0x00010000;
	public const int WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION| WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU;

	// Buttons & styles
	public const int BS_DEFPUSHBUTTON = 0x00000001;
	public const int BS_FLAT = 0x00008000;
	public const int BS_CENTER = 0x00000300;
	public const int SS_CENTER = 0x00000001;
	public const int SS_LEFT = 0x00000000;
	// Messages
	public const int WM_COMMAND = 0X0111;
	public const int WM_CLOSE = 0x0010;
	public const int WM_CTLCOLORSTATIC = 0x0138;
	public const int WM_KEYDOWN = 0x0100;
	public const int WM_KEYUP = 0x0101;
	public const int WM_CHAR = 0x0102; // character input , after translation.
	public const int WM_CTLCOLORBTN = 0x0135;
	public const int WM_SETFONT = 0x0030;  // FONT msg

	// Background 
	public const int TRANSPARENT = 1;
	public const int OPAQUE   	= 2;

	// menu bar 
	public const int MF_STRING = 0x00000000;
	public const int MF_SEPARATOR = 0x00000800;
	// common virtual-key codes
	public const int VK_RETURN = 0x0D;
	// --- 
	public const int FW_NORMAL = 400;
	public const int DEFAULT_CHARSET = 1;
	public const int OUT_DEFAULT_PRECIS = 0;
	public const int CLIP_DEFAULT_PRECIS = 0;
	public const int DEFAULT_QUALITY = 0;
	public const int DEFAULT_PITCH = 0;
	public const int FF_SWISS = 0x20;

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

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
	[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern IntPtr CreateWindowEx(
		uint dwExStyle,
		string lpClassName,
		string lpWindowName,
		uint dwStyle,
		int x, int y,
		int nWidth, int nHeight,
		IntPtr hWndParent,
		IntPtr hMenu,
		IntPtr hInstance,
		IntPtr lpParam);

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
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
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszMenuName;  // menu resource name
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszClassName; // name of this window class
	}

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
	[DllImport("gdi32.dll")]
	public static extern IntPtr CreateSolidBrush(int color);
	[DllImport("gdi32.dll")]
	public static extern IntPtr SetTextColor(IntPtr hdc, int color);
	[DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr CreateFont(
		int nHeight, int nWidth, int nEscapement, int nOrientation,
		int fnWeight, uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut,
		uint fdwCharSet, uint fdwOutputPrecision, uint fdwClipPrecision, 
		uint fdwQuality, uint fdwPitchAndFamily, string lpszFace
	);
	[DllImport("gdi32.dll")]
	public static extern int SetBkMode(IntPtr hdc, int mode);
	[DllImport("user32.dll")]
	public static extern IntPtr CreateMenu();
	[DllImport("user32.dll")]
	public static extern bool SetMenu(IntPtr hWnd, IntPtr hMenu);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern bool AppendMenu(IntPtr hMenu, uint uFlags, uint uIDNewItem, string? lpNewItem);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr GetModuleHandle(string? lpModuleName);
	[DllImport("user32.dll")]
	public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
}