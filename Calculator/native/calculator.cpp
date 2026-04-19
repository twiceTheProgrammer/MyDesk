#include <windows.h>

#define API __declspec(dllexport)

extern "C" {
	API double Add(double a, double b)      { return a + b; }
	API double Subtract(double a, double b) { return a - b; }
	API double Multiply(double a, double b) { return a * b; }
	API double Divide(double a, double b) { return b == 0 ? 0 : static_cast<double>(a) / b;}
}