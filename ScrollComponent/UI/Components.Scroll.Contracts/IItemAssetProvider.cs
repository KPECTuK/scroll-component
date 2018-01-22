using UnityEngine;

namespace UI.Components.Scroll.Contracts
{
	public interface IItemAssetProvider
	{
		RectTransform GetAsset<TData>() where TData : class;
	}
}