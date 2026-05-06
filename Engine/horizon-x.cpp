#include <windows.h>
#include <limits>
#include "calculator.h"
#include "construction\estimation.h"

#define API __declspec(dllexport)
#define VERSION 1

extern "C" {
	struct HorizonAPI
	{
		unsigned int version;
		unsigned int size;
		double (*Add)(double a, double b);
		double (*Subtract)(double a, double b);
		double (*Multiply)(double a, double b);
		double (*Divide) (double a, double b);
		int (*EstimateBricks)(int cementBags);
	};

	// Return API table.
	API const HorizonAPI* IHorizonAPI()
	{
		static HorizonAPI api = {
			VERSION,
			sizeof(HorizonAPI),
			&Add,
			&Subtract,
			&Multiply,
			&Divide,
			&EstimateBricks
		};
	
		return &api;
	}
}