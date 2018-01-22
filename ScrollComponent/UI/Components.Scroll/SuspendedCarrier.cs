using UI.Components.Scroll.Contracts;

namespace UI.Components.Scroll
{
	public class SuspendedCarrier<TData> : CarrierBase<TData> where TData : class
	{
		protected override void Update(IScrollComponent<TData> component) { }
	}
}