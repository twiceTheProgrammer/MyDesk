using SciterSharp;
using SciterSharp.Interop;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Drawing;

namespace TestCore
{
	class Program
	{
		class SciterMessages : SciterDebugOutputHandler
		{
			protected override void OnOutput(SciterXDef.OUTPUT_SUBSYTEM subsystem, SciterXDef.OUTPUT_SEVERITY severity, string text)
			{
				MessageBox.Show(IntPtr.Zero, text, "CandyX UI Debug");
				Debug.Write(text);// so I can see Debug output even if 'native debugging' is off
			}
		}

		public static SciterWindow AppWnd;
		public static Host AppHost;
		private static SciterMessages sm = new SciterMessages();

		[STAThread]
		static void Main(string[] args)
		{

			Debug.WriteLine("Sciter: " + SciterX.Version);
			Debug.WriteLine("Bitness: " + IntPtr.Size);
			// SciterX.API.SciterSetOption(IntPtr.Zero, SciterXDef.SCITER_SET_DEBUG_MODE);

			// MessageBox.Show(IntPtr.Zero, SciterX.Version, "Debug");
			// Sciter needs this for drag'n'drop support; STAThread is required for OleInitialize succeess
			int oleres = PInvokeWindows.OleInitialize(IntPtr.Zero);
			Debug.Assert(oleres == 0);
			
			// Create the window
			AppWnd = new SciterWindow();

			var wnd = AppWnd;

			wnd.CreateMainWindow(1600, 900);
			wnd.CenterTopLevelWindow();

			// Prepares SciterHost and then load the page
			AppHost = new Host();
			var host = AppHost;
			host.Setup(wnd);
			host.AttachEvh(new HostEvh());
			host.SetupPage("main.htm");

			// Show window and Run message loop
			wnd.Show();
			PInvokeUtils.RunMsgLoop();

		}
	}
}