using System.Collections.Generic;
using UI.Extensions;

namespace UI.Components.Scroll.Contracts
{
	public interface IPageProvider<TData> : IList<TData>
	{
		void StartLoadPage(IPageProviderListener listener, IRange<int> range);
	}
}