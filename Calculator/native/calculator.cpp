#include <windows.h>
#include <limits>

#define API __declspec(dllexport)

extern "C" {
	struct CalculatorAPI 
	{
		double (*Add)(double a, double b);
		double (*Subtract)(double a, double b);
		double (*Multiply)(double a, double b);
		double (*Divide)(double a, double b);
	};

	static double Add(double a, double b)      { return a + b; }
	static double Subtract(double a, double b) { return a - b; }
	static double Multiply(double a, double b) { return a * b; }
	static double Divide(double a, double b)   { return b == 0 ? std::numeric_limits<double>::quiet_NaN() :  a / b ; }

	// Return API table.
	API const CalculatorAPI* ICalculatorAPI()
	{
		static CalculatorAPI api = {
			&Add,
			&Subtract,
			&Multiply,
			&Divide
		};

		return &api;
	}
}