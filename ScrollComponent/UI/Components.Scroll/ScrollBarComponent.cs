using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Components.Scroll
{
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[SelectionBase]
	public class ScrollBarComponent : UIBehaviour
	{
#if UNITY_EDITOR
		protected override void Reset() { }

		protected override void OnValidate() { }
#endif
	}
}