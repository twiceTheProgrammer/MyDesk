#include <iostream>

using namespace std;

namespace Room1 {
	void greet() {
		cout << "Hello from Room 1" << endl;
	}
}

namespace Room2 {
	void greet() {
		cout << "Hello from Room 2" << endl;
	}
}

void func(int& x)
{
	x++;
}

using namespace Room1;

int main()
{
	greet();
	Room2::greet();
	int a = 3;
	func(a);
	cout << a << endl;
	return 0;
}