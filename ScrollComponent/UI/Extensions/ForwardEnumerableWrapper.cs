using System.Collections;
using System.Collections.Generic;

namespace UI.Extensions
{
	public struct ForwardEnumerableWrapper<TSource, TData> : IEnumerable<TData> where TSource : IEnumeratorBidirectional<TData> where TData : class
	{
		private readonly IEnumerator<TData> _enumerator;

		public ForwardEnumerableWrapper(TSource source, TData first)
		{
			_enumerator = source.GetEnumeratorForward(first);
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