using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Components.Scroll.Contracts;
using UI.Extensions;

namespace UI.Components.Scroll
{
	public class Chain<TPage, TData> : IList<TData> where TPage : PageMap<TData> where TData : class
	{
		// с другой стороны это можно представить себе как движение по списку с противоположными приращениями, 
		// но там много мелки отличий которые я не знаю пока как слить

		private readonly IPageProvider<TData> _source;
		private readonly TPage[] _pages;
		private int _first;
		private int _last;
		private LoadBinderBase _binder;

		public bool IsReadOnly => true;
		public bool IsEmpty => _last - _first < 0;
		public int Count => _pages.Sum(_ => _.Count);
		public bool IsUpperLimit => false;
		public bool IsLowerLimit => false;
		public TPage First => _pages[_first];
		public TPage Last => _pages[_last];
		public int PageSize { get; }
		public TData this[int index]
		{
			get { return _pages.FirstOrDefault(_ => _.Bounds.IsInside(index))?[index]; }
			set { throw new NotSupportedException(); }
		}

		public Chain(IPageProvider<TData> source, TPage[] pages)
		{
			_source = source;
			_pages = new TPage[pages.Length + 2];
			Array.Copy(pages, 0, _pages, 1, pages.Length);

			// TODO: check sequential in Editor

			_first = 1;
			_last = _pages.Length - 2;

			new ReloadExistingBinder(this, _first);
			new ReloadExistingBinder(this, _last);
		}

		public void LoadPreviousPage()
		{
			if(_binder is LoadPreviousBinder)
				return;

			_binder = new LoadPreviousBinder(this, (_first + _pages.Length - 1) % _pages.Length);
		}

		public void LoadNextPage()
		{
			if(_binder is LoadNextBinder)
				return;

			_binder = new LoadNextBinder(this, (_last + 1) % _pages.Length);
		}

		public bool Contains(TData item)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(TData item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TData[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<TData> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		public void Add(TData item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Remove(TData item)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, TData item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		private class LoadPreviousBinder : LoadBinderBase
		{
			public LoadPreviousBinder(Chain<TPage, TData> parent, int into) : base(parent, into)
			{
				Parent._source.StartLoadPage(this, new Range<int>(
					Parent.First.Bounds.Min - Parent.PageSize, 
					Parent.First.Bounds.Min));
			}

			public override void OnPageLoad(IRange<int> range)
			{
				base.OnPageLoad(range);

				Parent._pages[Parent._last] = null;
				Parent._first = (Parent._first - 1) % Parent._pages.Length;
				Parent._last = (Parent._last - 1) % Parent._pages.Length;
			}
		}

		private class LoadNextBinder : LoadBinderBase
		{
			public LoadNextBinder(Chain<TPage, TData> parent, int into) : base(parent, into)
			{
				Parent._source.StartLoadPage(this, new Range<int>(
					Parent.First.Bounds.Max, 
					Parent.First.Bounds.Max + Parent.PageSize));
			}

			public override void OnPageLoad(IRange<int> range)
			{
				base.OnPageLoad(range);

				Parent._pages[Parent._first] = null;
				Parent._first = (Parent._first + 1) % Parent._pages.Length;
				Parent._last = (Parent._last + 1) % Parent._pages.Length;
			}
		}

		private class ReloadExistingBinder : LoadBinderBase
		{
			public ReloadExistingBinder(Chain<TPage, TData> parent, int into) : base(parent, into)
			{
				Parent._source.StartLoadPage(this, Parent._pages[into].Bounds);
			}
		}

		private abstract class LoadBinderBase : IPageProviderListener
		{
			//! holding reference
			protected readonly Chain<TPage, TData> Parent;
			protected readonly int Index;

			protected LoadBinderBase(Chain<TPage, TData> parent, int into)
			{
				Parent = parent;
				Index = into;
			}

			public virtual void OnPageLoad(IRange<int> range)
			{
				Parent._pages[Index] = PageMap<TData>.Create<TPage>(Parent._source, range);
				Parent._binder = null;
			}
		}
	}
}