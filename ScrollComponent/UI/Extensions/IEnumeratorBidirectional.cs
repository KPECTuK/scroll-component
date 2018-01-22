using System.Collections.Generic;

namespace UI.Extensions
{
	public interface IEnumeratorBidirectional<TData>
	{
		IEnumerator<TData> GetEnumeratorForward(TData node);

		IEnumerator<TData> GetEnumeratorBackward(TData node);
	}
}