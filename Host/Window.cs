using System;
using Win32.Interop;

public class MainWindow
{
	public IntPtr Handle { get; private set; }
	public MainWindow(string className, string title, IntPtr hInstance)
	{
		Handle = User32.CreateWindowEx(
			0,
			className,
			title,
			(uint) (WindowStyles.OverlappedWindow | WindowStyles.Caption),
			100, 100, 1000, 900,
			IntPtr.Zero,
			IntPtr.Zero,
			hInstance,
			IntPtr.Zero
		);
	}

	public void Show()
	{
		User32.ShowWindow(Handle, (int)ShowWindowCommands.Maximized);
		User32.UpdateWindow(Handle);
	}
}