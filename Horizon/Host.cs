using SciterSharp;
using SciterSharp.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Horizon.API;
using Horizon.Interop;

namespace TestCore
{
	class Host : BaseHost
	{
	}

	class HostEvh : SciterEventHandler
	{
		//|
		//| Native API (script handler) - functions that handle calls/events from UI.
		//|
		private readonly NativeAPI _api = new NativeAPI();
		public SciterValue Host_HelloWorld() => _api.Host_HelloWorld();
		public SciterValue AddNumbers(SciterValue[] args) => _api.AddNumbers(args);
	}

	class BaseHost : SciterHost
	{
		protected static SciterX.ISciterAPI _api = SciterX.API;
		protected SciterArchive _archive = new SciterArchive();
		protected SciterWindow _wnd;

		public BaseHost()
		{
		#if !DEBUG
			_archive.Open(SciterAppResource.ArchiveResource.resources);
		#endif
		}

		public void Setup(SciterWindow wnd)
		{
			_wnd = wnd;
			SetupWindow(wnd);
		}

		public void SetupPage(string page_from_res_folder)
		{
		#if DEBUG
			string path = Path.GetFullPath(Environment.CurrentDirectory + "/../../../res/" + page_from_res_folder);
			Debug.Assert(File.Exists(path));
            path = path.Replace('\\', '/');

			string url = "file://" + path;
		#else
			string url = "archive://app/" + page_from_res_folder;
		#endif

			bool res = _wnd.LoadPage(url);
			Debug.Assert(res);
		}

		protected override SciterXDef.LoadResult OnLoadData(SciterXDef.SCN_LOAD_DATA sld)
		{
			if(sld.uri.StartsWith("archive://app/"))
			{
				// load resource from SciterArchive
				string path = sld.uri.Substring(14);
				byte[] data = _archive.Get(path);
				if(data!=null)
					_api.SciterDataReady(_wnd._hwnd, sld.uri, data, (uint) data.Length);
			}
			return base.OnLoadData(sld);
		}
	}
}