using AppTheme;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace AddressListApp
{
	public class AddressListApp : UIWidgetsPanel
	{
		VoidCallback _showBottomSheetCallback;

		protected override Widget createWidget()
		{
			// MaterialApp(Material是一种标准的移动端和web端的视觉设计语言)
			return new MaterialApp(
				showPerformanceOverlay: false,
				title: "通讯录",
				home: new AddressListPage(),
				theme: new ThemeData(primaryColor: Theme1.themePrimaryColor)
			);
		}

		protected override void OnEnable()
		{
			// 载入图标
			FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
			base.OnEnable();
		}
	}
}
