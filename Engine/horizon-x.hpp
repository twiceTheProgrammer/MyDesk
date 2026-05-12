#pragma once

#include "calculator.h"
#include "construction/estimation.h"
#include <windows.h>
#include <limits>

#define API __declspec(dllexport)
#define VERSION 1

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

extern "C" API const HorizonAPI* IHorizonAPI();