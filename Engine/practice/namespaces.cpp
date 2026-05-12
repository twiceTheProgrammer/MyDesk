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

using namespace Room1;

int main()
{
	greet();
	Room2::greet();

	return 0;
}