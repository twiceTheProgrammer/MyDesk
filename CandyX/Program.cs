using System;
using System.Data;
using System.Runtime.InteropServices;
using Win32.Interop;
class Program
{
	static void Main()
	{
		string className = "CandyX";

		// Register window class 
		WNDCLASS wc = new WNDCLASS
		{
			lpfnWndProc = Marshal.GetFunctionPointerForDelegate(
				new WndProcHandler.WndProcDelegate(WndProcHandler.WndProc)
			),
			lpszClassName = className,
			hCursor = User32.LoadCursor(IntPtr.Zero, (int)IDC.Arrow)
		};
		User32.RegisterClass(ref wc);

		// createw main window
		IntPtr hWnd = User32.CreateWindowEx(
			0,
			className,
			"CandyX",
			(uint) (WindowStyles.OverlappedWindow),
			20, 20, 1500, 1000,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero
		);

		// show window
		User32.ShowWindow(hWnd, (int) ShowWindowCommands.Maximized);
		User32.UpdateWindow(hWnd);

		IntPtr hFont = Controls.FPTR_CreateFont();

		IntPtr hCandyButton        = Controls.CreateModuleButton(hWnd, "Candy",              1,  0, 0, 20, 20, hFont);
		IntPtr hEstButton          = Controls.CreateModuleButton(hWnd, "Estimating",         2,  141, 0, 60, 20, hFont);
		IntPtr hPlanningButton     = Controls.CreateModuleButton(hWnd, "Planning",           3 , 282, 0, 80, 20, hFont);
		IntPtr hLinkForecastButton = Controls.CreateModuleButton(hWnd, "Link And Forecast",  5,  423, 0, 80, 30, hFont);
		IntPtr hCashFlowButton     = Controls.CreateModuleButton(hWnd, "Cashflow",           6,  564, 0, 80, 30, hFont);
		IntPtr hValuationsButton   = Controls.CreateModuleButton(hWnd, "Valuations",         7,  705, 0, 80, 30, hFont);
		IntPtr hSubCntMngButton    = Controls.CreateModuleButton(hWnd, "Subcontract Manager",8,  846, 0, 80, 30, hFont);
		IntPtr hCostAllowableButton= Controls.CreateModuleButton(hWnd, "Cost And Allowables",9,  987, 0, 80, 30, hFont);
		IntPtr hMaterialsReceivedButton = Controls.CreateModuleButton(hWnd, "Materials Received", 10, 1128, 0, 80, 30, hFont);
		IntPtr hDrawingsButton = Controls.CreateModuleButton(hWnd, "Drawings",           11, 1269, 0, 80, 30, hFont);
		// Run Message loop
		MSG msg;
		while(User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			User32.TranslateMessage(ref msg);
			User32.DispatchMessage(ref msg);
		}
	}
}