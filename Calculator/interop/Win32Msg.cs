using System;

namespace Win32.Interop
{
	public enum WindowMsg : uint
	{
		Command = 0x0111,
		Close = 0x0010,
		CtlColorStatic = 0x0138,
		KeyDown = 0x0100,
		KeyUp = 0x0101,
		Char = 0x0102,
		CtlColorBtn = 0x0135,
		SetFont = 0x0030,
		EraseBackground = 0x0014
	}
}