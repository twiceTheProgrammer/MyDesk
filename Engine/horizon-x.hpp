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
	int HUBFN(EstimateBricks) (int cementBags);
	int HUBFN(SandRequired) (int cementBags);
	int HUBFN(CementRequiredForWall) (int bricks);
	int HUBFN(BricksForWall) (int length, int height, int width);
};

extern "C" API const HorizonAPI* IHorizonAPI();

int Estimate::BricksProduced(int cements){
	int bricks = cements * BRICKS_PER_BAG;
	return bricks;
}

int Estimate::SandRequired(int cement)
{
	return cement * WHEELBARROWS_PER_BAG;
}

int Estimate::BricksForWall(int length, int width, int height)
{
	double wallArea = length * height;
	double brickArea = Brick.length * Brick.height;

	int estimatedBricks = static_cast<int>(wallArea / brickArea);
	return estimatedBricks;
}

int Estimate::CementRequiredForWall(int bricks) {
	return (bricks + 25 - 1) / 25;   // ~25 blocks per bag of cement
}
