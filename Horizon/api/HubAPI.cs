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
				res = _api.EstimateBricks(25)
			};

			return SciterValue.FromObject(res); 
		}

		public SciterValue EstimateBricksFor(SciterValue[] args)
		{
			int bagsOfCement = args[0].Get(0);

			var res = new
			{
				total = _api.EstimateBricks(bagsOfCement)
			};

			return SciterValue.FromObject(res);
		}
	}
}