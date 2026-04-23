using System;
using System.Runtime.InteropServices;
using Win32.Interop;
using static Controls;
using static WindowProcedure;

class Program
{
	static void Main()
	{
		string className = "MyDesk";
		IntPtr hInstance = Kernel32.GetModuleHandle(null);

		WNDCLASS wc = new WNDCLASS
		{
			lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
			lpszClassName = className,
			hInstance = hInstance
		};
		User32.RegisterClass(ref wc);

		var AppWnd = new MainWindow(className, "MyDesk", hInstance);

		IntPtr hFont = FPTR_CreateFont();
		IntPtr hCandyButton = CreateModuleButton(AppWnd.Handle, "Candy", 2100, 0, 1, 121, 30, hFont);

		string[] modules = {
			"Estimating", "Planning", "Link & Forecast", "Cashflow",
			"Valuations", "Subcontract Manager", "Cost & Allowables",
			"Materials Received", "Drawings"
		};

		int x = 121;
		
		for (int i = 1; i < modules.Length; i++)
		{
			IntPtr hBtn = Controls.CreateModuleButton(AppWnd.Handle, modules[i],(2000 + i), x, 1, 140, 30, hFont);
			x += 140;
		}

		AppWnd.Show();

		RECT rect;
		User32.GetClientRect(AppWnd.Handle, out rect);

		int clientWidth = rect.right - rect.left;
		int clientHeiht = rect.bottom - rect.top;

		IntPtr hToolbarPanel = User32.CreateWindowEx(
			0, "STATIC", "",
			(uint) (WindowStyles.Child | WindowStyles.Visible),
			0, 28, clientWidth, 100,
			AppWnd.Handle,
			(IntPtr)34,
			IntPtr.Zero,
			IntPtr.Zero
		);

		string[] toolbar = { "Main", "Documents", "Reports", "Advanced", "Housekeeping"};
		int posX  = 5;
		for(int i = 0; i < toolbar.Length; i++)
		{
			IntPtr hButton = Controls.CreateToolbarButton(AppWnd.Handle, toolbar[i], (3000 + i), posX, 32, 115, 20, hFont);
			posX += 116;
		}

		MSG msg;
		while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			User32.TranslateMessage(ref msg);
			User32.DispatchMessage(ref msg);
		}
	}
}