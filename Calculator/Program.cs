using System;
using System.Runtime.InteropServices;
using static Win32;



class Program
{
	const int WS_CHILD = 0X40000000;
	const int WS_VISIBLE = 0x10000000;
	const int WS_BORDER = 0x00800000;

	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch (msg)
		{
			case 0x0111: // WM_COMMAND
				int controlId = wParam.ToInt32() & 0xFFFF;
				if (controlId == 1) // button ID
				{
					MessageBox(hWnd, "Button clicked!", "Info", 0);
				}
				break;

			case 0x0010: // WM_CLOSE
				Environment.Exit(0);
				break;
		}
		return DefWindowProc(hWnd, msg, wParam, lParam);  // Default behavior.
	}

	static void Main()
	{
		string className = "Calculator";

		// Register window class
		WNDCLASS wc = new WNDCLASS
		{
			lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
			lpszClassName = className
		};
		RegisterClass(ref wc);

		// Create main window
		IntPtr hWnd = CreateWindowEx(
			0,
			className,
			"Calculator",
			0xCF0000, // WS_OVERLAPPEDWINDOW
			100, 100, 500, 600,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero);

		// First input 
		IntPtr hInputA = CreateWindowEx(
			0, 
			"EDIT", "", 
			WS_CHILD | WS_VISIBLE | WS_BORDER,
			50, 20, 100, 25,
			hWnd, (IntPtr)2,
			IntPtr.Zero,
			IntPtr.Zero 
		);

		// second input 
		IntPtr hInputB = CreateWindowEx(
			0,
			"Edit", "",
			WS_CHILD | WS_VISIBLE | WS_BORDER,
			160, 20, 100, 25,
			hWnd, (IntPtr)3,
			IntPtr.Zero,
			IntPtr.Zero
		);

		// Add a button
		IntPtr hButton = CreateWindowEx(
			0,
			"BUTTON",
			"Click Me",
			0x50010000, // WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON
			50, 50, 100, 30,
			hWnd,
			(IntPtr)1,
			IntPtr.Zero,
			IntPtr.Zero);

		ShowWindow(hWnd, 1);
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
