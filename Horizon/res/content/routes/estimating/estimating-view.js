import { PropertiesView } from "../../../properties/view.js";
import { ToolBar }        from "../../../toolbar/tool-bar.js";
export class EstimatingView extends Element 
{

	render() {
		return <estimating-view styleset={__DIR__ + "estimating-view.css#estimating"}>
			<section #properties >
				<header>
					<h3>Estimating Module</h3>
				</header>
				<select type="tree" treelines>
					<option expanded>
						<caption>Preparation</caption>
						<option>1. Bricks molding</option>
						<option>2. Progress report</option>
					</option>
				</select>
			</section>
			<section #content >
				<ToolBar />
				<form #prep-form >
					<label>Bags of Cement</label> <input name="bagsOfCement"/>
					<label>Wheel barrows of Sand</label><input name="sand"/>
				</form>
				<div #result ></div>
				<button #run-estimate >Calculate</button>
			</section>
		</estimating-view>;
	}

	estimateBricksFor(bagsOfCement) {
		let res = Window.this.xcall("EstimateBricksFor", bagsOfCement);
		// else {
		// 	Window.this.modal(<error> {bagsOfCement} is an invalid input.</error>);
		// }
		return res;
	}

	["on click at button#run-estimate"](){

		let bagsOfCement = this.$("#prep-form").value.bagsOfCement;
		let result = this.estimateBricksFor(bagsOfCement);
		this.$("#result").append(<p>Total bricks : {result.total}</p>);
		// Window.this.modal(<info>Estimated bricks for {bagsOfCement} bags of cement : {result.total} bricks<br />
		// </info>);
		return true;
	}
}