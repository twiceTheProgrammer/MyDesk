export class NavigationView extends Element 
{

	render() {
		return <section styleset={__DIR__ + "navigation.css#navigation"}>
			<header>
				<h2>Horizon Hub</h2>
			</header>
			<ul #navigation-bar >
				<li checked value="tasks">Tasks</li>
				<li  value="inventory">My Inventory</li>
				<li value="reports">Reports</li>
			</ul>
		</section>;
	}
}