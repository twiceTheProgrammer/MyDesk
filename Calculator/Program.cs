using System;
using System.Runtime.InteropServices;
using static Win32;
using Calculator.API;

class Program
{
	static string currentInput = "";
	static double leftOperand = 0;
	static string? pendingOperator = null;

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
				string? digit = null; // Maps digit with id 8->1, 9 -> 2 etc.

				switch(controlId)
				{
					// digit mapping
					case 2:
					{
						currentInput = "";
						leftOperand = 0;
						pendingOperator = null;
						SetWindowText(hResult, "");
						break;
					}
					case 3:
					{
						if(currentInput.Length > 0)
						{
							currentInput = currentInput.Substring(0, currentInput.Length - 1);
							SetWindowText(hResult, currentInput);
						}
						break;
					}
					case 4:
					{
						if (double.TryParse(currentInput, out leftOperand))
						{
					 		pendingOperator = "/"; 
							currentInput = ""; 
							SetWindowText(hResult, $"{leftOperand} {pendingOperator}");
						}
						break;
					}
					case 5:  digit = "1"; break;
					case 6:  digit = "2"; break;
					case 7:  digit = "3"; break;
					case 8:
					{
						if(double.TryParse(currentInput, out leftOperand))
						{
					 		pendingOperator = "*";
							currentInput = "";
							SetWindowText(hResult, $"{leftOperand} {pendingOperator}");
						}
						break;
					}
					case 9:  digit = "4"; break;
					case 10: digit = "5"; break;
					case 11: digit = "6"; break;
					case 12:
					{
						if ( double.TryParse(currentInput, out leftOperand))
						{
							pendingOperator = "+"; 
							SetWindowText(hResult, $"{leftOperand} {pendingOperator}");
							currentInput = "";  // reset for next number.
						}
						break;
					}
					case 13: digit = "7"; break;
					case 14: digit = "8"; break;
					case 15: digit = "9"; break;
					case 16:
					{
						if(double.TryParse(currentInput, out leftOperand))
						{
							pendingOperator = "-"; 
							currentInput = "";
							SetWindowText(hResult, $"{leftOperand} {pendingOperator}");
						}
						break;
					}
					case 17:
					{
						if(!currentInput.Contains("."))  // prevent multiple decimals
						{
							currentInput = currentInput.Length == 0 ? "0." : currentInput + ".";
							SetWindowText(hResult, currentInput);
						}
						break;
					}
					case 18: digit = "0"; break;
					case 19:
					{
						if (double.TryParse(currentInput, out double rightOperand))
						{
							double result = 0;
							switch(pendingOperator)
							{
								case "+": result = CalculatorAPI.Add(leftOperand, rightOperand); break;
								case "-": result = leftOperand - rightOperand; break;
								case "*": result = leftOperand * rightOperand; break;
								case "/": result = rightOperand != 0 ? leftOperand / rightOperand : double.NaN; break;
							}

							SetWindowText(hResult, $"{leftOperand} {pendingOperator} {rightOperand} = {result}"); // format nicely
							currentInput = "";   // reset for next input
							leftOperand = result;  // allow chaining.
							pendingOperator = null;
						}
						break;
					}

				}
				if (digit != null)  
				{
					currentInput += digit;
					SetWindowText(hResult, currentInput);
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
