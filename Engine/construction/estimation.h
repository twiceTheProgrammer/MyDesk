#pragma once

class Estimate 
{
	private: 
		int const BRICKS_PER_BAG = 50;
		int const BRICKS_PER_M2 = 50;   // ~50 brick per square meter
		int const WHEELBARROWS_PER_BAG = 4; // let's make our ratio 1 bag for every 4 wheel barrows of sand.

	public:
		Estimate();
		int NumberOfBricks(int cements);
		int SandRequired(int bagsOfCement);
		int BricksForWall(double length, double height, double width);
};


Estimate estimate;
int EstimateBricks(int cementBags) { return estimate.NumberOfBricks(cementBags); }
int EstimateSand(int cement) { return estimate.SandRequired(cement); }