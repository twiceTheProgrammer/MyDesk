using System;
using Win32.Interop;

public static class Controls
{
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

    public static IntPtr CreateButton(IntPtr hWnd, int id, string text, int x, int y, int width, int height)
    {
        return User32.CreateWindowEx(
            0,
            "BUTTON",
            text,
            (uint) (WindowStyles.Child | WindowStyles.Visible | WindowStyles.DefPushButton | WindowStyles.Flat),
            x, y, width, height,
            hWnd,
            (IntPtr)id,
            IntPtr.Zero,
            IntPtr.Zero
        );
    }
}
