using NUnit.Framework;
using System.Collections.Generic;
using UI.Extensions;

namespace ScrollComponent.Tests
{
	[TestFixture]
    public class Tests
    {
	    [Test]
	    public void Test()
		{
			var links = new LinkedListImpl<int>();
			for(var index = 0; index < 10; index++)
			{
				///links.AddFirst<LinkedListNodeDefaultImpl<int>>(index);
				links.AddLast<LinkedListNodeDefaultImpl<int>>(index);
			}

			var list = new List<int>();
			foreach(var data in new ForwardEnumerableWrapper<LinkedListImpl<int>, IListNode<int>>(links, links.FirstNode))
			{
				list.Add(data.Value);
			}

			list.TrueForAll(_ => true);
		}
    }
}
