using System;

namespace Win32.Interop
{
	public enum WindowMsg : uint
	{
		// Common messages
		Null = 0x0000,
		Create = 0x0001,
		Command = 0x0111,
		Close = 0x0010,
		Destroy = 0x0001,
		KeyDown = 0x0100,
		KeyUp = 0x0101,
		Char = 0x0102,
		SetFont = 0x0030,
		EraseBackground = 0x0014,

		// Text Messages
		GetText = 0x000D,
		GetTextLength = 0x000E,
		SetText = 0x000C,

		// Control color messages 
		CtlColorMsgBox = 0x0132,
		CtlColorEdit = 0x0133,
		CtlColorListBox = 0x0134,
		CtlColorBtn = 0x0135,
		CtlColorDlg = 0x0136,
		CtlColorScrollBar = 0x0137,
		CtlColorStatic = 0x0138
	}

	public enum ListBoxMsg : uint
	{
		AddString = 0x0180,
		DeleteString = 0x0182,
		GetCurSel = 0x0188,
		GetTextLen = 0x018A
	}
}