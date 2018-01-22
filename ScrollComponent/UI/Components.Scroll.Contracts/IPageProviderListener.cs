using UI.Extensions;

namespace UI.Components.Scroll.Contracts
{
	public interface IPageProviderListener
	{
		void OnPageLoad(IRange<int> range);
	}
}