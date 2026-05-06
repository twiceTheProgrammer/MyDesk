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

		public SciterValue AddNumbers(SciterValue[] args)
		{
			var res = new
			{
				res = _api.Add(1.5, 3.5)
			};

			return SciterValue.FromObject(res); 
		}
	}
}