using System;
using Win32.Interop;
using Calculator.API;

public static class WndProcHandler
{
	public static IntPtr hResult;
	private static State state = new State();
	private static Engine engine = new Engine(state);
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch ((WindowMsg) msg)
		{
			case WindowMsg.Command:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;

				switch (controlId)
				{
					case 2:
						state.Reset();
						User32.SetWindowText(hResult, "");
						break;

					case 3:
						if (state.CurrentInput.Length > 0)
						{
							state.CurrentInput = state.CurrentInput.Substring(0, state.CurrentInput.Length - 1);
							User32.SetWindowText(hResult, state.CurrentInput);
						}
						break;

					case 4: engine.SetOperator("/"); User32.SetWindowText(hResult, $"{state.LeftOperand} /"); break;
					case 5: engine.AppendDigit("1"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 6: engine.AppendDigit("2"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 7: engine.AppendDigit("3"); User32.SetWindowText(hResult, state.CurrentInput); break;

					case 8: engine.SetOperator("*"); User32.SetWindowText(hResult, $"{state.LeftOperand} x"); break;
					case 9: engine.AppendDigit("4"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 10: engine.AppendDigit("5"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 11: engine.AppendDigit("6"); User32.SetWindowText(hResult, state.CurrentInput); break;

					case 12: engine.SetOperator("+"); User32.SetWindowText(hResult, $"{state.LeftOperand} +"); break;
					case 13: engine.AppendDigit("7"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 14: engine.AppendDigit("8"); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 15: engine.AppendDigit("9"); User32.SetWindowText(hResult, state.CurrentInput); break;

					case 16: engine.SetOperator("-"); User32.SetWindowText(hResult, $"{state.LeftOperand} -"); break;
					case 17: engine.AppendDecimal(); User32.SetWindowText(hResult, state.CurrentInput); break;
					case 18: engine.AppendDigit("0"); User32.SetWindowText(hResult, state.CurrentInput); break;

					case 19: 
						if (double.TryParse(state.CurrentInput, out double rightOperand))
						{
							string output = engine.Evaluate(rightOperand);
							User32.SetWindowText(hResult, output);
						}
						break;
				}
				break;
			}

			case WindowMsg.CtlColorStatic:
			{
				IntPtr hdc = wParam;
				Gdi32.SetTextColor(hdc, 0x00FFFFFF);      // white text
				Gdi32.SetBkMode(hdc, (int)BackgroundMode.Transparent);
				return Gdi32.CreateSolidBrush(0x00463414);   // dark background
			}

			case WindowMsg.CtlColorBtn:
			{
				IntPtr hdc = wParam;
				Gdi32.SetTextColor(hdc, 0x00000000); // black text
				Gdi32.SetBkMode(hdc, (int)BackgroundMode.Transparent);
				return Gdi32.CreateSolidBrush(0x00FFFFFF); // white background
			}

			case WindowMsg.Close:
				Environment.Exit(0);
				break;
		}

		return User32.DefWindowProc(hWnd, msg, wParam, lParam);
	}
}