namespace UI.Components.Scroll.Contracts
{
	public interface ICarrier<TData>
	{
		void Update(IScrollComponent<TData> component);
	}
}