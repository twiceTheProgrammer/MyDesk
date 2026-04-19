#include <windows.h>
#include <limits>

#define API __declspec(dllexport)

extern "C" {
	API double Add(double a, double b)      { return a + b; }
	API double Subtract(double a, double b) { return a - b; }
	API double Multiply(double a, double b) { return a * b; }
	API double Divide(double a, double b)   { return b == 0 ? std::numeric_limits<double>::quiet_NaN() :  a / b ; }
}