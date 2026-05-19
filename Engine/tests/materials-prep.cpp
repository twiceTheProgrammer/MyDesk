#include <iostream>
#include <string>
#include "../horizon-x.hpp"

using namespace std;

int main()
{
	Estimate estimate;

	int cement;
	int length, width, height;
	
	string jobName;
	cout << "What is the Job name : " << endl;
	getline(cin, jobName);

	cout << "\n=========== PROJECT SPECIFICS ==========\n" << endl;
	cout << jobName << " Length (m): " << endl;
	cin >> length;

	cout <<  jobName << " Width (m): " << endl;
	cin >> width;
	
	cout <<  jobName << " Height (m): " << endl;
	cin >> height;
	
	SepticTankMaterials materials = estimate.SepticTankEstimate(length, width, height);

	cout << "\n=========== MATERIALS REPORT ==========\n" << endl;
	cout << "- Bricks:            " <<  materials.bricks << endl;
	cout << "- Sand :             " << materials.sand << " Wheel barrows" << endl;
	cout << "- Cement :           " << materials.cementBags << " bags"<< endl;
	cout << "- Dampkos :          " << materials.dampkos << endl;
	cout << "- Brick Force Wire : " << materials.brickForce << endl;
	cout << "- Lintels :          " << materials.lintels << endl;

	return 0;
}