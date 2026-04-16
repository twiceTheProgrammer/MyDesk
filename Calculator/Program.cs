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
			{
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
			} 
			case WM_KEYDOWN:
			{
				if(wParam.ToInt32() == VK_RETURN)
				{
					var firstInput = new System.Text.StringBuilder(256);
					var secondInput = new System.Text.StringBuilder(256);

					GetWindowText(hInputA, firstInput, firstInput.Capacity);
					GetWindowText(hInputB, secondInput, secondInput.Capacity);

					if (int.TryParse(firstInput.ToString(), out int a) &&
						int.TryParse(secondInput.ToString(), out int b))
					{
						SetWindowText(hResult, $"Result: {a + b}");
					}
					else
					{
						SetWindowText(hResult, "Invalid input");
					}				
				}
				break;
			}
			case WM_CLOSE:
			{
				Environment.Exit(0);
				break;
			} 
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
		hResult = Controls.CreateLabel(hWnd, 3, "", 20, 20, 500,50);

		IntPtr hButton     = Controls.CreateButton(hWnd, 4, "Add", 20, 80, 80, 40);
		IntPtr hMultButton = Controls.CreateButton(hWnd, 5, "Multiply", 110, 80, 80, 40);
		IntPtr hSubButton  = Controls.CreateButton(hWnd, 6, "Subtract", 200, 80, 80, 40);
		IntPtr hDivButton  = Controls.CreateButton(hWnd, 7, "Divide", 290, 80, 80, 40);

		IntPtr hbuttonOne   = Controls.CreateButton(hWnd, 8, "1", 20, 130, 80, 40);
		IntPtr hbuttonTwo   = Controls.CreateButton(hWnd, 9, "2", 110 , 130, 80, 40);
		IntPtr hbuttonThree = Controls.CreateButton(hWnd, 10, "3", 200 , 130, 80, 40);

		IntPtr hbuttonFour = Controls.CreateButton(hWnd, 11, "4", 20 , 180, 80, 40);
		IntPtr hbuttonFive = Controls.CreateButton(hWnd, 12, "5", 110 , 180, 80, 40);
		IntPtr hbuttonSix  = Controls.CreateButton(hWnd, 13, "6", 200 , 180, 80, 40);

		IntPtr hbuttonSeven = Controls.CreateButton(hWnd, 14, "7", 20 , 230, 80, 40);
		IntPtr hbuttonEight = Controls.CreateButton(hWnd, 15, "8", 110, 230, 80, 40);
		IntPtr hbuttonNine  = Controls.CreateButton(hWnd, 16, "9", 200, 230, 80, 40);

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
