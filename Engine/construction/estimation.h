#pragma once

struct {
	double length;
	double height;
	double width;
} brick;

class Estimate 
{
	private: 
		int const BRICKS_PER_BAG = 50;      // production ratio
		double const BRICK_LENGTH = 0.30;   // meters
		double const BRICK_HEIGHT = 0.18;   // meters
		double const BRICK_WIDTH = 0.12;    // meters
		int const WHEELBARROWS_PER_BAG = 5; // let's make our ratio 1 bag for every 4 wheel barrows of sand.

	public:
		// Estimate(double length, double width, double height);
		int BricksProduced(int cements);
		int SandRequired(int bagsOfCement);
		int BricksForWall(int length, int height, int width);
		int CementRequiredForWall(int bricks);  // mortar requirement
};

Estimate estimate;

int EstimateBricks(int cementBags) { return estimate.BricksProduced(cementBags); }
int SandRequired(int cement) { return estimate.SandRequired(cement); }
int CementRequiredForWall(int bricks) { return estimate.CementRequiredForWall(bricks); }
int BricksForWall(int length, int height, int width) { return estimate.BricksForWall(length, height, width); }
