using System;
using System.Runtime.InteropServices;
namespace Win32.Interop
{
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
	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
	[StructLayout(LayoutKind.Sequential)]
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

	[StructLayout(LayoutKind.Sequential)]
	public struct DRAWITEMSTRUCT
	{
		public uint CtlType;
		public uint CtlID;
		public uint itemID;
		public uint itemAction;
		public uint itemState;
		public IntPtr hWndItem;
		public IntPtr hdc;
		public RECT rcItem;
		public IntPtr itemData;	
	}
}