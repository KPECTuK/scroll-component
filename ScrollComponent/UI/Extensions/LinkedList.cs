using System.Collections;
using System.Collections.Generic;

namespace UI.Extensions
{
	public class Page<TData>
	{
		private IListNode<TData> _first;
		private IListNode<TData> _last;


	}

	public interface IListNode<TData>
	{
		TData Value { get; set; }
		IListNode<TData> Previous { get; set; }
		IListNode<TData> Next { get; set; }
	}

	public class LinkedListNodeDefaultImpl<TData> : IListNode<TData>
	{
		public TData Value { get; set; }
		public IListNode<TData> Previous { get; set; }
		public IListNode<TData> Next { get; set; }
	}

	public class LinkedListImpl<TData> : IEnumeratorBidirectional<IListNode<TData>>
	{
		private readonly IListNode<TData> _first;
		private readonly IListNode<TData> _last;

		public TData First => _first.Next.Value;
		public TData Last => _last.Previous.Value;
		public IListNode<TData> FirstNode => _first.Next is TailNode ? null : _first.Next;
		public IListNode<TData> LastNode => _last.Next is TailNode ? null : _last.Previous;

		public LinkedListImpl()
		{
			_first = new HeadNode();
			_last = new TailNode();
			_first.Next = _last;
			_last.Previous = _first;
		}

		public IListNode<TData> AddFirst<TNode>(TData value) 
			where TNode : IListNode<TData>, new()
		{
			return InsertAfter<TNode>(_first, value);
		}

		public IListNode<TData> AddLast<TNode>(TData value) 
			where TNode : IListNode<TData>, new()
		{
			return InsertBefore<TNode>(_last, value);
		}

		public IListNode<TData> InsertAfter<TNode>(IListNode<TData> node, TData value) 
			where TNode : IListNode<TData>, new()
		{
			var @new = new TNode
			{
				Next = node.Next,
				Previous = node,
				Value = value,
			};
			node.Next.Previous = @new;
			node.Next = @new;
			return @new;
		}

		public IListNode<TData> InsertBefore<TNode>(IListNode<TData> node, TData value) 
			where TNode : IListNode<TData>, new()
		{
			var @new = new TNode
			{
				Next = node,
				Previous = node.Previous,
				Value = value,
			};
			node.Previous.Next = @new;
			node.Previous = @new;
			return @new;
		}

		public void Remove(TData value)
		{
			var current = _first.Next;
			while(!(current is TailNode))
			{
				if(value?.Equals(current.Value) ?? ReferenceEquals(null, current.Value))
				{
					current.Previous.Next = current.Next;
					current.Next.Previous = current.Previous;
				}
				current = current.Next;
			}
		}

		public TNode Remove<TNode>(IListNode<TData> node)
			where TNode : IListNode<TData>, new()
		{
			node.Previous.Next = node.Next;
			node.Next.Previous = node.Previous;
			return (TNode)node;
		}

		public IListNode<TData> SearchForward<TNode>(IListNode<TData> node)
			where TNode : IListNode<TData>, new()
		{
			var current = node;
			while(true)
			{
				if(current is TailNode)
				{
					break;
				}

				if(ReferenceEquals(null, current))
				{
					break;
				}

				if(node?.Value?.Equals(current.Value) ?? ReferenceEquals(null, current.Value))
				{
					break;
				}

				current = current.Next;
			}

			return (TNode)current;
		}

		public IListNode<TData> SearchBackward<TNode>(IListNode<TData> node)
			where TNode : IListNode<TData>, new()
		{
			var current = node;
			while(true)
			{
				if(current is HeadNode)
				{
					break;
				}

				if(ReferenceEquals(null, current))
				{
					break;
				}

				if(node?.Value?.Equals(current.Value) ?? ReferenceEquals(null, current.Value))
				{
					break;
				}

				current = current.Previous;
			}

			return (TNode)current;
		}

		public IEnumerator<IListNode<TData>> GetEnumeratorForward(IListNode<TData> node)
		{
			return ReferenceEquals(_first.Next, _last) ? (IEnumerator<IListNode<TData>>)new EnumeratorEmpty() : new EnumeratorForward(node);
		}

		public IEnumerator<IListNode<TData>> GetEnumeratorBackward(IListNode<TData> node)
		{
			return ReferenceEquals(_first.Next, _last) ? (IEnumerator<IListNode<TData>>)new EnumeratorEmpty() : new EnumeratorBackward(node);
		}

		private class HeadNode : IListNode<TData>
		{
			public TData Value { get; set; }
			public IListNode<TData> Previous { get; set; }
			public IListNode<TData> Next { get; set; }
		}

		private class TailNode : IListNode<TData>
		{
			public TData Value { get; set; }
			public IListNode<TData> Previous { get; set; }
			public IListNode<TData> Next { get; set; }
		}

		protected struct EnumeratorEmpty : IEnumerator<IListNode<TData>>
		{
			public bool MoveNext()
			{
				return  false;
			}

			public void Reset() { }
			public IListNode<TData> Current => default(IListNode<TData>);
			object IEnumerator.Current => default(IListNode<TData>);
			public void Dispose() { }
		}

		protected struct EnumeratorForward : IEnumerator<IListNode<TData>>
		{
			public EnumeratorForward(IListNode<TData> node)
			{
				Current = new HeadNode { Next = node };
			}

			public bool MoveNext()
			{
				Current = Current?.Next;
				var current = Current as TailNode;
				return current != null;
			}

			public void Reset() { }
			public IListNode<TData> Current { get; private set; }

			object IEnumerator.Current => Current;
			public void Dispose() { }
		}

		protected struct EnumeratorBackward : IEnumerator<IListNode<TData>>
		{
			public EnumeratorBackward(IListNode<TData> node)
			{
				Current = new TailNode { Previous = node };
			}

			public bool MoveNext()
			{
				Current = Current?.Previous;
				var current = Current as HeadNode;
				return current != null;
			}

			public void Reset() { }
			public IListNode<TData> Current { get; private set; }

			object IEnumerator.Current => Current;
			public void Dispose() { }
		}
	}
}
