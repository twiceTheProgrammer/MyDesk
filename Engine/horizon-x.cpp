#include "horizon-x.hpp"

// Return API table.
extern "C" API const HorizonAPI* IHorizonAPI() {

	static HorizonAPI api = {
		VERSION,
		sizeof(HorizonAPI),
		&EstimateBricks
	};

	return &api;
}