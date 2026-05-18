#pragma once

struct Brick{
	double length = 0.30; // meters
	double width = 0.12; // meters
	double height = 0.18; // meters
} Brick;

struct SepticTankMaterials {
	int bricks;
	int sand;
	int cementBags;
	int dampkos;
	int reinforcementWire;
	int lintels;
};

class Estimate 
{
	private: 
		int const BRICKS_PER_BAG = 50;      // production ratio
		int const WHEELBARROWS_PER_BAG = 5; // let's make our ratio 1 bag for every 4 wheel barrows of sand.

	public:
		// Estimate(double length, double width, double height);
		int BricksProduced(int cements);
		int SandRequired(int bagsOfCement);
		int BricksForWall(int length, int width, int height);
		int CementRequiredForWall(int bricks);  // mortar requirement
		SepticTankMaterials SepticTankEstimate(int length, int width, int height) {
			SepticTankMaterials result{};

			result.bricks = BricksForWall(length, width, height);
			result.sand = SandRequired(result.bricks);
			result.cementBags = CementRequiredForWall(result.bricks);
			result.dampkos = 1;
			result.reinforcementWire = 5;
			return result;
		}
};

Estimate estimate;

int EstimateBricks(int cementBags) { return estimate.BricksProduced(cementBags); }
int SandRequired(int cement) { return estimate.SandRequired(cement); }
int CementRequiredForWall(int bricks) { return estimate.CementRequiredForWall(bricks); }
int BricksForWall(int length, int height, int width) { return estimate.BricksForWall(length, height, width); }
SepticTankMaterials EstimateSepticTank(int length, int width, int height) { 
	return estimate.SepticTankEstimate(length, width, height);
}