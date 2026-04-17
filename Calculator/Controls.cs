using static Win32;
public static class Controls
{
	public static IntPtr CreateEditBox(IntPtr hWnd, int id, int x, int y, int width, int height)
	{
		return CreateWindowEx(
			0, 
			"EDIT", "", 
			WS_CHILD | WS_VISIBLE | WS_BORDER,
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero 
		);
	}
	public static IntPtr CreateLabel(IntPtr hWnd, int id, string text, int x, int y, int width, int height)
	{
		return CreateWindowEx(
			0,
			"STATIC",
			text,
			WS_CHILD | WS_VISIBLE,
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);
	}

	public static IntPtr CreateButton(IntPtr hWnd, int id, string text, int x, int y, int width, int height)
	{
		return CreateWindowEx(
			0,
			"BUTTON",
			text,
			WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | BS_FLAT,
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);
	}
}