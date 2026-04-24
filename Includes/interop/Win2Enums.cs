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
		StaticCenter = 0x00000001,

		// list box
		Notify = 0x0001
	}

	public enum ShowWindowCommands
	{
		Normal = 1,
		Maximized = 3
	}
	
	[Flags]
	public enum SetWindowPosFlags : uint
	{
		NoSize = 0x0001,
		NoMove = 0x0002,
		NoZOrder = 0x0004,
		ShowWindow = 0x0040
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
	[Flags]
	public enum ListBoxStyles : uint
	{
		Notify = 0x0001,
		Sort = 0x0002,
		NoRedraw = 0x0004,
		MultiSelect = 0x0008,
		OwnerDrawVariable = 0x0020,
		HasStrings = 0x0040,
		UseTabStops = 0x0080,
		NoIntegrationHeight = 0x0100,
		Multicolumn = 0x0200,
		WantKeyboardInput = 0x0400,
		NoData = 0x2000
	}
	public enum FontWeight : uint
	{
		Normal = 400,
		Bold = 700
	}
	public enum FontCharSet : uint
	{
		Default = 1,
		Symbol = 2,
		ShiftJIS = 128,
		Hangeul = 129,
		GB2312 = 134,
		ChineseBig5 = 136,
		Greek = 161,
		Turkish = 162,
		Hebrew = 177,
		Arabic = 178,
		Baltic = 186,
		Russian = 204,
		Thai = 222,
		EastEurope = 238,
		OEM = 255
	}
	public enum OutputPrecision : uint
	{
		Default = 0,
		String = 1,
		Character = 2,
		Stroke = 3,
		TT = 4, 
		Device = 5,
		Raster = 6,
		TTOnly = 7,
		Outline = 8,
		ScreenOutline = 9,
		PSOnly = 10
	}
	public enum ClipPrecision : uint
	{
		Default = 0,
		Character = 1,
		Stroke = 2,
		Mask = 15
	}
	public enum Quality : uint
	{
		Default = 0,
		Draft = 1,
		Proof = 2,
		NonAntialiased = 3,
		Antialiased = 4,
		ClearType = 5
	}

	[Flags]
	public enum PitchAndFamily : uint
	{
		DefaultPitch = 0,
		FixedPitch = 1,
		VariablePitch = 2,

		// Families 
		DontCare = 0 << 4,
		Roman = 1 << 4,
		Swiss = 2 << 4,
		Modern = 3 << 4,
		Script = 4 << 4,
		Decorative = 5 << 4
	}

	// cursor 
	public enum IDC : int
	{
		Arrow = 32512,
		IBeam = 32513, 
		Wait = 32514,
		Cross = 32515,
		UpArrow = 32516,
		SizeNWSE = 32642,
		SizeNESW = 32643,
		SizeWE = 32644,
		SizeNS = 32645,
		SizeAll = 32646,
		No = 32648,
		Hand = 32649,
		Help = 32651,
	}
}