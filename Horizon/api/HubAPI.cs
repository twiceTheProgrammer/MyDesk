using SciterSharp;
using Horizon.Interop;

namespace Horizon.API
{

	public class NativeAPI : SciterEventHandler
	{
		private readonly HorizonAPI _api = Native.Load();

		public SciterValue Host_HelloWorld()
		{
			return new SciterValue("Hello World from native side!");
		}

		public SciterValue Add(SciterValue[] args)
		{
			return SciterValue.FromObject(new {});
		}
	}
}