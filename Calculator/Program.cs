using System;
using System.Runtime.InteropServices;
using Win32.Interop;
using Calculator.API;

class Program
{
	static void Main()
	{
		string className = "Calculator";

		// Register window class
		WNDCLASS wc = new WNDCLASS
		{
			lpfnWndProc = Marshal.GetFunctionPointerForDelegate(
				new WndProcHandler.WndProcDelegate(WndProcHandler.WndProc)
			),
			lpszClassName = className
		};
		User32.RegisterClass(ref wc);

		// Create main window
		IntPtr hWnd = User32.CreateWindowEx(
			0,
			className,
			"Calculator",
			(uint)(WindowStyles.OverlappedWindow | WindowStyles.Visible),
			100, 100, 400, 400,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero);

		// Result display label
		WndProcHandler.hResult = Controls.CreateLabel(hWnd, 1, "", 20, 20, 340, 60);

		// Buttons
		Controls.CreateButton(hWnd, 2, "AC", 20, 85, 170, 40);
		Controls.CreateButton(hWnd, 3, "DEL", 195, 85, 80, 40);
		Controls.CreateButton(hWnd, 4, "/", 280, 85, 80, 40);

		Controls.CreateButton(hWnd, 5, "1", 20, 130, 85, 40);
		Controls.CreateButton(hWnd, 6, "2", 110, 130, 80, 40);
		Controls.CreateButton(hWnd, 7, "3", 195, 130, 80, 40);
		Controls.CreateButton(hWnd, 8, "*", 280, 130, 80, 40);

		Controls.CreateButton(hWnd, 9, "4", 20, 175, 85, 40);
		Controls.CreateButton(hWnd, 10, "5", 110, 175, 80, 40);
		Controls.CreateButton(hWnd, 11, "6", 195, 175, 80, 40);
		Controls.CreateButton(hWnd, 12, "+", 280, 175, 80, 40);

		Controls.CreateButton(hWnd, 13, "7", 20, 220, 85, 40);
		Controls.CreateButton(hWnd, 14, "8", 110, 220, 80, 40);
		Controls.CreateButton(hWnd, 15, "9", 195, 220, 80, 40);
		Controls.CreateButton(hWnd, 16, "-", 280, 220, 80, 40);

		Controls.CreateButton(hWnd, 17, ".", 20, 265, 85, 40);
		Controls.CreateButton(hWnd, 18, "0", 110, 265, 80, 40);
		Controls.CreateButton(hWnd, 19, "=", 195, 265, 165, 40);

		// Show window
		User32.ShowWindow(hWnd, (int) ShowWindowCommands.Normal);
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
