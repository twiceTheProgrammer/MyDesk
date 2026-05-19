#pragma once
#include "horizon-x-primitives.h"
#include "construction/estimation.h"
#include <windows.h>
#include <limits>
#include <cmath>

#define VERSION 1

struct HorizonAPI
{
	unsigned int version;
	unsigned int size;
	int HUBFN(EstimateBricks) (int cementBags);
	int HUBFN(SandRequired) (int cementBags);
	int HUBFN(CementRequiredForWall) (int bricks);
	int HUBFN(BricksForWall) (int length, int width, int height);
};

extern "C" API const HorizonAPI* IHorizonAPI();