using UI.Components.Scroll.Carriers;
using UI.Components.Scroll.Contracts;

namespace UI.Components.Scroll
{
	public class DefaultCarrierFactory<TData> : ICarrierFactory<TData> where TData : class
	{
		//private readonly Dictionary<Type, ICarrier<TData>> _carriers;
		private ICarrier<TData> _current;
		private ICarrier<TData> _suspended;

		// ICarrierFactory
		public bool IsMoving => _current is OneItemHopCoDirectionCarrier<TData> || _current is OneItemHopCounterDirectionCarrier<TData>;

		public DefaultCarrierFactory(IScrollComponent<TData> component)
		{
			//_carriers = new Dictionary<Type, ICarrier<TData>>(5)
			//{
			//	{ typeof(OneItemHopCoDirectionCarrier<TData>), new OneItemHopCoDirectionCarrier<TData>(component) },
			//	{ typeof(OneItemHopCounterDirectionCarrier<TData>), new OneItemHopCounterDirectionCarrier<TData>(component) },
			//	{ typeof(BouncingCoDirectionCarrier<TData>), new BouncingCoDirectionCarrier<TData>() },
			//	{ typeof(BouncingCounterDirectionCarrier<TData>), new BouncingCounterDirectionCarrier<TData>() },
			//	{ typeof(DefaultCarrier<TData>), new DefaultCarrier<TData>() },
			//};

			SetDefaultCarrier(component);
		}

		// ICarrierFactory
		public void SetCarrierCoDirection(IScrollComponent<TData> component)
		{
			if(_current is SuspendedCarrier<TData>)
				return;
			_current = new OneItemHopCoDirectionCarrier<TData>(component);
			component.OnCarrierChange(_current);
		}

		// ICarrierFactory
		public void SetCarrierCounterDirection(IScrollComponent<TData> component)
		{
			if(_current is SuspendedCarrier<TData>)
				return;
			_current = new OneItemHopCounterDirectionCarrier<TData>(component);
			component.OnCarrierChange(_current);
		}

		// ICarrierFactory
		public void SetBumperCoDirection(IScrollComponent<TData> component)
		{
			if(_current is SuspendedCarrier<TData>)
				return;
			_current = new BouncingCoDirectionCarrier<TData>();
			component.OnCarrierChange(_current);
		}

		// ICarrierFactory
		public void SetBumperCounterDirection(IScrollComponent<TData> component)
		{
			if(_current is SuspendedCarrier<TData>)
				return;
			_current = new BouncingCounterDirectionCarrier<TData>();
			component.OnCarrierChange(_current);
		}

		// ICarrierFactory
		public void SetDefaultCarrier(IScrollComponent<TData> component)
		{
			if(_current is SuspendedCarrier<TData>)
				return;
			_current = new DefaultCarrier<TData>();
			component.OnCarrierChange(_current);
		}

		// ICarrierFactory
		public void UpdateController(IScrollComponent<TData> component)
		{
			_current.Update(component);
		}

		public void Suspend()
		{
			_suspended = _current;
			_current = new SuspendedCarrier<TData>();
		}

		public void Resume()
		{
			_current = _suspended ?? new DefaultCarrier<TData>();
			_suspended = null;
		}
	}
}