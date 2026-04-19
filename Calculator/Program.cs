using System;
using System.Runtime.InteropServices;
using static Win32;
using Calculator.API;

class Program
{
	static IntPtr hResult;
	static State state = new State();
	static Engine engine = new Engine(state);
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch (msg)
		{
			case WM_COMMAND:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;

				switch(controlId)
				{
					// digit mapping
					case 2:
					{
						state.Reset();
						SetWindowText(hResult, "");
						break;
					}
					case 3:
					{
						if(state.CurrentInput.Length > 0)
						{
							state.CurrentInput = state.CurrentInput.Substring(0, state.CurrentInput.Length - 1);
							SetWindowText(hResult, state.CurrentInput);
						}
						break;
					}
					case 4:  engine.SetOperator("/"); SetWindowText(hResult, $"{state.LeftOperand} /"); break;
					case 5:  engine.AppendDigit("1"); SetWindowText(hResult, state.CurrentInput); break;
					case 6:  engine.AppendDigit("2"); SetWindowText(hResult, state.CurrentInput); break;
					case 7:  engine.AppendDigit("3"); SetWindowText(hResult, state.CurrentInput); break;

					case 8:	 engine.SetOperator("*"); SetWindowText(hResult, $"{state.LeftOperand} x"); break;
					case 9:  engine.AppendDigit("4"); SetWindowText(hResult, state.CurrentInput); break;
					case 10: engine.AppendDigit("5"); SetWindowText(hResult, state.CurrentInput); break;
					case 11: engine.AppendDigit("6"); SetWindowText(hResult, state.CurrentInput); break;

					case 12: engine.SetOperator("+"); SetWindowText(hResult, $"{state.LeftOperand} +"); break;
					case 13: engine.AppendDigit("7"); SetWindowText(hResult, state.CurrentInput); break;
					case 14: engine.AppendDigit("8"); SetWindowText(hResult, state.CurrentInput); break;
					case 15: engine.AppendDigit("9"); SetWindowText(hResult, state.CurrentInput); break;
					
					case 16: engine.SetOperator("-"); SetWindowText(hResult, $"{state.LeftOperand} -"); break;
					case 17: engine.AppendDecimal();  SetWindowText(hResult, state.CurrentInput); break;
					case 18: engine.AppendDigit("0"); SetWindowText(hResult, state.CurrentInput); break;
					case 19:
					{
						if (double.TryParse(state.CurrentInput, out double rightOperand))
						{
							string output = engine.Evaluate(rightOperand);
							SetWindowText(hResult, output);
						}
						break;
					}
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
		hResult = Controls.CreateLabel(hWnd, 1, "", 20, 20, 340,60);

		// GUI
		IntPtr hClearButton  = Controls.CreateButton(hWnd, 2, "AC", 20, 85, 170, 40);
		IntPtr hDeleteButton = Controls.CreateButton(hWnd, 3, "DEL",195, 85, 80, 40);
		IntPtr hDivideButton = Controls.CreateButton(hWnd, 4, "%", 280, 85, 80, 40);

		IntPtr hbuttonTwo      = Controls.CreateButton(hWnd, 5, "1", 20, 130, 85, 40);
		IntPtr hbuttonThree    = Controls.CreateButton(hWnd, 6, "2", 110 , 130, 80, 40);
		IntPtr hButtonFour     = Controls.CreateButton(hWnd, 7, "3", 195, 130, 80, 40);
		IntPtr hMultiplyButton = Controls.CreateButton(hWnd, 8, "*", 280, 130, 80, 40);

		IntPtr hbuttonFour  = Controls.CreateButton(hWnd, 9, "4", 20 , 175, 85, 40);
		IntPtr hbuttonFive  = Controls.CreateButton(hWnd, 10, "5", 110 , 175, 80, 40);
		IntPtr hbuttonSix   = Controls.CreateButton(hWnd, 11, "6", 195 , 175, 80, 40);
		IntPtr hAddbutton   = Controls.CreateButton(hWnd, 12, "+", 280 , 175, 80, 40);

		IntPtr hButtonSeven = Controls.CreateButton(hWnd, 13, "7", 20,  220, 85, 40);
		IntPtr hbuttonEight = Controls.CreateButton(hWnd, 14, "8", 110, 220, 80, 40);
		IntPtr hbuttonNine  = Controls.CreateButton(hWnd, 15, "9", 195, 220, 80, 40);
		IntPtr hbuttonDot   = Controls.CreateButton(hWnd, 16, "-", 280, 220, 80, 40);

		IntPtr hButtonDot    = Controls.CreateButton(hWnd, 17, ".", 20, 265, 85, 40);
		IntPtr hbuttonZero   = Controls.CreateButton(hWnd, 18, "0", 110, 265, 80, 40);
		IntPtr hbuttonEqual  = Controls.CreateButton(hWnd, 19, "=", 195, 265, 165, 40);
		
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
