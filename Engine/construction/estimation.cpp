#include <iostream>
#include "estimation.h"

using namespace std;

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

int main()
{
	BricksEstimate est = BricksEstimate();

	cout << "Total bricks : " << est.NumberOfBricks(20) << endl;
	return 0;
}
