export class NavigationView extends Element 
{
	routeName;  // current route name
	routeView;  // current route VNode
	routeParams;   // optional
	routes = {};

	constructor() {
		super();
		this.routeName = "initial";
		this.routeView = this.routes["initial"];
	}

	render() {
		return <section styleset={__DIR__ + "view.css#navigation"}>
			<header>
				<h2>Horizon Hub</h2>
			</header>
			<ul #navigation-bar >
				<li checked>Active tasks</li>
				<li>Tasks</li>
				<li>My Inventory</li>
				<li>Reports</li>
			</ul>
		</section>;
	}
}