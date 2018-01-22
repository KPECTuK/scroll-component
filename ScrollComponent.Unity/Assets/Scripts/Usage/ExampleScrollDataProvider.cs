using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Components.Scroll.Contracts;
using UI.Extensions;
using UnityEngine;

namespace Assets.Scripts.Usage
{
	public partial class ExampleScrollDataProvider : MonoBehaviour, IPageProvider<ExampleScrollItemData>
	{
		private List<ExampleScrollItemData> _data = new List<ExampleScrollItemData>();
		private int counter;

		public void StartLoadPage(IPageProviderListener listener, IRange<int> range)
		{
			StartCoroutine(LoadPage(listener, range));
		}

		private IEnumerator LoadPage(IPageProviderListener listener, IRange<int> range)
		{
			if(!ReferenceEquals(null, range))
			{
				yield return null;

				Debug.Log("loading range: " + range);

				if(range.Max >= _data.Count)
				{
					Resize(range.Max);
				}

					for(var index = range.Min; index <= range.Max; index++)
				{
					_data[index] = new ExampleScrollItemData {Data = "data-" + counter++};
				}
			}
			listener.OnPageLoad(range);
		}

		private void Resize(int size)
		{
			var @new = new ExampleScrollItemData[size];
			_data.CopyTo(@new);
			_data = @new.ToList();
		}
	}
}
