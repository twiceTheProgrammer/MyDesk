#include <iostream>
#include "horizon-x.hpp"

using namespace std;

int main()
{
	Estimate estimate;

	int cement;
	int length, width, height;

	cout << "Enter bags of cement : " << endl;
	cin >> cement;

	int bricksProduced = estimate.BricksProduced(cement);
	int sandRequired = estimate.SandRequired(cement);
	
	cout << "Enter tank Length : " << endl;
	cin >> length;

	cout << "Enter Tank width : " << endl;
	cin >> width;
	
	cout << "Enter Tank Height : " << endl;
	cin >> height;
	
	int bricksForWall = estimate.BricksForWall(length, width, height);
	int cementBagsRequired = estimate.CementRequiredForWall(bricksForWall);

	cout << "\n=========== REPORT [ Septic Tank Project ]==========\n" << endl;
	cout << cement << " bags of cement produces:\n" << endl;
	cout << "- Bricks Produced : " <<  bricksProduced << endl;
	cout << "- Sand required : " << sandRequired << endl;
	cout << "- Bricks Required for Wall : " << bricksForWall << endl;
	cout << "- Cement Required : " << cementBagsRequired << endl;

	return 0;
}