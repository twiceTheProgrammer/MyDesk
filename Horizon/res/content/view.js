
export class ContentView extends Element 
{
	render()
	{
		return <section styleset={__DIR__ + "view.css#content"}>
			<button #test >Click me!</button>
		</section>;
	}

	["on click at #test"]() {
		let res = Window.this.xcall("Host_HelloWorld");
		Window.this.modal(<info>{res}</info>);
		return true; 
	}
}