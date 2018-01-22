using System.Diagnostics;
using UI.Components.Scroll.Contracts;
using UI.Extensions;
using UnityEngine;

namespace UI.Components.Scroll.Carriers
{
	public class DraggingCarrier<TData> : CarrierBase<TData> where TData : class
	{
		protected override void Update(IScrollComponent<TData> component)
		{
			var delta = component.PointerEventData?.delta.ProjectTo(GrowDirection) ?? Vector2.zero;
			MoveAll(component, delta);
			Shift(component, delta);
		}

		[Conditional("UNITY_EDITOR")]
		private void DrawDebug(IScrollComponent<TData> component)
		{
			ScrollerExtensions.MultiLine(component.RectTransform.GetSidesInLocalSpace(), Color.yellow * .5f, true, .5f);
			ScrollerExtensions.MultiLine(component.VisibleWindow.First.RectTransform, Color.yellow, true, .5f);
			ScrollerExtensions.MultiLine(component.VisibleWindow.Last.RectTransform, Color.yellow, true, .5f);
			//ScrollerExtensions.Cross(intersection.Origin, Color.yellow, 30f, 0f);
			//ScrollerExtensions.Cross(intersection.Target, Color.yellow, 30f, 0f);
		}
	}
}