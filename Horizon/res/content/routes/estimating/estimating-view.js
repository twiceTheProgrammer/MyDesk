import { PropertiesView } from "../../../properties/view.js";
import { ToolBar }        from "../../../toolbar/tool-bar.js";
export class EstimatingView extends Element 
{

	render() {
		return <estimating-view styleset={__DIR__ + "estimating-view.css#estimating"}>
			<section #properties >
				<header>
					<h3>Estimating Bill documents</h3>
				</header>
				<select type="tree" treelines>
					<option>
						<caption>1. Bill Pricing</caption>
						<option>1.2 Pricing Bill</option>
						<option>1.3 Master Pricing Bill</option>
						<option>1.4 Price code centric Bill</option>
					</option>
					<option>
						<caption>2. Rate & quantity comparisons</caption>
						<option>2.1 Net vs Gross vs Selling</option>
					</option>
					<option>
						<caption>3. Bill Analysis</caption>
						<option>3.1 Resources per Bill item</option>
						<option>3.2 Manhours per Bill item</option>
					</option>
					<option>
						<caption>4. Subcontract Adjudication</caption>
						<option>4.1 Package allocation</option>
					</option>
					<option>
						<caption>5. Specialised bills</caption>
						<option>5.1 QTO Bill of Quantities</option>
						<option>5.2 Bill importing - Basic</option>
						<option>5.7 Alternative estimate (Parent only)</option>
					</option>
				</select>
			</section>
			<section #content >
				<ToolBar />
				Estimating Suite
			</section>
		</estimating-view>;
	}
}