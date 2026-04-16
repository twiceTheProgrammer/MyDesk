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
				// buffers to hold input.
				var firstInput  = new System.Text.StringBuilder(256);
				var secondInput = new System.Text.StringBuilder(256);

				// read input.
				GetWindowText(hInputA, firstInput, firstInput.Capacity);
				GetWindowText(hInputB, secondInput, secondInput.Capacity);

				// Parse numbers
				if (int.TryParse(firstInput.ToString(), out int a) && int.TryParse(secondInput.ToString(), out int b))
				{
					switch(controlId)
					{
						case 4:
						{
							SetWindowText(hResult, $"Result: {a + b}");
							break;
						}
						case 5:
						{
							SetWindowText(hResult, $"Result {a * b}");
							break;
						}
						case 6:
						{
							SetWindowText(hResult, $"Result: {a - b}");
							break;
						}
						case 7:
						{
							if (b != 0)
							{
								SetWindowText(hResult, $"Result: {a / b}");
							}
							else
							{
								SetWindowText(hResult, $"Cannot divide by zero!");
							}
							break;
						}
					}
				}
				else
				{
					SetWindowText(hResult, "Invalid input");
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
		hInputA = Controls.CreateEditBox(hWnd, 1, 50, 20, 100, 25);
		hInputB = Controls.CreateEditBox(hWnd, 2, 160, 20, 100, 25);
		hResult = Controls.CreateLabel(hWnd, 3, "Result: ", 270, 20, 200, 25);

		IntPtr hButton     = Controls.CreateButton(hWnd, 4, "Add", 50, 50, 100, 30);
		IntPtr hMultButton = Controls.CreateButton(hWnd, 5, "Multiply", 160, 50, 100, 30);
		IntPtr hSubButton = Controls.CreateButton(hWnd, 6, "Subtract", 270, 50, 100, 30);
		IntPtr hDivButton = Controls.CreateButton(hWnd, 7, "Divide", 380, 50, 100, 30);

		
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
