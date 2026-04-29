using System;
using System.Runtime.InteropServices;
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
			(uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.Flat | WindowStyles.Center),
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

	public static IntPtr CreateTooltip(IntPtr hWndParent)
	{
		return User32.CreateWindowEx(
			0x00000008,
			"Tooltips_class32",
			"",
			(uint) WindowStyles.Popup,
			0, 0, 0, 0,
			hWndParent,
			IntPtr.Zero,
			Kernel32.GetModuleHandle(null),
			IntPtr.Zero
		);
	}

	public static void AttachTooltip(IntPtr hTooltip, IntPtr hControl, IntPtr hWndParent, string text)
	{
		TOOLINFO ti = new TOOLINFO();
		ti.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
		ti.uFlags = (int) (TooltipFlags.IdIsHwnd | TooltipFlags.SubClass);
		ti.hwnd = hWndParent;
		ti.uId = hControl;
		ti.lpszText = text;

		IntPtr pToolInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TOOLINFO)));
		Marshal.StructureToPtr(ti, pToolInfo, false);

		User32.SendMessage(hTooltip, (uint)TooltipMsg.AddTool, IntPtr.Zero, pToolInfo);

		Marshal.FreeHGlobal(pToolInfo);
	}
}
