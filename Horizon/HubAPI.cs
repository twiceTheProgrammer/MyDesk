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

		public SciterValue EstimateBricksFor(SciterValue[] args)
		{
			int bagsOfCement;
			int.TryParse(args[0].Get(""), out bagsOfCement);

			var res = new
			{
				total = _api.EstimateBricks(bagsOfCement)
			};

			return SciterValue.FromObject(res);
		}

		public SciterValue SandRequired(SciterValue[] args)
		{
			int bagsOfCement;
			int.TryParse(args[0].Get(""), out bagsOfCement);

			var res = new
			{
				total = _api.SandRequired(bagsOfCement)
			};

			return SciterValue.FromObject(res);
		}
	}
}