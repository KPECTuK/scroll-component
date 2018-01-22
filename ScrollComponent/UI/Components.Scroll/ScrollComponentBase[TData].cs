using System.Collections.Generic;
using UI.Components.Scroll.Contracts;
using UI.Extensions;

namespace UI.Components.Scroll
{
	public partial class ScrollComponentBase<TData> : PanelComponent, IScrollComponent<TData> where TData : class
	{
		private List<IScrollItem<TData>> _widgets;
		private Chain<PageMap<TData>, TData> _chain;

		public PageMap<IScrollItem<TData>> VisibleWindow { get; private set; }

		protected override void Awake()
		{
			base.Awake();

			SetFactory(null as IItemFactory<TData>);
			SetFactory(null as ICarrierFactory<TData>);

			_widgets = new List<IScrollItem<TData>>();
			VisibleWindow = new PageMap<IScrollItem<TData>>(null, new Range<int>());

			Bind(GetComponent<IPageProvider<TData>>());
		}

		public void Bind(IPageProvider<TData> pageProvider)
		{
			_chain = new Chain<PageMap<TData>, TData>(
				pageProvider, 
				new []
				{
					new PageMap<TData>(pageProvider, new Range<int>()),
					new PageMap<TData>(pageProvider, new Range<int>()),
				});
		}



		public void ShiftWindowUp()
		{
			if(VisibleWindow.TryShiftUp())
			{
				if(_chain.First.IsUpperHalf(_chain.First))
				{
					_chain.LoadPreviousPage();
				}
			}
			else
			{
				if(_chain.IsUpperLimit)
				{
					CarrierFactory.SetBumperCoDirection(this);
				}
				else
				{
					CarrierFactory.Suspend();
				}
			}
		}

		public void ShiftWindowDown()
		{
			if(VisibleWindow.TryShiftDown())
			{
				if(_chain.Last.IsLowerHalf(_chain.Last))
				{
					_chain.LoadNextPage();
				}
			}
			else
			{
				if(_chain.IsLowerLimit)
				{
					CarrierFactory.SetBumperCounterDirection(this);
				}
				else
				{
					CarrierFactory.Suspend();
				}
			}
		}

		public void OnCarrierChange(ICarrier<TData> current)
		{
			// sound hook
		}
	}
}