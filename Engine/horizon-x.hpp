#pragma once

#include "construction/estimation.h"
#include <windows.h>
#include <limits>

#define API __declspec(dllexport)
#define VERSION 1

struct HorizonAPI
{
	unsigned int version;
	unsigned int size;
	int (*EstimateBricks)(int cementBags);
};

extern "C" API const HorizonAPI* IHorizonAPI();