using UI.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components.Scroll.Contracts
{
	public interface IScrollComponent<TData> :
		IInitializePotentialDragHandler,
		IBeginDragHandler,
		IEndDragHandler,
		IDragHandler,
		ICanvasElement,
		ILayoutElement,
		ILayoutGroup
	{
		RectTransform RectTransform { get; }
		PointerEventData PointerEventData { get; }
		ICarrierFactory<TData> CarrierFactory { get; }
		PageMap<IScrollItem<TData>> VisibleWindow { get; }

		void ShiftWindowUp();
		void ShiftWindowDown();
		void OnCarrierChange(ICarrier<TData> current);
	}
}