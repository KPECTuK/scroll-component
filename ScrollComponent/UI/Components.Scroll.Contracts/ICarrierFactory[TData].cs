namespace UI.Components.Scroll.Contracts
{
	public interface ICarrierFactory<TData>
	{
		bool IsMoving { get; }
		void Suspend();
		void Resume();
		void UpdateController(IScrollComponent<TData> component);
		void SetDefaultCarrier(IScrollComponent<TData> component);
		//
		void SetCarrierCoDirection(IScrollComponent<TData> component);
		void SetCarrierCounterDirection(IScrollComponent<TData> component);
		void SetBumperCoDirection(IScrollComponent<TData> component);
		void SetBumperCounterDirection(IScrollComponent<TData> component);
	}
}