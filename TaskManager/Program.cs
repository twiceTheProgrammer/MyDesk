using System;
using System.Runtime.InteropServices;
using Win32.Interop;

class Program
{
	static IntPtr hTaskInput;
	static IntPtr hTaskList;
	public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		switch((WindowMsg) msg)
		{
			case WindowMsg.Command: 
			{
				int controlId = wParam.ToInt32() & 0xffff;
				switch(controlId)
				{
					case 2:
					{
						// get text length
						int length = (int) User32.SendMessage(hTaskInput, (uint) WindowMsg.GetTextLength, IntPtr.Zero, IntPtr.Zero);
						
						if (length > 0)
						{
							var sb = new System.Text.StringBuilder(length + 1);
							User32.GetWindowText(hTaskInput, sb, sb.Capacity);

							User32.SendMessage(hTaskList, (uint) ListBoxMsg.AddString, IntPtr.Zero, sb.ToString());

							// clear list box after adding task.
							User32.SetWindowText(hTaskInput, "");
						}
						break;
					} 
					case 3:
					{
						int selected = (int) User32.SendMessage(hTaskList, (uint) ListBoxMsg.GetCurSel, IntPtr.Zero, IntPtr.Zero);
						
						if (selected != -1)
						{
							int res = (int) User32.SendMessage(hTaskList, (uint)ListBoxMsg.DeleteString, (IntPtr)selected, IntPtr.Zero);
						}
						break;
					}
				}
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
	static void Main()
	{

		string className = "TaskManager";
		// Register Main window class
		WNDCLASS wc = new WNDCLASS
		{
			lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
			lpszClassName = className	
		};

		User32.RegisterClass(ref wc);

		// Create main window
		IntPtr hWnd = User32.CreateWindowEx(
			0,
			className,
			"Task Manager",
			(uint) WindowStyles.OverlappedWindow,
			100, 100, 800, 800,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hTitle = User32.CreateWindowEx(
			0, "STATIC", "Task Management",
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.StaticCenter),
			20, 0, 340, 20,
			hWnd, 
			(IntPtr)5,
			IntPtr.Zero,
			IntPtr.Zero
		);

		hTaskInput = User32.CreateWindowEx(
			0, "EDIT", "",
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.Border),
			20, 25, 250, 25,
			hWnd,
			(IntPtr)1, // control id 
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hAddTaskButton = User32.CreateWindowEx(
			0, "BUTTON", "Add",
			(uint) (WindowStyles.Child | WindowStyles.Visible),
			280, 25, 80, 25,
			hWnd,
			(IntPtr)2,
			IntPtr.Zero,
			IntPtr.Zero
		);

		hTaskList = User32.CreateWindowEx(
			0, "LISTBOX", "",
			(uint) ((WindowStyles.Child | WindowStyles.Visible | WindowStyles.Border | WindowStyles.Notify)),
			20, 60, 340, 200,
			hWnd,
			(IntPtr)4,
			IntPtr.Zero,
			IntPtr.Zero
		);

		IntPtr hDeleteButton = User32.CreateWindowEx(
			0,
			"BUTTON", "Delete",
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.DefPushButton),
			20, 270, 340, 30,
			hWnd,
			(IntPtr)3,
			IntPtr.Zero,
			IntPtr.Zero
		);

		User32.ShowWindow(hWnd, 1);
		User32.UpdateWindow(hWnd);

		// Message loop
		MSG msg;
		while(User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
		{
			User32.TranslateMessage(ref msg);
			User32.DispatchMessage(ref msg);
		}
	}
}