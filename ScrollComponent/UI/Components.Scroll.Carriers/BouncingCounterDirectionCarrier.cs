using UI.Components.Scroll.Contracts;
using UI.Extensions;

namespace UI.Components.Scroll.Carriers
{
	public class BouncingCounterDirectionCarrier<TData> : BumperCarrierBase<TData> where TData : class
	{
		protected override void Update(IScrollComponent<TData> component)
		{
			var cutting = new Straight { Direction = GrowDirection };
			VectorGeneric2 intersection;
			component.RectTransform.GetIntersectionInLocalSpace(cutting, out intersection);
			var isFirst = true;
			foreach (var item in new ForwardEnumerableWrapper<PageMap<IScrollItem<TData>>, IScrollItem<TData>>(component.VisibleWindow, null))
			{
				item.RectTransform.anchoredPosition = isFirst ? intersection.Origin : intersection.Target;
				item.RectTransform.GetIntersectionInParentSpace(cutting, out intersection);
				isFirst = false;
			}
		}
	}
}