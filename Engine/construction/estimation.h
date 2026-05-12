#pragma once

class BricksEstimate 
{
	private: 
		int const ESTIMATE_BRICKS = 50;
	
	public:
		BricksEstimate();
		int NumberOfBricks(int cements);
};

BricksEstimate::BricksEstimate() {}

int BricksEstimate::NumberOfBricks(int cements){
	int bricks = cements * ESTIMATE_BRICKS;
	return bricks;
}

int EstimateBricks(int cementBags)
{
	BricksEstimate estimate;
	return estimate.NumberOfBricks(cementBags);
}