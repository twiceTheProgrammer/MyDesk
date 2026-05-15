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

	["on click at button#run-estimate"](evt){

		let bagsOfCement = this.$("#the-form").value ?? {};
		// let result = this.estimateBricksFor(bagsOfCement);
		const errors = this.validate(bagsOfCement);
		Form.instance.componentUpdate({errors});

		return true;
	}
}