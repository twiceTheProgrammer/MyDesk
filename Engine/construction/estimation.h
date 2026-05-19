#pragma once
struct BrickDimensios{
	double length = 0.30; // meters
	double width = 0.12; // meters
	double height = 0.18; // meters
} Brick;

struct SepticTankMaterials {
	int bricks;
	int sand;
	int cementBags;
	int dampkos;   // DPC sheets
	int brickForce;
	int lintels;
};

class Estimate 
{
	private: 
		int const BRICKS_PER_BAG = 50;      // production ratio
		int const WHEELBARROWS_PER_BAG = 5; // let's make our ratio 1 bag for every 4 wheel barrows of sand.

	public:
		int BricksProduced(int cements);
		int SandRequired(int bagsOfCement);
		int BricksForWall(int length, int width, int height);
		int CementRequiredForWall(int bricks);  // mortar requirement
		SepticTankMaterials SepticTankEstimate(double length, double width, double height);
};

Estimate estimate;

int EstimateBricks(int cementBags) { return estimate.BricksProduced(cementBags); }
int SandRequired(int cement) { return estimate.SandRequired(cement); }
int CementRequiredForWall(int bricks) { return estimate.CementRequiredForWall(bricks); }
int BricksForWall(int length, int width, int height) { return estimate.BricksForWall(length, width, height); }
SepticTankMaterials EstimateSepticTank(int length, int width, int height) { 
	return estimate.SepticTankEstimate(length, width, height);
}

// Defining Estimating API functions
int Estimate::BricksProduced(int cements){
	int bricks = cements * BRICKS_PER_BAG;
	return bricks;
}

int Estimate::SandRequired(int cement)
{
	return cement * WHEELBARROWS_PER_BAG;
}

int Estimate::BricksForWall(int length, int width,int height)
{
	double wallArea = 2  * ((length * height) + (width * height));
	double brickArea = Brick.length * Brick.height;

	int estimatedBricks = static_cast<int>((wallArea / brickArea));
	return estimatedBricks;
}

int Estimate::CementRequiredForWall(int bricks) {
	return (bricks + 25 - 1) / 25;   // ~25 blocks per bag of cement
}

SepticTankMaterials Estimate::SepticTankEstimate(double length, double width, double height)
{
	SepticTankMaterials result {};

	result.bricks = BricksForWall(length, width, height);

	// ~10% wastage
	result.bricks = static_cast<int>(result.bricks * 1.10);
	result.sand = SandRequired(result.bricks);
	result.cementBags = CementRequiredForWall(result.bricks);
	result.dampkos = 2;
	result.brickForce = 5;
	result.lintels = 4;

	return result;
}