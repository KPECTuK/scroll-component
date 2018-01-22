using System;
using System.Collections.Generic;
using System.Linq;
using UI.Components.Scroll.Contracts;
using UnityEngine;

namespace UI.Components.Scroll
{
	public class DefaultItemFactory<TData> : IItemFactory<TData> where TData : class
	{
		private readonly Stack<IScrollItem<TData>> _itemsCache = new Stack<IScrollItem<TData>>();
		private readonly IScrollComponent<TData> _container;
		private readonly IItemAssetProvider _assetProvider;

		public DefaultItemFactory(IScrollComponent<TData> container, IItemAssetProvider assetProvider)
		{
			_container = container;
			_assetProvider = assetProvider;
		} 

		// IItemFactory
		public IScrollItem<TData> Get(TData data)
		{
			var widget = _itemsCache.Count == 0
				? UnityEngine.Object.Instantiate(_assetProvider.GetAsset<TData>(), _container.RectTransform, false).GetComponent<IScrollItem<TData>>()
				: _itemsCache.Pop();
			//! remove
			widget.RectTransform.gameObject.SetActive(true);
			widget.Resume(data);
			return widget;
		}

		// IItemFactory
		public void Put(IScrollItem<TData> widget)
		{
			foreach(var item in widget.RectTransform.GetComponentsInChildren<IScrollItem<TData>>())
				item.Suspend();
			//! remove
			widget.RectTransform.gameObject.SetActive(false);
			_itemsCache.Push(widget);
		}

		// IItemFactory
		public void ClearAssetCache()
		{
			var group = _itemsCache.Select(_ => _ as MonoBehaviour).Where(_ => _ != null).ToArray();
			Array.ForEach(group, UnityEngine.Object.Destroy);
			_itemsCache.Clear();
		}

		public void MaintainCache()
		{
			// do cache operations here (hiding unused)
		}
	}
}