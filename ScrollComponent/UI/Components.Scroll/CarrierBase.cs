using UI.Components.Scroll.Contracts;
using UI.Extensions;
using UnityEngine;

namespace UI.Components.Scroll
{
	public abstract class CarrierBase<TData> : ICarrier<TData> where TData : class
	{
		// temporal
		public static Vector2 GrowDirection  = Vector2.down;

		protected abstract void Update(IScrollComponent<TData> component);

		void ICarrier<TData>.Update(IScrollComponent<TData> component)
		{
			Update(component);
		}

		protected void Shift(IScrollComponent<TData> component, Vector2 delta)
		{
			//! temporal - item height limit speed
			//delta = delta.magnitude > ScrollerExtensions.SCROLL_SPEED_LIMIT_F ? delta.normalized * ScrollerExtensions.SCROLL_SPEED_LIMIT_F : delta;

			var direction = new Straight { Direction = GrowDirection };
			VectorGeneric2 intersection;
			if (!component.RectTransform.GetIntersectionInLocalSpace(direction, out intersection))
			{
				return;
			}

			if (Vector2.Dot(delta, GrowDirection) > 0)
			{
				var first = component.VisibleWindow.First;
				if (first != null && !first.RectTransform.IsInsideParentSpace(intersection.Origin) && component.RectTransform.IsOverlapsChild(first.RectTransform))
				{
					component.ShiftWindowUp();
				}
			}

			if (Vector2.Dot(delta, GrowDirection) < 0)
			{
				var last = component.VisibleWindow.Last;
				if (last != null && !last.RectTransform.IsInsideParentSpace(intersection.Target) && component.RectTransform.IsOverlapsChild(last.RectTransform))
				{
					component.ShiftWindowDown();
				}
			}
		}

		protected void MoveAll(IScrollComponent<TData> component, Vector2 delta)
		{
			//! temporal - item height limit speed
			//delta = delta.magnitude > ScrollerExtensions.SCROLL_SPEED_LIMIT_F ? delta.normalized * ScrollerExtensions.SCROLL_SPEED_LIMIT_F : delta;
			foreach (var item in new ForwardEnumerableWrapper<PageMap<IScrollItem<TData>>, IScrollItem<TData>>(component.VisibleWindow, null))
			{
				item.RectTransform.anchoredPosition += delta;
			}
		}
	}
}