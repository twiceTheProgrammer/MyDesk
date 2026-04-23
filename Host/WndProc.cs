using System;
using System.Runtime.InteropServices;
using Win32.Interop;

public static class WindowProcedure
{
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch((WindowMsg) msg)
		{
			case WindowMsg.Command:
			{
				int controlId = wParam.ToInt32() & 0xFFFF;
				// Module buttons start at 2000
				// if (controlId >= 2000 && controlId < 2100)
				// {
				// 	SetWindowText(hContentLabel, $"Module: {controlId - 2000}");
				// }
				// // Toolbar buttons start at 3000
				// else if (controlId >= 3000 && controlId < 3100)
				// {
				// 	SetWindowText(hContentLabel, $"Toolbar: {controlId - 3000}");
				// }
				break;	
			}
			case WindowMsg.CtlColorStatic:
			{
				IntPtr hdc = wParam;   // handle device context
				Gdi32.SetTextColor(hdc, 0x00FFFFFF);  // white text.
				Gdi32.SetBkMode(hdc, 1);     // transparent background.
				return Gdi32.CreateSolidBrush(0x00D8D8D8);
			}
			case WindowMsg.CtlColorBtn:
			{
				IntPtr hdc = wParam; 
				Gdi32.SetTextColor(hdc, 0x00000000);
				Gdi32.SetBkMode(hdc, 1);
				return Gdi32.CreateSolidBrush(0x00D8D8D8);
			}
			case WindowMsg.EraseBackground:
			{
				IntPtr hdc = wParam;
				IntPtr hBrush = Gdi32.CreateSolidBrush(0x005D4A3B); // #3b4a5d
				RECT rect;
				User32.GetClientRect(hWnd, out rect);
				User32.FillRect(hdc, ref rect, hBrush);
				return  (IntPtr)1;
			}
			case WindowMsg.Close:
			{
				Environment.Exit(0);
				break;	
			}
		}
		return User32.DefWindowProc(hWnd, msg, wParam, lParam);
	}
}