#include <windows.h>
#include <limits>

#define API __declspec(dllexport)
#define VERSION 1

extern "C" {
	struct CalculatorAPI 
	{
		unsigned int version;
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
			VERSION,
			&Add,
			&Subtract,
			&Multiply,
			&Divide
		};

		return &api;
	}
}