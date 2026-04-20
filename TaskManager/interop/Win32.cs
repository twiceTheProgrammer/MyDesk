using System;
using System.Runtime.InteropServices;

public static class Win32
{
	#region Window styles
	public const int WS_CHILD = 0X40000000;
	public const int WS_VISIBLE = 0x10000000;
	public const int WS_BORDER = 0x00800000;
	public const int WS_OVERLAPPED = 0x00000000;
	public const int WS_OVERLAPPEDWINDOW = 0x00CF0000;
	#endregion

	#region  Edit Control styles
	public const int ES_LEFT = 0x0000;
	public const int ES_CENTER = 0x0001;
	public const int ES_RIGHT = 0x0002;
	public const int ES_MULTILINE = 0x0004;
	public const int ES_AUTOVSCROLL = 0x0040;
	public const int ES_AUTOHSCROLL = 0x0080;
	#endregion

	#region Button styles
	public const int BS_PUSHBUTTON = 0x00000000;
	public const int BS_DEFPUSHBUTTON = 0x00000001;
	public const int BS_CENTER = 0x00000300;
	public const int BS_FLAT = 0x00008000;
	#endregion

	#region List box styles
	public const int LBS_NOTIFY = 0x0001;
	public const int LBS_SORT = 0x0002;
	public const int LBS_HASSTRINGS = 0x0040;
	public const int LB_ADDSTRING = 0x0180;
	public const int LB_DELETESTRING = 0x0182;
	public const int LB_GETCURSEL = 0x0188;
	#endregion

	#region Static Controls styles
	public const int SS_LEFT = 0x00000000;
	public const int SS_CENTER = 0x00000001;
	public const int SS_RIGHT = 0x00000002;
	#endregion

	#region Window messages
	public const int WM_COMMAND = 0x0111;
	public const int WM_CLOSE = 0x0010;
	public const int WM_CTLCOLORSTATIC = 0x0138;
	public const int WM_KEYDOWN = 0x0100;
	public const int WM_KEYUP = 0x0101;
	public const int WM_CHAR = 0x0102;
	public const int WM_CTLCOLORBTN = 0x0135;
	public const int WM_SETFONT = 0x0030;
	public const int WM_GETTEXT = 0x000D;
	public const int WM_GETTEXTLENGTH = 0x000E;
	#endregion

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
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
}