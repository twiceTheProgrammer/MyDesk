#pragma once

class Estimate 
{
	private: 
		int const BRICKS_PER_BAG = 50;  // production ratio
		double const BRICK_LENGTH = 0.39;   // meters
		double const BRICK_HEIGHT = 0.19;   // meters
		int const WHEELBARROWS_PER_BAG = 4; // let's make our ratio 1 bag for every 4 wheel barrows of sand.

	public:
		Estimate();
		int BricksProduced(int cements);
		int SandRequired(int bagsOfCement);
		int BricksForWall(int length, int height, int width);
		int CementRequiredForWall(int bricks);  // mortar requirement
};


Estimate estimate;
int EstimateBricks(int cementBags) { return estimate.BricksProduced(cementBags); }
int EstimateSand(int cement) { return estimate.SandRequired(cement); }