using UnityEngine;

namespace UI.Components.Scroll.Contracts
{
	public interface IScrollItem<TData>
	{
		TData LinkedData { get; }
		// assuming the position is anchoredPosition, had been removed here
		RectTransform RectTransform { get; }
		void Resume(TData data);
		void Suspend();
		void SetVisible();
		void SetInvisible();
		void UpdateView();
	}
}
