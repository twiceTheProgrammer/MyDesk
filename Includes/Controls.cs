using System;
using Win32.Interop;

public static class Controls
{
	public static IntPtr CreateModuleButton(IntPtr hWnd ,string text, int id,int x, int y, int width, int height, IntPtr hFont)
	{	
		IntPtr hBtn = User32.CreateWindowEx(
			0, "BUTTON", text,
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.Flat | WindowStyles.Center),
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);

		User32.SendMessage(hBtn,(uint) WindowMsg.SetFont, hFont, (IntPtr)1);
		return hBtn;
	}
	public static IntPtr CreateToolbarButton(IntPtr hWnd, string text, int id, int x, int y, int width, int height, IntPtr hFont)
	{
		IntPtr hBtn = User32.CreateWindowEx(
			0, "BUTTON", text,
			(uint)(WindowStyles.Child | WindowStyles.Visible | WindowStyles.Flat),
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);

		User32.SendMessage(hBtn,(uint) WindowMsg.SetFont, hFont, (IntPtr)1);
		return hBtn;
	}
	public static IntPtr CreateEditBox(IntPtr hWnd, int id, int x, int y, int width, int height)
	{
		return User32.CreateWindowEx(
			0,
			"EDIT",
			"",
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.Border),
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);
	}
	public static IntPtr CreateLabel(IntPtr hWnd, int id, string text, int x, int y, int width, int height)
	{
		return User32.CreateWindowEx(
			0,
			"STATIC",
			text,
			(uint) ( WindowStyles.Child | WindowStyles.Visible | WindowStyles.StaticLeft),
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);
	}
	public static IntPtr CreateButton(IntPtr hWnd , int id, string text, int x, int y, int width, int height)
	{
		IntPtr hBtn = User32.CreateWindowEx(
			0, "BUTTON", text,
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.Flat | WindowStyles.Center | WindowStyles.OwnerDraw),
			x, y, width, height,
			hWnd,
			(IntPtr)id,
			IntPtr.Zero,
			IntPtr.Zero
		);

		// User32.SendMessage(hBtn,(uint) WindowMsg.SetFont, hFont, (IntPtr)1);
		return hBtn;
	}
	public static IntPtr FPTR_CreateFont()
	{
		return Gdi32.CreateFont(
			18, 0, 0, 0,
			(uint) FontWeight.Normal,
			0, 0, 0, 
			(uint) FontCharSet.Default,
			(uint) OutputPrecision.Default, 
			(uint) ClipPrecision.Default,
			(uint) Quality.Default, 
			(uint) (PitchAndFamily.DefaultPitch | PitchAndFamily.Swiss),
			"Segoe UI"
		);
	}
}
