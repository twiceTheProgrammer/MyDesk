export class NavigationView extends Element 
{
	render() {
		return <section styleset={__DIR__ + "view.css#navigation"}>
			<ul>
				<li>Active tasks</li>
				<li>Tasks</li>
				<li>My Inventory</li>
				<li>Reports</li>
			</ul>
		</section>;
	}
}