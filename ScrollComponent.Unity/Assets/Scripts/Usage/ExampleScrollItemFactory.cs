using UI.Components.Scroll;
using UI.Components.Scroll.Contracts;
using UnityEngine;

namespace Assets.Scripts.Usage
{
	public class ExampleScrollItemFactory : MonoBehaviour, IItemFactory<ExampleScrollItemData>, IItemAssetProvider
	{
		public RectTransform ItemAsset;

		private IItemFactory<ExampleScrollItemData> _itemFactory;

		// ReSharper disable once UnusedMember.Local
		private void Awake()
		{
			_itemFactory = new DefaultItemFactory<ExampleScrollItemData>(GetComponent<IScrollComponent<ExampleScrollItemData>>(), this);
		}

		public IScrollItem<ExampleScrollItemData> Get(ExampleScrollItemData data)
		{
			return _itemFactory.Get(data);
		}

		public void Put(IScrollItem<ExampleScrollItemData> item)
		{
			_itemFactory.Put(item);
		}

		public void ClearAssetCache()
		{
			_itemFactory.ClearAssetCache();
		}

		public void MaintainCache()
		{
			_itemFactory.MaintainCache();
		}

		public RectTransform GetAsset<TData>() where TData : class
		{
			return ItemAsset;
		}
	}
}