using System;
using System.Runtime.InteropServices;
using Win32.Interop;
using Calculator.API;

class Program
{
	static IntPtr hResult;
	static State state = new State();
	static Engine engine = new Engine(state);
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch ((WindowMsg) msg)
		{
			case WindowMsg.Command:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;

				switch(controlId)
				{
					// digit mapping
					case 2:
					{
						state.Reset();
						User32.SetWindowText(hResult, "");
						break;
					}
					case 3:
					{
						if(state.CurrentInput.Length > 0)
						{
							state.CurrentInput = state.CurrentInput.Substring(0, state.CurrentInput.Length - 1);
							User32.SetWindowText(hResult, state.CurrentInput);
						}
						break;
					}
					case 4:  engine.SetOperator("/"); User32.SetWindowText(hResult, $"{state.LeftOperand} /"); break;
					case 5:  engine.AppendDigit("1"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 6:  engine.AppendDigit("2"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 7:  engine.AppendDigit("3"); User32.SetWindowText(hResult, state.CurrentInput); break;

					case 8:	 engine.SetOperator("*"); User32.SetWindowText(hResult, $"{state.LeftOperand} x"); break;
					case 9:  engine.AppendDigit("4"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 10: engine.AppendDigit("5"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 11: engine.AppendDigit("6"); User32.SetWindowText(hResult, state.CurrentInput); break;

					case 12: engine.SetOperator("+"); User32.SetWindowText(hResult, $"{state.LeftOperand} +"); break;
					case 13: engine.AppendDigit("7"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 14: engine.AppendDigit("8"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 15: engine.AppendDigit("9"); User32.SetWindowText(hResult, state.CurrentInput); break;
					
					case 16: engine.SetOperator("-"); User32.SetWindowText(hResult, $"{state.LeftOperand} -"); break;
					case 17: engine.AppendDecimal();  User32.SetWindowText(hResult, state.CurrentInput); break;
					case 18: engine.AppendDigit("0"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 19:
					{
						if (double.TryParse(state.CurrentInput, out double rightOperand))
						{
							string output = engine.Evaluate(rightOperand);
							User32.SetWindowText(hResult, output);
						}
						break;
					}
				}
				break;
			} 
			case WindowMsg.CtlColorStatic:
			{
				IntPtr hdc = wParam;   // handle device context
				Gdi32.SetTextColor(hdc, 0x00FFFFFF);  // white text.
				Gdi32.SetBkMode(hdc, 1);     // transparent background.
				return Gdi32.CreateSolidBrush(0x00463414);
			}
			case WindowMsg.CtlColorBtn:
			{
				IntPtr hdc = wParam; 
				Gdi32.SetTextColor(hdc, 0x00000000);
				Gdi32.SetBkMode(hdc, 1);
				return Gdi32.CreateSolidBrush(0x00FFFFFF);
			}
			case WindowMsg.Close:
			{
				Environment.Exit(0);
				break;
			} 
		}
		return User32.DefWindowProc(hWnd, msg, wParam, lParam);  // Default behavior.
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
		User32.RegisterClass(ref wc);

		// Create main window
		IntPtr hWnd = User32.CreateWindowEx(
			0,
			className,
			"Calculator",
			(uint) (WindowStyles.OverlappedWindow ),
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
		
		User32.ShowWindow(hWnd, 1);
		User32.UpdateWindow(hWnd);

		// Message loop
		MSG msg;
		while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			User32.TranslateMessage(ref msg);
			User32.DispatchMessage(ref msg);
		}
	}
}
