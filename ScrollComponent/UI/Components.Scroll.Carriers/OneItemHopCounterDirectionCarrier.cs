using UI.Components.Scroll.Contracts;
using UI.Extensions;
using UnityEngine;

namespace UI.Components.Scroll.Carriers
{
	public class OneItemHopCounterDirectionCarrier<TData> : CarrierBase<TData> where TData : class
	{
		private const float TIME_MAX_F = .2f;

		private VectorGeneric2 _path;
		private Vector2 _distance;
		private float _time = TIME_MAX_F;

		public OneItemHopCounterDirectionCarrier(IScrollComponent<TData> component)
		{
			var last = component.VisibleWindow.Last;
			last?.RectTransform.GetIntersectionInParentSpace(new Straight { Direction = GrowDirection }, out _path);
		}

		protected override void Update(IScrollComponent<TData> component)
		{
			var time = _time - Time.deltaTime;
			var normalized = 1f - Mathf.Clamp01(time / TIME_MAX_F);
			var offset = (1f - Mathf.Cos(Mathf.PI * normalized)) * .5f * _path.Direction;
			MoveAll(component, offset - _distance);
			// Debug.Log(string.Format("<color=magenta>moving UP : [ time: {0}, distance: {1}, offset: {2}]</color>", _time, _distance, offset));
			if(_time < 0f)
				component.CarrierFactory.SetDefaultCarrier(component);
			_time = time;
			Shift(component, offset - _distance);
			_distance = offset;
		}
	}
}