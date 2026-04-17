using System;
using System.Runtime.InteropServices;
using static Win32;

class Program
{
	// controls
	static IntPtr hResult;
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch (msg)
		{
			case WM_COMMAND:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;

				if (controlId >= 8 && controlId <= 16)
				{
					string digit = (controlId - 7).ToString(); // Maps digit with id 8->1, 9 -> 2 etc.
					var buffer = new System.Text.StringBuilder(256);
					GetWindowText(hResult, buffer, buffer.Capacity);
					SetWindowText(hResult, buffer.ToString() + digit);
				}
				else if (controlId == 18)
				{
					var buffer = new System.Text.StringBuilder(256);
					GetWindowText(hResult, buffer, buffer.Capacity);
					SetWindowText(hResult, buffer.ToString() + "0");
				}
				break;
			} 
			case WM_CTLCOLORSTATIC:
			{
				IntPtr hdc = wParam;   // handle device context
				SetTextColor(hdc, 0x00FFFFFF);  // white text.
				SetBkMode(hdc, 1);     // transparent background.
				return CreateSolidBrush(0x00463414);
			}
			case WM_CTLCOLORBTN:
			{
				IntPtr hdc = wParam; 
				SetTextColor(hdc, 0x00000000);
				SetBkMode(hdc, 1);
				return CreateSolidBrush(0x00FFFFFF);
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
			WS_OVERLAPPEDWINDOW,
			100, 100, 400, 400,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero);

		// child windows. 
		hResult = Controls.CreateLabel(hWnd, 3, "", 20, 20, 350,50);

		// GUI
		IntPtr hClearButton  = Controls.CreateButton(hWnd, 4, "AC", 20, 80, 170, 40);
		IntPtr hDeleteButton = Controls.CreateButton(hWnd, 5, "DEL",200, 80, 80, 40);
		IntPtr hDivideButton = Controls.CreateButton(hWnd, 6, "%", 290, 80, 80, 40);

		IntPtr hbuttonTwo      = Controls.CreateButton(hWnd, 7, "1", 20, 130, 80, 40);
		IntPtr hbuttonThree    = Controls.CreateButton(hWnd, 8, "2", 110 , 130, 80, 40);
		IntPtr hButtonFour     = Controls.CreateButton(hWnd, 9, "3", 200, 130, 80, 40);
		IntPtr hMultiplyButton = Controls.CreateButton(hWnd, 10, "*", 290, 130, 80, 40);

		IntPtr hbuttonFour  = Controls.CreateButton(hWnd, 11, "4", 20 ,  180, 80, 40);
		IntPtr hbuttonFive  = Controls.CreateButton(hWnd, 12, "5", 110 , 180, 80, 40);
		IntPtr hbuttonSix   = Controls.CreateButton(hWnd, 13, "6", 200 , 180, 80, 40);
		IntPtr hAddbutton   = Controls.CreateButton(hWnd, 14, "+", 290 , 180, 80, 40);

		IntPtr hButtonSeven = Controls.CreateButton(hWnd, 15, "7", 20,  230, 80, 40);
		IntPtr hbuttonEight = Controls.CreateButton(hWnd, 16, "8", 110, 230, 80, 40);
		IntPtr hbuttonNine  = Controls.CreateButton(hWnd, 17, "9", 200, 230, 80, 40);
		IntPtr hbuttonDot   = Controls.CreateButton(hWnd, 18, "-", 290, 230, 80, 40);

		IntPtr hButtonDot    = Controls.CreateButton(hWnd, 19, ".", 20, 280, 80, 40);
		IntPtr hbuttonZero   = Controls.CreateButton(hWnd, 20, "0", 110, 280, 80, 40);
		IntPtr hbuttonEqual  = Controls.CreateButton(hWnd, 21, "=", 200, 280, 170, 40);
		
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
