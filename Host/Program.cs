using System;
using System.Runtime.InteropServices;
using static Win32;
class Program
{
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch(msg)
		{
			case WM_COMMAND:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;
				switch(controlId)
				{
					case 1:
					{
						// System.Diagnostics.Process.Start("Calculator.exe");
						break;
					}
				}
				break;
			}
			case WM_CTLCOLORSTATIC:
			{
				IntPtr hdc = wParam;  // device context.
				SetBkMode(hdc, OPAQUE);
				return CreateSolidBrush(0x00F0F0F0);
			}
			case WM_CLOSE:
			{
				Environment.Exit(0);
				break;
			}
		}
		return DefWindowProc(hWnd, msg, wParam, lParam);
	}

	static void Main()
	{
		string className = "MyDesk";

		IntPtr hInstance = GetModuleHandle(null);

		WNDCLASS wc = new WNDCLASS
		{
			lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
			lpszClassName = className,
			hInstance = hInstance
		};

		RegisterClass(ref wc);

		IntPtr hWnd = CreateWindowEx(
			0,
			className,
			"MyDesk",
			WS_OVERLAPPEDWINDOW | WS_CAPTION,
			100, 100, 1000, 900,
			IntPtr.Zero,
			IntPtr.Zero,
			hInstance,
			IntPtr.Zero
		);
		
		// font
		IntPtr hFont = CreateFont(
			18, 0, 0, 0, FW_NORMAL,
			0, 0, 0, DEFAULT_CHARSET,
			OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS,
			DEFAULT_QUALITY, DEFAULT_PITCH | FF_SWISS,
			"Segeo UI"
		);

		// Main menu 
		IntPtr hMenu = CreateMenu();

		AppendMenu(hMenu, MF_STRING, 100, "File");
		AppendMenu(hMenu, MF_STRING, 200, "Edit");
		AppendMenu(hMenu, MF_STRING, 500, "About");

		SetMenu(hWnd, hMenu);  // Attach menu to Main Window.

		IntPtr hSideBar = CreateWindowEx(
			0,
			"STATIC", "",
			WS_CHILD | WS_VISIBLE,
			0, 0, 220, 900,
			hWnd,
			(IntPtr)700,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hSideBarTitle = CreateWindowEx(
			0,
			"STATIC", "Welcome to my Desk",
			WS_CHILD | WS_VISIBLE | SS_CENTER,
			10, 10, 200, 30,
			hWnd,
			(IntPtr) 600,
			IntPtr.Zero,
			IntPtr.Zero
		);

		// ===
		string[] navItems = { "Home", "Calculator", "Task Manager", "Settings", "About" };

		int y = 50; // starting Y position
		for (int i = 0; i < navItems.Length; i++)
		{
			IntPtr hButton = CreateWindowEx(
				0,
				"BUTTON", navItems[i],
				WS_CHILD | WS_VISIBLE | BS_FLAT | BS_CENTER,
				10, y, 180, 30,   // x, y, width, height
				hWnd,
				(IntPtr)(1000 + i), // unique ID
				IntPtr.Zero,
				IntPtr.Zero
			);
			y += 35; // move down for next label
			// Apply font.

			SendMessage(hButton, WM_SETFONT, hFont, (IntPtr)1);
		}
		
		ShowWindow(hWnd, 1);
		UpdateWindow(hWnd);

		// Run Message loop
		MSG msg;
		while (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			TranslateMessage(ref msg);
			DispatchMessage(ref msg);
		}
	}
}