namespace UI.Extensions
{
	public static class RangeExtensions
	{
		private static Range<float> _ident = new Range<float>(0f, 1f);

		public static float Interpolate(this Range<float> source, float value)
		{
			return source.Max - source.Min > 0
				? _ident.Clamp(value / (source.Max - source.Min))
				: 0f;
		}

		public static int Length(this Range<int> source)
		{
			return source.Max - source.Min;
		}

		public static int Length(this IRange<int> source)
		{
			return source.Max - source.Min;
		}
	}
}