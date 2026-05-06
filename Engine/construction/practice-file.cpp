#include <iostream>

using std::string;

class Employee {

private:
	string Name;
	string Company;
	int Age;

public:
	void setName(string name) {
		Name = name;
	}

	string getName() {
		return Name;
	}

	void IntroduceYourSelf() {

		Employee::setName("Riley");

		std::cout << "Name: " << Employee::getName() << std::endl;
		std::cout << "Company: " << Company << std::endl;
		std::cout << "Age: " << Age << std::endl;
	}

	Employee(string name, string company, int age)
	{
		Name = name;
		Company = company;
		Age = age;
	}
};

int main()
{
	Employee employee("Njabulo", "TheHorizonHub", 29);

	employee.IntroduceYourSelf();

	return 0;
}