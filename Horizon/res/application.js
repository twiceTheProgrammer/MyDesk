import { MenuBar } from "menu/menu-bar.js";
import { ToolBar } from "toolbar/tool-bar.js";
import { NavigationView } from "navigation/view.js";
import { ContentView }    from "content/view.js";
import { PropertiesView } from "properties/view.js";

export class Application extends Element 
{

	// application state:
	navigationViewShown = true;
	propertiesViewShown = true;

	render()
	{
		return <body>
			<MenuBar app={this} />
			<ToolBar app={this} />
			<main>
				{ this.navigationViewShown && <NavigationView app={this} /> }
				<ContentView app={this} />
				{ this.propertiesViewShown && <PropertiesView app={this} /> }
			</main>
		</body>
	}
}