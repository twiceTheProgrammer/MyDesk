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
		IntPtr hTooltip = Controls.CreateTooltip(hWnd);

		IntPtr hCandyButton             = Controls.CreateButton(hWnd, 1, "Candy",     0,   0, 80, 24);
		Controls.AttachTooltip(hTooltip, hCandyButton, hWnd, "Click for job manager");
		IntPtr hEstButton               = Controls.CreateModuleButton(hWnd, "Estimating",         2,  80,  0, 110, 24, hFont);
		IntPtr hPlanningButton          = Controls.CreateModuleButton(hWnd, "Planning",           3,  190, 0, 110, 24, hFont);
		IntPtr hLinkForecastButton      = Controls.CreateModuleButton(hWnd, "Link And Forecast",  5,  300, 0, 125, 24, hFont);
		IntPtr hCashFlowButton          = Controls.CreateModuleButton(hWnd, "Cashflow",           6,  425, 0, 100, 24, hFont);
		IntPtr hValuationsButton        = Controls.CreateModuleButton(hWnd, "Valuations",         7,  525, 0, 100, 24, hFont);
		IntPtr hSubCntMngButton         = Controls.CreateModuleButton(hWnd, "Subcontract Manager",8,  625, 0, 145, 24, hFont);
		IntPtr hCostAllowableButton     = Controls.CreateModuleButton(hWnd, "Cost And Allowables",9,  770, 0, 150, 24, hFont);
		IntPtr hMaterialsReceivedButton = Controls.CreateModuleButton(hWnd, "Materials Received", 10, 920, 0, 150, 24, hFont);
		IntPtr hDrawingsButton          = Controls.CreateModuleButton(hWnd, "Drawings",           11, 1070, 0,110, 24, hFont);

		// Run Message loop
		MSG msg;
		while(User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			User32.TranslateMessage(ref msg);
			User32.DispatchMessage(ref msg);
		}
	}
}