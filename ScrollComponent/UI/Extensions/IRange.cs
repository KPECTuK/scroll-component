using System;
using System.Xml.Serialization;

namespace UI.Extensions
{
	public interface IRange<T> : IXmlSerializable where T : IEquatable<T>, IComparable<T>
	{
		T Min { get; }
		T Max { get; }
		T Clamp(T value);
		bool IsInside(T value, bool inclusive = false);
		bool IsAbove(T value, bool inclusive = false);
		bool IsBelow(T value, bool inclusive = false);
	}
}