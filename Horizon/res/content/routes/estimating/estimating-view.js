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
				<section #prep-report >
					<form #prep-form >
						<label>Bags of Cement</label> <input name="bagsOfCement"/>
					</form>
					<div #result >
						<dl>
							<header>Materials list</header>
							<dt>Bricks</dt> <dd>x</dd>
							<dt>Sand</dt> <dd>x</dd>
							<dt>Damp kos</dt> <dd>x</dd>
							<dt>ReInforcement wire</dt> <dd>x</dd>
							<dt>Cements</dt> <dd>x</dd>
							<dt>Labour</dt> <dd>x</dd>
							<dt>Transport costs</dt> <dd>x</dd>
						</dl>
						<hr />
						<dl>
							<header>Produced Materials</header>
							<dt>Bricks : </dt> <dd>x</dd>
						</dl>
					</div>
				</section>
			</section>
		</estimating-view>;
	}

	estimateBricksFor(bagsOfCement) {
		let bricksProduced;
		let sandRequired;

		if (bagsOfCement) {
			bricksProduced = Window.this.xcall("EstimateBricksFor", bagsOfCement);
			sandRequired   = Window.this.xcall("SandRequired", bagsOfCement);
			return { bricks: bricksProduced, sand: sandRequired};
		}
		else {
			Window.this.modal(<error>{bagsOfCement} is an Invalid input.</error>);
		}

		return false;
	}

	["on click at button#run-estimate"](){

		let bagsOfCement = this.$("#prep-form").value.bagsOfCement;
		let result = this.estimateBricksFor(bagsOfCement);

		if(result) {
			this.$("#produced-bricks").innerText = result.bricks.total;
			this.$("#required-bricks").innerText = 0;
			this.$("#required-sand").innerText = result.sand.total;
			this.$("#required-labour").innerText = 5000;
		}
		return true;
	}
}