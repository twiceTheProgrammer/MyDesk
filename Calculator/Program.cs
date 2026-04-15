using System;
using System.Runtime.InteropServices;
using static Win32;

class Program
{

	// controls
	static IntPtr hInputA, hInputB, hResult;
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch (msg)
		{
			case WM_COMMAND: 
				int controlId = wParam.ToInt32() & 0xFFFF;
				if (controlId == 1) // button ID
				{
					// buffers to hold input.
					var firstInput  = new System.Text.StringBuilder(256);
					var secondInput = new System.Text.StringBuilder(256);

					// read input.
					GetWindowText(hInputA, firstInput, firstInput.Capacity);
					GetWindowText(hInputB, secondInput, secondInput.Capacity);

					// Parse numbers
					if (int.TryParse(firstInput.ToString(), out int a) && int.TryParse(secondInput.ToString(), out int b))
					{
						int sum = a + b;
						SetWindowText(hResult, $"Result: {sum}");	
					}
					else
					{
						SetWindowText(hResult, "Invalid input");
					}
				}
				break;

			case WM_CLOSE: 
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

		// child windows. 
		hInputA = Controls.CreateEditBox(hWnd, 2, 50, 20, 100, 25);
		hInputB = Controls.CreateEditBox(hWnd, 3, 160, 20, 100, 25);
		hResult = Controls.CreateLabel(hWnd, 4, "Result: ", 50, 100, 200, 25);

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
