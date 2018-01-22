using UI.Components.Scroll.Contracts;
using UI.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components.Scroll
{
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Mask))]
	[RequireComponent(typeof(Image))]
	[SelectionBase]
	public partial class ScrollComponentBase<TData>
	{
		private IItemFactory<TData> _itemFactory;

		public PointerEventData PointerEventData { get; private set; }
		public ICarrierFactory<TData> CarrierFactory { get; private set; }

		protected void SetFactory(IItemFactory<TData> factory)
		{
			_itemFactory = factory ?? new DefaultItemFactory<TData>(this, GetComponent<IItemAssetProvider>());
		}

		protected void SetFactory(ICarrierFactory<TData> factory)
		{
			CarrierFactory = factory ?? new DefaultCarrierFactory<TData>(this);
		}

		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			// Debug.Log(GetType().NameNice() + " >> " + "OnInitializePotentialDrag");

			if(!Application.isPlaying)
				return;

			PointerEventData = eventData;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			// Debug.Log(GetType().NameNice() + " >> " + "OnBeginDrag");

			if(!Application.isPlaying)
				return;

			PointerEventData = eventData;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			// Debug.Log(GetType().NameNice() + " >> " + "OnEndDrag");

			if(!Application.isPlaying)
				return;

			PointerEventData = eventData;
		}

		public void OnDrag(PointerEventData eventData)
		{
			// Debug.Log(GetType().NameNice() + " >> " + "OnDrag");

			if(!Application.isPlaying)
				return;

			PointerEventData = eventData;
			if(CarrierFactory.IsMoving)
				return;

			if(Vector2.Dot(eventData.delta, CarrierBase<TData>.GrowDirection) < 0)
				CarrierFactory.SetCarrierCoDirection(this);
			else
				CarrierFactory.SetCarrierCounterDirection(this);
		}

		public void Rebuild(CanvasUpdate executing)
		{
			Debug.Log(GetType().NameNice() + " >> " + "Rebuild");

			if(!Application.isPlaying)
				return;

		}

		public void LayoutComplete()
		{
			Debug.Log(GetType().NameNice() + " >> " + "LayoutComplete");

			if(!Application.isPlaying)
				return;

		}

		public void GraphicUpdateComplete()
		{
			Debug.Log(GetType().NameNice() + " >> " + "GraphicUpdateComplete");

			if(!Application.isPlaying)
				return;

		}

		public void SetLayoutHorizontal()
		{
			Debug.Log(GetType().NameNice() + " >> " + "SetLayoutHorizontal");

			if(!Application.isPlaying)
				return;

		}

		public void SetLayoutVertical()
		{
			Debug.Log(GetType().NameNice() + " >> " + "SetLayoutVertical");

			if(!Application.isPlaying)
				return;

		}
	}
}
