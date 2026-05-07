import { TasksView }     from "routes/tasks-view.js";
import { InventoryView } from "routes/inventory-view.js";

const routes = {
	"tasks" : <TasksView />,
	"inventory" : <InventoryView />
};

export class ContentView extends Element 
{
	routeName;
	routeView;
	routeParams;

	constructor()
	{
		super();

		this.routes = routes;
		this.routeName = "inventory";
		this.routeView = routes[this.routeName];
	}

	navigateTo(routeName, routeParams = null)
	{
		let routeView = routes[routeName];
		if(routeView)
		{
			this.componentUpdate({
				routeView: routeView,
				routeName: routeName,
				routeParams: routeParams
			});

			return true;
		}
	}

	render()
	{
		return <section styleset={__DIR__ + "content-view.css#content"}>
			{this.routeView}
		</section>;
	}

	// event handlers:
	onnavigateto(event)
	{
		let {route, params} = event.data;
		return this.navigateTo(route, params);   // true - event consumed
	}

	// click on <a href="route:..."> or <button href="route:...">
	["on click at [href^='route:'"](event, hyperlink) {
		const routeName = hyperlink.attributes["href"].substr(6);
		return this.navigateTo(routeName);
	}
}