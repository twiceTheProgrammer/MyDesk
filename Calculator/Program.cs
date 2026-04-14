using System;
using System.Runtime.InteropServices;
using static Win32;

class Program
{
    // Delegate for WndProc
    public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    // Our WndProc implementation
    public static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case 0x0111: // WM_COMMAND
                int controlId = wParam.ToInt32() & 0xFFFF;
                if (controlId == 1) // button ID
                {
                    MessageBox(hWnd, "Button clicked!", "Info", 0);
                }
                break;

            case 0x0010: // WM_CLOSE
                Environment.Exit(0);
                break;
        }
        return DefWindowProc(hWnd, msg, wParam, lParam);
    }

    static void Main()
    {
        string className = "Calculator";

        // Register window class
        WNDCLASS wc = new WNDCLASS
        {
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new WndProcDelegate(WndProc)),
            lpszClassName = className
        };
        RegisterClass(ref wc);

        // Create main window
        IntPtr hWnd = CreateWindowEx(
            0,
            className,
            "Calculator",
            0xCF0000, // WS_OVERLAPPEDWINDOW
            100, 100, 400, 300,
            IntPtr.Zero,
            IntPtr.Zero,
            IntPtr.Zero,
            IntPtr.Zero);

        // Add a button
        IntPtr hButton = CreateWindowEx(
            0,
            "BUTTON",
            "Click Me",
            0x50010000, // WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON
            50, 50, 100, 30,
            hWnd,
            (IntPtr)1,
            IntPtr.Zero,
            IntPtr.Zero);

        ShowWindow(hWnd, 1);
        UpdateWindow(hWnd);

        // Message loop
        MSG msg;
        while (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
        {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }
    }
}
