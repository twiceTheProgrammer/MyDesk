
export class InventoryView extends Element 
{
	render()
	{
		return <inventory-view styleset={__DIR__ + "inventory-view.css#inventory"}>
				<section #prep-report >
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
						<hr />
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
		</inventory-view>;
	}
}