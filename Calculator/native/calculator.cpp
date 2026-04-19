#include <windows.h>

#define API __declspec(dllexport)

extern "C" {
	API int Add(int a, int b)      { return a + b; }
	API int Subtract(int a, int b) { return a - b; }
	API int Multiply(int a, int b) { return a * b; }
	API double Divide(int a, int b) { return b == 0 ? 0.0 : static_cast<double>(a) / b;}
}