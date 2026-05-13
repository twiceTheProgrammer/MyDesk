#include <iostream>
#include "estimation.h"

using namespace std;

Estimate::Estimate() {}

int Estimate::NumberOfBricks(int cements){
	int bricks = cements * BRICKS_PER_BAG;
	return bricks;
}

int Estimate::SandRequired(int cement)
{
	return cement * WHEELBARROWS_PER_BAG;
}

int Estimate::BricksForWall(double length, double width, double height)
{
	double wallArea = 2 * ((length * height) + (width * height));  // perimeter walls
	return static_cast<int>(wallArea * BRICKS_PER_M2 );
}

int main()
{
	int bagsOfCement;
	int sand;
	int length, width, height;

	cout << "===| HORIZON HUB ESTIMATION |===\n" << endl;
	
	cout << "Enter septic tank length (m) : " << endl;
	cin >> length;

	cout << "Enter septic tank width (m) : " << endl;
	cin >> width;

	cout << "Enter septic tank height (m) : " << endl;
	cin >> height;

	cout << "Enter cement bags : " << endl;
	cin >> bagsOfCement;

	int bricks = estimate.BricksForWall(length, width, height);
	
	cout << "\n--- Estimation Report -----\n";
	cout << "Wall Area : " << 2 * (length * height + width * height) << " m2"  << endl;
	cout << "Bricks required : " << bricks << endl;
	cout << "Sand Required : " << estimate.SandRequired(bagsOfCement) << " Wheel barrows"<<endl;

	return 0;
}
