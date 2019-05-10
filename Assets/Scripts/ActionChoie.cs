using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;

namespace AddressListApp
{
	public class ActionChoice
	{
		public readonly string title;
		public readonly IconData icon;

		public static List<ActionChoice> choiceList = new List<ActionChoice>
	{
		new ActionChoice("同步", Icons.update),
		new ActionChoice("设置", Icons.settings)
	};

		public ActionChoice(string title, IconData icon)
		{
			this.title = title;
			this.icon = icon;
		}
	}
}
