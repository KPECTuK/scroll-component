using System.Collections;
using System.Collections.Generic;

namespace UI.Extensions
{
	public struct BackwardEnumerableWrapper<TSource, TData> : IEnumerable<TData> where TSource : IEnumeratorBidirectional<TData> where TData : class
	{
		private readonly IEnumerator<TData> _enumerator;

		public BackwardEnumerableWrapper(TSource source, TData first)
		{
			_enumerator = source.GetEnumeratorBackward(first);
		}

		public IEnumerator<TData> GetEnumerator()
		{
			return _enumerator;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}