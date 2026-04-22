using System;
using System.Runtime.InteropServices;
using static Win32;

public static class WindowProcedure
{
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch(msg)
		{
			case WM_COMMAND:
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
			case WM_CTLCOLORSTATIC:
			{
				IntPtr hdc = wParam;   // handle device context
				SetTextColor(hdc, 0x00FFFFFF);  // white text.
				SetBkMode(hdc, 1);     // transparent background.
				return CreateSolidBrush(0x00D8D8D8);
			}
			case WM_CTLCOLORBTN:
			{
				IntPtr hdc = wParam; 
				SetTextColor(hdc, 0x00000000);
				SetBkMode(hdc, 1);
				return CreateSolidBrush(0x00D8D8D8);
			}
			case WM_ERASEBKGND:
			{
				IntPtr hdc = wParam;
				IntPtr hBrush = CreateSolidBrush(0x005D4A3B); // #3b4a5d
				RECT rect;
				GetClientRect(hWnd, out rect);
				FillRect(hdc, ref rect, hBrush);
				return  (IntPtr)1;
			}
			case WM_CLOSE:
			{
				Environment.Exit(0);
				break;	
			}
		}
		return DefWindowProc(hWnd, msg, wParam, lParam);
	}
}