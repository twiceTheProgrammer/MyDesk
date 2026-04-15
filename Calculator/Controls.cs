public static class Controls
{

	public static IntPtr CreateEditBox(IntPtr hWnd, int id, int x, int y, int width, int height)
	{
		return Win32.CreateWindowEx(
			0, 
			"EDIT", "", 
			Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_BORDER,
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero 
		);
	}

	public static IntPtr CreateLabel(IntPtr hWnd, int id, string text, int x, int y, int width, int height)
	{
		return Win32.CreateWindowEx(
			0,
			"STATIC",
			text,
			Win32.WS_CHILD | Win32.WS_VISIBLE,
			50, 100, 200, 25,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);
	}
}