
export class ContentView extends Element 
{
	render()
	{
		return <section styleset={__DIR__ + "view.css#content"}>
			<button #test >Click me!</button>
		</section>;
	}

	["on click at #test"]() {
		let res = Window.this.xcall("EstimateBricks", 30);
		Window.this.modal(<info>{res.total}</info>);
		return true; 
	}
}