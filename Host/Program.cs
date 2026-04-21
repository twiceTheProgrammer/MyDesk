using System;
using System.Runtime.InteropServices;
using static Win32;

class Program
{
	static IntPtr hContentPanel;
	static IntPtr hContentLabel;

	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch (msg)
		{
			case WM_COMMAND:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;
				// Module buttons start at 2000
				if (controlId >= 2000 && controlId < 2100)
				{
					SetWindowText(hContentLabel, $"Module: {controlId - 2000}");
				}
				// Toolbar buttons start at 3000
				else if (controlId >= 3000 && controlId < 3100)
				{
					SetWindowText(hContentLabel, $"Toolbar: {controlId - 3000}");
				}
				break;
			}
			case WM_CTLCOLORBTN:
			case WM_CTLCOLORSTATIC:
			{
				IntPtr hdc = wParam;
				SetBkMode(hdc, OPAQUE);
				return CreateSolidBrush(0x00303040); // unify controls with dark background
			}
			case WM_ERASEBKGND:
			{
				IntPtr hdc = wParam;
				IntPtr hBrush = CreateSolidBrush(0x005D4A3B); // #3b4a5d
				RECT rect;
				GetClientRect(hWnd, out rect);
				FillRect(hdc, ref rect, hBrush);
				return 1;
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

		// Font
		IntPtr hFont = CreateFont(
			18, 0, 0, 0, FW_NORMAL,
			0, 0, 0, DEFAULT_CHARSET,
			OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS,
			DEFAULT_QUALITY, DEFAULT_PITCH | FF_SWISS,
			"Segoe UI"
		);

		// === Module Bar ===
		string[] modules = {
			"Estimating", "Planning", "Link & Forecast", "Cashflow",
			"Valuations", "Subcontract Manager", "Cost & Allowables",
			"Materials Received", "Drawings"
		};

		int x = 10;
		for (int i = 0; i < modules.Length; i++)
		{
			IntPtr hBtn = CreateWindowEx(
				0, "BUTTON", modules[i],
				WS_CHILD | WS_VISIBLE | BS_FLAT | BS_CENTER,
				x, 40, 120, 30,
				hWnd,
				(IntPtr)(2000 + i),
				IntPtr.Zero,
				IntPtr.Zero
			);
			SendMessage(hBtn, WM_SETFONT, hFont, (IntPtr)1);
			x += 125;
		}

		// === Toolbar Row ===
		string[] toolbar = { "Main", "Documents", "Reports", "Advanced", "Housekeeping" };
		x = 10;
		for (int i = 0; i < toolbar.Length; i++)
		{
			IntPtr hBtn = CreateWindowEx(
				0, "BUTTON", toolbar[i],
				WS_CHILD | WS_VISIBLE | BS_FLAT | BS_CENTER,
				x, 80, 100, 25,
				hWnd,
				(IntPtr)(3000 + i),
				IntPtr.Zero,
				IntPtr.Zero
			);
			SendMessage(hBtn, WM_SETFONT, hFont, (IntPtr)1);
			x += 105;
		}

		// === Content Panel ===
		hContentPanel = CreateWindowEx(
			0, "STATIC", "",
			WS_CHILD | WS_VISIBLE | WS_BORDER,
			10, 120, 960, 680,
			hWnd,
			(IntPtr)800,
			IntPtr.Zero,
			IntPtr.Zero
		);

		hContentLabel = CreateWindowEx(
			0, "STATIC", "Content Area",
			WS_CHILD | WS_VISIBLE | SS_CENTER,
			20, 20, 920, 30,
			hContentPanel,
			(IntPtr)801,
			IntPtr.Zero,
			IntPtr.Zero
		);
		SendMessage(hContentLabel, WM_SETFONT, hFont, (IntPtr)1);

		// === Footer/Status ===
		IntPtr hFooter = CreateWindowEx(
			0, "STATIC", "Dev-folder = C:\\Demo-Main",
			WS_CHILD | WS_VISIBLE | SS_LEFT,
			10, 820, 400, 30,
			hWnd,
			(IntPtr)950,
			IntPtr.Zero,
			IntPtr.Zero
		);
		SendMessage(hFooter, WM_SETFONT, hFont, (IntPtr)1);

		ShowWindow(hWnd, SW_SHOWMAXIMIZED); // Start App in Maximized state.
		UpdateWindow(hWnd);

		// Message loop
		MSG msg;
		while (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			TranslateMessage(ref msg);
			DispatchMessage(ref msg);
		}
	}
}