#include <iostream>
#include "estimation.h"

using namespace std;

Estimate::Estimate() {}

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
	double wallArea = 2 * (length * height + width * height);  // perimeter walls
	double brickFaceArea = BRICK_LENGTH * BRICK_HEIGHT;
	return static_cast<int>(wallArea / brickFaceArea );
}

int Estimate::CementRequiredForWall(int bricks) {
	return (bricks + 25 - 1) / 25;   // ~25 blocks per bag of cement
}

int main()
{
	Estimate estimate;
	int bagsOfCement;
	int sand;
	int length, width, height;

	cout << "===| HORIZON HUB ESTIMATION |===\n" << endl;
	
	cout << "Septic tank dimensions" << endl;
	cout << "Enter length (m) : " << endl;
	cin >> length;

	cout << "Enter width (m) : " << endl;
	cin >> width;

	cout << "Enter height (m) : " << endl;
	cin >> height;

	
	cout << "\nBricks Production" << endl;
	cout << "Enter cement bags : " << endl;
	cin >> bagsOfCement;

	// Bricks Production.
	int producedBricks = estimate.BricksProduced(bagsOfCement);
	int sandForProduction = estimate.SandRequired(bagsOfCement);
	int bricksForWall = estimate.BricksForWall(length, width, height);
	int cementForWall = estimate.CementRequiredForWall(bricksForWall);

	cout << "\n--- Estimation Report -----\n";
	cout << "Brick Production:\n";
	cout << " Bricks produced : " << producedBricks << endl;
	cout << " Sand Required : " << sandForProduction << " Wheel barrows"<<endl;

	cout << "\nWall construction : " << endl;
	cout << " Bricks Needed : " << bricksForWall << endl;
	cout << "Cement Required : " <<  cementForWall << endl;

	return 0;
}
