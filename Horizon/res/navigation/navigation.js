export class NavigationView extends Element 
{

	render() {
		return <section styleset={__DIR__ + "navigation.css#navigation"}>
			<header>
				<h2>Horizon Hub</h2>
			</header>
			<ul #navigation-bar >
				<li #tasks      value="tasks" checked >My Tasks</li>
				<li #inventory  value="inventory">My Inventory</li>
				<li #reports    value="reports">Reports</li>
				<hr/>
				<li #estimating         value="estimating">Estimating</li>
				<li #planning           value="planning">Planning</li>
				<li #forecasting        value="forecasting">Forecasting</li>
				<li #cashflow           value="cashflow">Cashflow</li>
				<li #valuations         value="valuations">Valuations</li>
				<li #subcontractManager value="subcontract">Subcontract Manager</li>
				<li #costAndAllowables  value="costAndAllowables">Cost & Allowables</li>
				<li #materials          value="materials">Materials Received</li>
				<li #drawings           value="drawings">Drawings</li>
				<hr/>
				<li #profile  value="profile">Profile</li>
				<li #settings value="settings">Settings</li>
			</ul>
		</section>;
	}

	["on change at #navigation-bar>li"](evt, li) {
		this.dispatchEvent(new Event("navigate-to", {bubbles: true, data: li.value}));
		return true;
	}
}