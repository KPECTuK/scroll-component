using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Usage
{
	public partial class ExampleScrollDataProvider
	{
		public bool IsReadOnly { get { return true; } }
		public int Count { get { return _data.Count; } }
		public ExampleScrollItemData this[int index]
		{
			get { return _data[index]; }
			set { _data[index] = value; }
		}

		public IEnumerator<ExampleScrollItemData> GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(ExampleScrollItemData item)
		{
			_data.Add(item);
		}

		public void Clear()
		{
			_data.Clear();
		}

		public bool Contains(ExampleScrollItemData item)
		{
			return _data.Contains(item);
		}

		public void CopyTo(ExampleScrollItemData[] array, int arrayIndex)
		{
			_data.CopyTo(array, arrayIndex);
		}

		public bool Remove(ExampleScrollItemData item)
		{
			return _data.Remove(item);
		}

		public int IndexOf(ExampleScrollItemData item)
		{
			return _data.IndexOf(item);
		}

		public void Insert(int index, ExampleScrollItemData item)
		{
			_data.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			_data.RemoveAt(index);
		}
	}
}
