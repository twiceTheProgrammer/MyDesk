using System;
using System.Runtime.InteropServices;
using static Win32;
using static Controls;
using static WindowProcedure;
class Program
{
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

		// Font
		IntPtr hFont = CreateFont(
			18, 0, 0, 0, FW_NORMAL,
			0, 0, 0, DEFAULT_CHARSET,
			OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS,
			DEFAULT_QUALITY, DEFAULT_PITCH | FF_SWISS,
			"Segoe UI"
		);

		IntPtr hCandyButton = CreateWindowEx(
			0, "BUTTON", "Candy",
			WS_CHILD | WS_VISIBLE | BS_FLAT, 
			0, 1, 121, 30,
			hWnd,
			(IntPtr)(2100),
			IntPtr.Zero,
			IntPtr.Zero
		);

		string[] modules = {
			"Estimating", "Planning", "Link & Forecast", "Cashflow",
			"Valuations", "Subcontract Manager", "Cost & Allowables",
			"Materials Received", "Drawings"
		};

		int x = 121;
		
		for (int i = 1; i < modules.Length; i++)
		{
			IntPtr hBtn = Controls.CreateModuleButton(hWnd, modules[i],(2000 + i), x, 1, 140, 30, hFont);
			x += 140;
		}

		ShowWindow(hWnd, SW_SHOWMAXIMIZED); // Start App in Maximized state.
		UpdateWindow(hWnd);

		RECT rect;
		GetClientRect(hWnd, out rect);

		int clientWidth = rect.right - rect.left;
		int clientHeiht = rect.bottom - rect.top;

		IntPtr hToolbarPanel = CreateWindowEx(
			0, "STATIC", "",
			WS_CHILD | WS_VISIBLE,
			0, 28, clientWidth, 100,
			hWnd,
			(IntPtr)34,
			IntPtr.Zero,
			IntPtr.Zero
		);

		string[] toolbar = { "Main", "Documents", "Reports", "Advanced", "Housekeeping"};
		int posX  = 5;
		for(int i = 0; i < toolbar.Length; i++)
		{
			IntPtr hButton = CreateWindowEx(
				0, "BUTTON", toolbar[i],
				WS_CHILD | WS_VISIBLE | BS_FLAT,
				posX, 32, 115, 20,
				hWnd,
				(IntPtr)(3000 + i),
				IntPtr.Zero,
				IntPtr.Zero
			);
			SendMessage(hButton, WM_SETFONT, hFont, (IntPtr)1);
			posX += 116;
		}
		// Message loop
		MSG msg;
		while (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			TranslateMessage(ref msg);
			DispatchMessage(ref msg);
		}
	}
}