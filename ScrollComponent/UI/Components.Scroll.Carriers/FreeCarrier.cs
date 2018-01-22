using UI.Components.Scroll.Contracts;
using UI.Extensions;
using UnityEngine;

namespace UI.Components.Scroll.Carriers
{
	public class FreeCarrier<TData> : CarrierBase<TData> where TData : class
	{
		private const float STOP_TIME_F = 1f;

		private readonly VectorGeneric2 _target;
		private readonly Vector2 _acceleration;
		private Vector2 _delta;
		private bool _isAtTarget;
		private float _passedDist;
		private readonly float _necessaryDist;

		public FreeCarrier(IScrollComponent<TData> component)
		{
			//var delta = component.PointerEventData.delta.ProjectTo(component.GrowDirection);
			////! temporal - item height limit speed
			//_delta = delta.magnitude > ScrollerExtensions.SCROLL_SPEED_LIMIT_F ? delta.normalized * ScrollerExtensions.SCROLL_SPEED_LIMIT_F : delta;
			//var cutting = new Straight { Direction = _delta.normalized, };
			//_isAtTarget = !component.RectTransform.GetIntersectionInLocalSpace(cutting, out _target);
			//var item = component.FirstOrDefault(_ => _.RectTransform.IsInsideParentSpace(_target.Origin)) ?? (Vector2.Dot(_delta, component.GrowDirection) > 0 ? component.First() : component.Last());

			//VectorGeneric2 @internal;
			//item.RectTransform.GetIntersectionInParentSpace(cutting, out @internal);

			//_necessaryDist = (_target.Origin - @internal.Origin).magnitude;
			//_acceleration = -_delta.normalized * (_necessaryDist / (STOP_TIME_F * STOP_TIME_F) + _delta.magnitude / STOP_TIME_F);

			////ScrollerExtensions.MultiLine(component.RectTransform.GetSidesInLocalSpace(), Color.yellow * .5f, true, 1f);
			////ScrollerExtensions.Cross(_target.Origin, Color.yellow, 30f, 1f);
			////ScrollerExtensions.Cross(@internal.Origin, Color.yellow, 30f, 1f);

			////Debug.Log(string.Format("speed: {0}, acceleration: {1}, distance is: {2}", _delta, _acceleration, _necessuaryDist));
		}

		protected override void Update(IScrollComponent<TData> component)
		{
			//if(_isAtTarget)
			//{
			//	component.CarrierFactory.SetDefaultCarrier(component);
			//	return;
			//}

			//var direction = new Straight { Direction = component.GrowDirection };
			//VectorGeneric2 intersection;
			//if(!component.RectTransform.GetIntersectionInLocalSpace(direction, out intersection))
			//	return;

			//if(Vector2.Dot(_delta, component.GrowDirection) > 0)
			//{
			//	if(!component.First().RectTransform.IsInsideParentSpace(intersection.Origin) && component.RectTransform.IsOverlapsChild(component.First().RectTransform))
			//	{
			//		component.ShiftWindowUp();
			//	}
			//}
			//else
			//{
			//	if(!component.Last().RectTransform.IsInsideParentSpace(intersection.Target) && component.RectTransform.IsOverlapsChild(component.Last().RectTransform))
			//	{
			//		component.ShiftWindowDown();
			//	}
			//}

			//var delta = _passedDist + _delta.magnitude < _necessaryDist ? _delta : _delta.normalized * (_necessaryDist - _passedDist);
			//foreach(var item in component)
			//	item.RectTransform.anchoredPosition += delta;

			//_passedDist += delta.magnitude;
			//_delta = delta + Time.deltaTime * _acceleration;
			//_isAtTarget = _necessaryDist - _passedDist <= 0f;

			////Debug.Log(string.Format("speed: {0} nDis: {1} pDis: {2} delta: {3}", _delta.magnitude, _necessuaryDist, _passedDist, delta.magnitude));
		}
	}
}