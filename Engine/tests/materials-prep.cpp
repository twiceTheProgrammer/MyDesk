#include <iostream>
#include "../horizon-x.hpp"

using namespace std;

int main()
{
	Estimate estimate;

	int cement;
	int length, width, height;
	
	cout << "Enter tank Length (m): " << endl;
	cin >> length;

	cout << "Enter Tank width (m): " << endl;
	cin >> width;
	
	cout << "Enter Tank Height (m): " << endl;
	cin >> height;
	
	SepticTankMaterials materials = estimate.SepticTankEstimate(length, width, height);

	cout << "\n=========== MATERIALS REPORT [ Septic Tank Project ]==========\n" << endl;
	cout << "- Bricks : " <<  materials.bricks << endl;
	cout << "- Sand : " << materials.sand << " Wheel barrows" << endl;
	cout << "- Cement : " << materials.cementBags << " bags"<< endl;
	cout << "- Dampkos : " << materials.dampkos << endl;
	cout << "- Brick Force Wire : " << materials.reinforcementWire << endl;
	cout << "- Lintels : " << materials.lintels << endl;

	return 0;
}