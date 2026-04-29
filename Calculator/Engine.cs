namespace Calculator.API
{
	public class Engine
	{
		private static readonly CalculatorAPI api = Native.Load();
		private readonly State _state;

		public Engine(State state)
		{
			_state = state;
		}
		public void AppendDigit(string digit)
		{
			_state.CurrentInput += digit;
		}
		public void AppendDecimal()
		{
			if (!_state.CurrentInput.Contains("."))
			{
				_state.CurrentInput = _state.CurrentInput.Length == 0 ? "0." : _state.CurrentInput + ".";
			}
		}
		public void SetOperator(string op)
		{
			if (double.TryParse(_state.CurrentInput, out double value))
			{
				_state.LeftOperand = value;
				_state.PendingOperator = op;
				_state.CurrentInput = "";
			}
		}

		public string Evaluate(double rightOperand)
		{
			double result = 0;
			switch(_state.PendingOperator)
			{
				case "+": result = api.Add(_state.LeftOperand, rightOperand); break;
				case "-": result = api.Subtract(_state.LeftOperand, rightOperand); break;
				case "*": result = api.Multiply(_state.LeftOperand, rightOperand); break;
				case "/": result = api.Divide(_state.LeftOperand, rightOperand); break;
			}

			// Handle Number / 0. Show a friendly message.
			if (double.IsNaN(result))
			{
				return $"{_state.LeftOperand} {_state.PendingOperator} {rightOperand} = Undefined"; // format nicely
			} 

			string formatedResult = $"{_state.LeftOperand} {_state.PendingOperator} {rightOperand} = {result.ToString("0.##")}";
			_state.Reset();
			_state.LeftOperand = result; // allow chaining.
			return formatedResult;
		}
	}
}