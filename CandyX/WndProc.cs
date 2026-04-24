using System;
using Win32.Interop;

public static class WndProcHandler
{
	public static IntPtr hResult;
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch((WindowMsg) msg)
		{
			case WindowMsg.Command:
			{
				break;	
			}
			case WindowMsg.Size:
			{
				RECT rect;
				User32.GetClientRect(hWnd, out rect);

				int clientWidth = rect.right - rect.left;
				int clientHeight = rect.bottom - rect.top;

				// User32.SetWindowPos()
				break;
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