using System;

namespace Win32.Interop
{
	[Flags]
	public enum WindowStyles : uint
	{
		Child = 0x40000000,
		Visible = 0x10000000,
		Border = 0x00800000,
		Overlapped = 0x00000000,
		Popup = 0x80000000,
		Minimize = 0x20000000,
		ClipSiblings = 0x04000000,
		ClipChildren = 0x02000000,
		Maximize = 0x01000000,
		Caption = 0x00C00000,
		SysMenu = 0x00080000,
		ThickFrame = 0x00040000,
		MinimizeBox = 0x00020000,
		MaximizeBox = 0x00010000,
		OverlappedWindow = Overlapped | Caption | ThickFrame | MinimizeBox | MaximizeBox | SysMenu,

		DefPushButton = 0x00000001,
		Flat = 0x00008000,
		Center = 0x00000300,

		StaticLeft = 0x00000000,
		StaticCenter = 0x00000001

	}

	public enum ShowWindowCommands
	{
		Normal = 1,
		Maximized = 3
	}
	
	public enum BackgroundMode
	{
		Transparent = 1,
		Opaque = 2
	}
	public enum VirtualKey
	{
		Return = 0x0D
	}
}