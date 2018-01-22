using UI.Components;
using UI.Components.Scroll.Contracts;
using UnityEngine.UI;

namespace Assets.Scripts.Usage
{
	public class ExampleScrollItemComponent : PanelComponent, IScrollItem<ExampleScrollItemData>
	{
		private Text _text;

		protected override void Awake()
		{
			_text = transform.GetComponentInChildren<Text>();

			base.Awake();
		}

		public ExampleScrollItemData LinkedData { get; private set; }

		public void Resume(ExampleScrollItemData data)
		{
			LinkedData = data;
		}

		public void Suspend()
		{
			LinkedData = null;
		}

		public void UpdateView()
		{
			_text.text = LinkedData.Data;
		}
	}
}