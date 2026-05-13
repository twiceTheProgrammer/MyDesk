#pragma once
#include "horizon-x-primitives.h"
#include "construction/estimation.h"
#include <windows.h>
#include <limits>

#define VERSION 1

struct HorizonAPI
{
	unsigned int version;
	unsigned int size;
	int HUBFN(EstimateBricks)(int cementBags);
};

extern "C" API const HorizonAPI* IHorizonAPI();