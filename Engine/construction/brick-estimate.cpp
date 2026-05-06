#include <iostream>

using std::string;

class BricksEstimate {

	private:
		int cement;
		int const ESTIMATE_BRICKS = 50;

	public:
		BricksEstimate(int bags)
		{
			cement = bags;
		}

		double NumberOfBricksFor(int cements)
		{
			int bricks = cements * ESTIMATE_BRICKS;

			return bricks;
		}
};

int main()
{
	BricksEstimate estimate1 = BricksEstimate(20);
	std::cout << "Total bricks : " << estimate1.NumberOfBricksFor(20) << std::endl;

	return 0;
}