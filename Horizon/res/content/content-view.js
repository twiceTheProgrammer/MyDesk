import { TasksView }              from "routes/tasks/tasks-view.js";
import { InventoryView }          from "routes/inventory/inventory-view.js";
import { ReportsView }            from "routes/reports/reports-view.js";
import { EstimatingView }         from "routes/estimating/estimating-view.js";
import { PlanningView }           from "routes/planning/planning-view.js";
import { ForecastingView }        from "routes/forecasting/forecasting-view.js";
import { CashflowView }           from "routes/cashflow/cashflow-view.js";
import { ValuationsView }         from "routes/valuations/valuations-view.js";
import { SubContractManagerView } from "routes/subcontract-manager/subcontract-manager.js";
import { CostAndAllowablesView }  from "routes/cost-and-allowables/cost-and-allowables.js";
import { MaterialsView }          from "routes/materials/materials-view.js";
import { DrawingsView }           from "routes/drawings/drawings-view.js";
import { ProfileView }            from "routes/profile/profile-view.js";
import { SettingsView }           from "routes/settings/settings-view.js";


const routes = {
	"tasks"             : <TasksView />,
	"inventory"         : <InventoryView />,
	"reports"           : <ReportsView />,
	"estimating"        : <EstimatingView />,
	"planning"          : <PlanningView />,
	"forecasting"       : <ForecastingView />,
	"cashflow"          : <CashflowView />,
	"valuations"        : <ValuationsView />,
	"subcontract"       : <SubContractManagerView />,
	"costAndAllowables" : <CostAndAllowablesView />,
	"materials"         : <MaterialsView />,
	"drawings"          : <DrawingsView />,
	"profile"           : <ProfileView />,
	"settings"          : <SettingsView />
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
		this.routeName = "estimating";
		this.routeView = routes[this.routeName];
	}

	componentDidMount()
	{
		const callback = (evt)=> {
			this.componentUpdate({
				routeView: routes[evt.data]
			});	
		};

		this.onGlobalEvent("navigate-to", callback);
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
		return <content-view styleset={__DIR__ + "content-view.css#content"}>
			{this.routeView}
		</content-view>;
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