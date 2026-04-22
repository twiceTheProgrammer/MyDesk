using System;
using static Win32;

public static class Controls
{

	public static IntPtr CreateModuleButton(IntPtr hWnd ,string text, int id,int x, int y, int width, int height, IntPtr hFont)
	{	
		IntPtr hBtn = CreateWindowEx(
			0, "BUTTON", text,
			WS_CHILD | WS_VISIBLE | BS_FLAT | BS_CENTER,
			x, y, 140, 30,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);

		SendMessage(hBtn, WM_SETFONT, hFont, (IntPtr)1);
		return hBtn;
	}

	public static IntPtr CreateToolbarButton(IntPtr hWnd, string text, int id, int x, int y, int width, int height, IntPtr hFont)
	{
		IntPtr hBtn = CreateWindowEx(
			0, "BUTTON", text,
			WS_CHILD | WS_VISIBLE | BS_FLAT,
			x, y, 115, 20,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);

		SendMessage(hBtn, WM_SETFONT, hFont, (IntPtr)1);
		return hBtn;
	}

	public static IntPtr FPTR_CreateFont()
	{
		return CreateFont(
			18, 0, 0, 0, FW_NORMAL,
			0, 0, 0, DEFAULT_CHARSET,
			OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS,
			DEFAULT_QUALITY, DEFAULT_PITCH | FF_SWISS,
			"Segoe UI"
		);
	}
}