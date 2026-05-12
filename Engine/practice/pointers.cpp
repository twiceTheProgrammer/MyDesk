#include <iostream>

using namespace std;

int main()
{
	int x = 10;
	int* myptr;

	myptr = &x;

	cout << "Value of x is : ";
	cout << x << endl;

	// print using a pointer
	cout << "Address stored in my pointer : ";
	cout << myptr << endl;

	// print value of x using my pointer;
	cout << "Value in myptr : " << *myptr << endl;

	return 0;
}