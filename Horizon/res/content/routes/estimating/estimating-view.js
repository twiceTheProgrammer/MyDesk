import { PropertiesView } from "../../../properties/view.js";
import { ToolBar }        from "../../../toolbar/tool-bar.js";
import { Form, FormError } from "../../../utils/form/form.js";
export class EstimatingView extends Element 
{

	validate(values)
	{
		const errors = {};
		if(!values.tankLength) {
			errors.tankLength = 'Required';
		}

		if(!values.tankWidth) {
			errors.tankWidth = 'Required';
		}

		if(!values.tankDepth) {
			errors.tankDepth = 'Required';
		}

		if(!values.bagsOfCement) {
			errors.bagsOfCement = 'Required';
		}

		if(!values.availableSand) {
			errors.availableSand = 'Required';
		}

		if(!values.brickLength) {
			errors.brickLength = 'Required';
		}

		if(!values.brickWidth) {
			errors.brickWidth = 'Required';
		}

		if(!values.brickHeight) {
			errors.brickHeight = 'Required';
		}
		return errors;
	}

	render() {
		return <estimating-view styleset={__DIR__ + "estimating-view.css#estimating"}>
			<section #content >
				<toolbar>
					<button #run-estimate tooltip="Run Estimate"/>
				</toolbar>
				<section #prep-report >
					<h3>Materials Manufactoring</h3>
					<Form #the-form value={{}}>
						<label>Length of Septic Tank (m) : </label> 
						 	<input|text name="tankLength"></input>
								<FormError key="tankLength" /> 
						<label>Width of Septic Tank (m): </label> 
							<input name="tankWidth"></input>
								<FormError key="tankWidth" /> 

						<label>Depth of Septic Tank (m): </label> 
							<input name="tankDepth"></input>
								<FormError key="tankDepth" /> 

						<label>Bags of Cement: </label> 
							<input name="bagsOfCement"/>
								<FormError key="bagsOfCement" /> 

						<label>Available sand : </label> 
							<input name="availableSand" />
								<FormError key="availableSand" /> 

						<label>Brick Length (mm):</label> 
							<input name="brickLength" />
								<FormError key="brickLength" /> 

						<label>Brick Width (mm): </label> 
							<input name="brickWidth" />
								<FormError key="brickWidth" /> 

						<label>Brick Height (mm): </label> 
							<input name="brickHeight" />
								<FormError key="brickHeight" /> 
					</Form>
					<div #result >
							<dl>
								<header>Materials list Required</header>
								<dt>Bricks</dt> <dd>x</dd>
								<dt>Sand</dt> <dd>x</dd>
								<dt>Damp kos</dt> <dd>x</dd>
								<dt>ReInforcement wire</dt> <dd>x</dd>
								<dt>Cements</dt> <dd>x</dd>
								<dt>Labour</dt> <dd>x</dd>
								<dt>Transport costs</dt> <dd>x</dd>
							</dl>
							<dl>
								<header>Produced/Available Materials</header>
								<dt>Bricks : </dt> <dd>x</dd>
								<dt>Sand : </dt> <dd>x</dd>
								<dt>Damp kos : </dt> <dd>x</dd>
								<dt>ReInforcement wire : </dt> <dd>x</dd>
								<dt>Cements : </dt> <dd>x</dd>
								<dt>Labour : </dt> <dd>x</dd>
								<dt>Transport costs : </dt> <dd>x</dd>
							</dl>
						</div>
					</section>
				</section>
		</estimating-view>;
	}

	runEstimate(spec) {
		let bricksProduced;
		let sandRequired;
		let estimateReport = {};

		bricksProduced = Window.this.xcall("EstimateBricksFor", spec.bagsOfCement);
		sandRequired   = Window.this.xcall("SandRequired", spec.bagsOfCement);

		estimateReport.bricks = bricksProduced;
		estimateReport.sand = sandRequired;

		return estimateReport;
	}

	["on click at button#run-estimate"](evt){

		let projectSpec = this.$("#the-form").value ?? {};
		const errors = this.validate(projectSpec);
		Form.instance.componentUpdate({errors});

		let res = this.runEstimate(projectSpec);
		// Window.this.modal(<info>{JSON.stringify(res, " ")}</info>);
		return true;
	}
}