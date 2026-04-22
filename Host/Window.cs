using System;
using static Win32;

public class MainWindow
{
	public IntPtr Handle { get; private set; }
	public MainWindow(string className, string title, IntPtr hInstance)
	{
		Handle = CreateWindowEx(
			0,
			className,
			title,
			WS_OVERLAPPEDWINDOW | WS_CAPTION,
			100, 100, 1000, 900,
			IntPtr.Zero,
			IntPtr.Zero,
			hInstance,
			IntPtr.Zero
		);
	}

	public void Show()
	{
		ShowWindow(Handle, SW_SHOWMAXIMIZED);
		UpdateWindow(Handle);
	}
}