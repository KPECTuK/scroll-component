using System;
using System.Collections;
using System.Collections.Generic;
using UI.Components.Scroll.Contracts;

namespace UI.Extensions
{
	public class PageMap<TData> : IEnumeratorBidirectional<TData> where TData : class
	{
		private readonly IPageProvider<TData> _source;

		public IRange<int> Bounds { get; private set; }
		public bool IsReadOnly => true;
		public int Count => Bounds?.Length() ?? 0;
		public TData First => this[Bounds.Min];
		public TData Last => this[Bounds.Max];
		public TData this[int index]
		{
			get { return (Bounds?.IsInside(index) ?? false) ? _source[index] : default(TData); }
			set { throw new NotSupportedException(); }
		}

		public static T Create<T>(IPageProvider<TData> source, IRange<int> range) where T : PageMap<TData>
		{
			if(ReferenceEquals(typeof(T), typeof(PageMap<TData>)))
			{
				return new PageMap<TData>(source, range) as T;
			}

			throw new NotSupportedException("type not supported");
		}

		public PageMap(IPageProvider<TData> source, int first, int last)
		{
			_source = source;
			if(ReferenceEquals(null, _source))
				throw new ArgumentException();
			SetBounds(first, last);
		}

		public PageMap(IPageProvider<TData> source, IRange<int> range)
		{
			_source = source;
			if(ReferenceEquals(null, _source))
				throw new ArgumentException();
			SetBounds(range);
		}

		public void SetBounds(int first, int length)
		{
			SetBounds(length == 0 ? null : (IRange<int>)new Range<int>(first, first + length - 1));
		}

		public void SetBounds(IRange<int> range)
		{
			//var assert = new Range<int>(0, _source.Count - 1);
			//if(!assert.IsInside(_bounds.Min) || !assert.IsInside(_bounds.Max))
			//	throw new ArgumentOutOfRangeException();
			Bounds = range;
		}

		public bool IsHalfFits(PageMap<TData> outer)
		{
			return outer.Count * 2 / _source.Count > 0;
		}

		public bool IsUpperHalf(PageMap<TData> outer)
		{
			var outerMiddle = outer.Count / 2;
			return Bounds.Max <= outerMiddle && Bounds.Min <= outerMiddle;
		}

		public bool IsLowerHalf(PageMap<TData> outer)
		{
			var outerMiddle = outer.Count / 2;
			return Bounds.Max >= outerMiddle && Bounds.Min >= outerMiddle;
		}

		public bool TryShiftUp()
		{
			if(ReferenceEquals(null, Bounds))
				return false;

			var @new = new Range<int>(Bounds.Min - 1, Bounds.Max - 1);
			var assert = AssertInsideSource(@new);
			Bounds = assert ? @new : Bounds;
			return assert;
		}

		public bool TryShiftDown()
		{
			if(ReferenceEquals(null, Bounds))
				return false;

			var @new = new Range<int>(Bounds.Min + 1, Bounds.Max + 1);
			var assert = AssertInsideSource(@new);
			Bounds = assert ? @new : Bounds;
			return assert;
		}

		public bool AssertInsideSource(Range<int> value)
		{
			var assert = new Range<int>(0, _source.Count - 1);
			return assert.IsInside(value.Min) && assert.IsInside(value.Max);
		}

		public bool Contains(TData item)
		{
			if(ReferenceEquals(null, item) || ReferenceEquals(null, Bounds))
			{
				return false;
			}

			for(var index = Bounds.Min; index <= Bounds.Max; index++)
			{
				if(item.Equals(_source[index]))
				{
					return true;
				}
			}

			return false;
		}

		public int IndexOf(TData item)
		{
			if(ReferenceEquals(null, item) || ReferenceEquals(null, Bounds))
			{
				return -1;
			}

			for(var index = Bounds.Min; index <= Bounds.Max; index++)
			{
				if(item.Equals(_source[index]))
				{
					return index;
				}
			}

			return -1;
		}

		public IEnumerator<TData> GetEnumeratorForward(TData node)
		{
			return ReferenceEquals(null, Bounds) 
				? (IEnumerator<TData>)new EnumeratorEmpty()  
				: new EnumeratorForward(this);
		}

		public IEnumerator<TData> GetEnumeratorBackward(TData node)
		{
			return ReferenceEquals(null, Bounds) 
				? (IEnumerator<TData>)new EnumeratorEmpty()  
				: new EnumeratorBackward(this);
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public void CopyTo(TData[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, TData item)
		{
			throw new NotSupportedException();
		}

		public void Add(TData item)
		{
			throw new NotSupportedException();
		}

		public bool Remove(TData item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		private struct EnumeratorEmpty : IEnumerator<TData>
		{
			public bool MoveNext()
			{
				return  false;
			}

			public void Reset() { }
			public TData Current => default(TData);
			object IEnumerator.Current => default(TData);
			public void Dispose() { }
		}

		private struct EnumeratorForward : IEnumerator<TData>
		{
			private readonly PageMap<TData> _page;
			private int _index;

			public EnumeratorForward(PageMap<TData> page)
			{
				_page = page;
				_index = _page.Bounds.Min - 1;
			}

			public bool MoveNext()
			{
				++_index;
				return _index < _page.Bounds.Max + 1;
			}

			public void Reset() { }
			public TData Current => _page.Bounds.IsInside(_index) ? _page[_index] : default(TData);
			object IEnumerator.Current => Current;
			public void Dispose() { }
		}

		private struct EnumeratorBackward : IEnumerator<TData>
		{
			private readonly PageMap<TData> _page;
			private int _index;

			public EnumeratorBackward(PageMap<TData> page)
			{
				_page = page;
				_index = _page.Bounds.Max + 1;
			}

			public bool MoveNext()
			{
				--_index;
				return _index > _page.Bounds.Min - 1;
			}

			public void Reset() { }
			public TData Current => _page.Bounds.IsInside(_index) ? _page[_index] : default(TData);
			object IEnumerator.Current => Current;
			public void Dispose() { }
		}
	}
}
