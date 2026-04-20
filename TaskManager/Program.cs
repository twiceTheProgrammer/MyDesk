using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using static Win32;

class Program
{
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch(msg)
		{
			case 0x0111:   // WM_COMMAND 
			{
				int controlId = wParam.ToInt32() & 0xffff;
				switch(controlId)
				{
					case 2:   // Add button 
						break;
					case 3:   // Delete button 
						break;
				}
				break;
			}
			case 0x0010:  // WM_CLOSE 
			{
				Environment.Exit(0);
				break;	
			}
		}
		return DefWindowProc(hWnd, msg, wParam, lParam);
	}
	static void Main()
	{

		string className = "TaskManager";
		// Register Main window class
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
			"Task Manager",
			WS_OVERLAPPEDWINDOW,
			100, 100, 800, 800,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hTitle = CreateWindowEx(
			0, "STATIC", "Task Management",
			WS_CHILD | WS_VISIBLE | SS_CENTER,
			20, 0, 340, 20,
			hWnd, 
			(IntPtr)5,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hTaskInput = CreateWindowEx(
			0, "EDIT", "",
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
			20, 25, 250, 25,
			hWnd,
			(IntPtr)1, // control id 
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hAddTaskButton = CreateWindowEx(
			0, "BUTTON", "Add",
			WS_CHILD | WS_VISIBLE,
			280, 25, 80, 25,
			hWnd,
			(IntPtr)2,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hTaskList = CreateWindowEx(
			0, "LISTBOX", "",
			WS_CHILD | WS_VISIBLE | WS_BORDER | LBS_NOTIFY,
			20, 60, 340, 200,
			hWnd,
			(IntPtr)4,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hDeleteButton = CreateWindowEx(
			0,
			"BUTTON", "Delete",
			WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
			20, 270, 340, 30,
			hWnd,
			(IntPtr)3,
			IntPtr.Zero,
			IntPtr.Zero
		);

		ShowWindow(hWnd, 1);
		UpdateWindow(hWnd);

		// Message loop
		MSG msg;
		while(GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			TranslateMessage(ref msg);
			DispatchMessage(ref msg);
		}
	}
}