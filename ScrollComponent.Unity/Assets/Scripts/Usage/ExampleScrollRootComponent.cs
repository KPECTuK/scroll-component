using UI.Components.Scroll;
using UI.Components.Scroll.Contracts;
using UnityEngine;

namespace Assets.Scripts.Usage
{
	public class ExampleScrollRootComponent : ScrollComponentBase<ExampleScrollItemData>
	{
		protected override void Awake()
		{
			if(!Application.isPlaying)
				return;

			SetFactory(new DefaultItemFactory<ExampleScrollItemData>(this, GetComponent<IItemAssetProvider>()));
			SetFactory(new DefaultCarrierFactory<ExampleScrollItemData>(this));

			//AppendToBottom(new[]
			//{
			//	new ExampleScrollItemData { Data = "One" },
			//	new ExampleScrollItemData { Data = "Two" },
			//	new ExampleScrollItemData { Data = "Three" },
			//	new ExampleScrollItemData { Data = "Four" },
			//	new ExampleScrollItemData { Data = "Five" },
			//	new ExampleScrollItemData { Data = "Six" },
			//	new ExampleScrollItemData { Data = "Seven" },
			//	new ExampleScrollItemData { Data = "Eight" },
			//	new ExampleScrollItemData { Data = "Nine" },
			//});

			base.Awake();
		}
	}
}