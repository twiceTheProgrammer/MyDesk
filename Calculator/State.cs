
namespace Calculator.API
{
	public class State
	{
		public string CurrentInput { get; set; } = "";
		public double LeftOperand { get; set; } = 0;
		public string? PendingOperator { get; set; } = null;
		public void Reset()
		{
			CurrentInput = "";
			LeftOperand = 0;
			PendingOperator = null;
		}

	}

}