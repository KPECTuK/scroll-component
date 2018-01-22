using System.Collections;
using UI.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
	[RequireComponent(typeof(CanvasGroup))]
// ReSharper disable once UnusedMember.Global
// ReSharper disable once CheckNamespace
	public abstract class PanelComponent : UIBehaviour, ILayoutElement
	{
		// TODO: implement links and RectTransformLink

		// ReSharper disable ConvertToConstant.Local
		[SerializeField] private float _max = 1f;
		[SerializeField] private float _min;
		// ReSharper restore ConvertToConstant.Local

		private CanvasGroup _canvasGroup;
		private RectTransform _rectTransform;
		private Transform _transform;
		//
		protected bool IsVisible;
		[SerializeField] protected float Hight;
		[SerializeField] protected float Width;

		// ReSharper disable once MergeConditionalExpression - unity
		public CanvasGroup CanvasGroup => ReferenceEquals(null, _canvasGroup) ? (_canvasGroup = GetComponent<CanvasGroup>()) : _canvasGroup;
		// ReSharper disable once MergeConditionalExpression - unity
		public RectTransform RectTransform => ReferenceEquals(null,_rectTransform) ? (_rectTransform = GetComponent<RectTransform>()) : _rectTransform;
		// ReSharper disable once MergeConditionalExpression - unity
		public Transform Transform => ReferenceEquals(null, _transform) ? (_transform = GetComponent<Transform>()) : _transform;

		protected void SetLimits(float min, float max)
		{
			if(max < min)
			{
				_max = Mathf.Clamp01(min);
				_min = Mathf.Clamp01(max);
			}
			else
			{
				_max = Mathf.Clamp01(max);
				_min = Mathf.Clamp01(min);
			}
		}

		public virtual void SetVisible()
		{
			IsVisible = true;
		}

		public virtual void SetInvisible()
		{
			IsVisible = false;
		}

		public virtual IEnumerator FadeIn(float duration)
		{
			CanvasGroup.blocksRaycasts = true;

			var speed = duration > 0f ? (_max - CanvasGroup.alpha) / duration : float.MaxValue;
			yield return new WaitWhile(() =>
			{
				CanvasGroup.alpha += speed * Time.deltaTime;
				return CanvasGroup.alpha < _max;
			});
			CanvasGroup.alpha = _max;

			CanvasGroup.interactable = true;
		}

		public virtual IEnumerator FadeOut(float duration)
		{
			CanvasGroup.interactable = false;

			var speed = duration > 0 ? (_min - CanvasGroup.alpha) / duration : float.MinValue;
			yield return new WaitWhile(() =>
			{
				CanvasGroup.alpha += speed * Time.deltaTime;
				return CanvasGroup.alpha > _min;
			});
			CanvasGroup.alpha = _min;

			CanvasGroup.blocksRaycasts = false;
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
			//Width = LayoutUtility.GetMinWidth(RectTransform);
			Width = RectTransform.rect.width;
			Debug.Log(GetType().NameNice() + " >> " + "CalculateLayoutInputHorizontal" + " - " + Width + " " + RectTransform.rect.width);
			DrawDebug();
		}

		public virtual void CalculateLayoutInputVertical()
		{
			//Hight = LayoutUtility.GetPreferredHeight(RectTransform);
			Hight = RectTransform.rect.height;
			Debug.Log(GetType().NameNice() + " >> " + "CalculateLayoutInputVertical" + " - " + Hight + " " + RectTransform.rect.height);
			DrawDebug();
		}

		public float minWidth => IsVisible ? Width : 0;
		public float preferredWidth => IsVisible ? Width : 0;
		public float flexibleWidth => IsVisible ? Width : 0;
		public float minHeight => IsVisible ? Hight : 0;
		public float preferredHeight => IsVisible ? Hight : 0;
		public float flexibleHeight => IsVisible ? Hight : 0;
		public int layoutPriority => -1;

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		protected virtual void DrawDebug()
		{

		}
	}
}