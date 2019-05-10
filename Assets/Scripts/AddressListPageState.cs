using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;
using UnityEngine;
using AppTheme;
using widgetsUi = Unity.UIWidgets.ui;
using Unity.UIWidgets.painting;

namespace AddressListApp
{
	public class AddressListPageState : State<AddressListPage>
	{
		Scaffold mScaffold;
		List<Widget> mList = new List<Widget>();    // 联系人列表
		List<Widget> mEditList = new List<Widget>();    // 编辑页面
		widgetsUi.VoidCallback showEditInfoSheetCallBack;

		// 编辑信息页面的输入控制
		TextEditingController mNameController = new TextEditingController("");
		TextEditingController mTpController = new TextEditingController("");    // Telephone
		TextEditingController mMailController = new TextEditingController("");
		TextEditingController mOtherController = new TextEditingController("");


		// PopupMenuButton(App标题栏右边的按钮)
		PopupMenuButton<ActionChoice> mPopupMenuButton = new PopupMenuButton<ActionChoice>(
			onSelected: (_) => { },
			itemBuilder: (BuildContext context) =>
			{
				List<PopupMenuEntry<ActionChoice>> popupItem = new List<PopupMenuEntry<ActionChoice>>();
				foreach (var item in ActionChoice.choiceList)
				{
					popupItem.Add(new PopupMenuItem<ActionChoice>(
						value: item,
						child: new ListTile(
							leading: new Icon(item.icon),
							title: new Text(item.title)
						))
					);
				}

				return popupItem;
			}
		);

		// --------------------------------------------------

		public override void initState()
		{
			// 从本地读取数据
			//mList.Clear();
			//Debug.Log("mContactList size:" + mContactList.Count);
			//foreach (var item in mContactList)
			//{
			//	ListTile mListTile = new ListTile(
			//		title: new Text(item.Name, style: Theme1.mediumFont),
			//		trailing: new IconButton(
			//		icon: new Icon(Icons.edit),
			//		onPressed: () => Debug.Log("pressed")
			//		),
			//		leading: new Icon(Icons.phone),
			//		subtitle: new Text("上次通话时间:", style: Theme1.smallFont)
			//	);

			//	mList.Add(mListTile);
			//	mList.Add(new Divider(height: 0));
			//}
			showEditInfoSheetCallBack = ShowEditInfoSheet;  // todo 代码写的不好, 之后应该改改
		}

		// build (为什么应用启动时会build两次
		public override Widget build(BuildContext context)
		{
			Debug.Log("build!");
			// todo 如果不对mlist的内存进行重新分配就不会更页面
			mList = new List<Widget>(mList);
			// Scaffold

			mScaffold = new Scaffold(
				appBar: new AppBar(
					leading: new Icon(Icons.book),
					title: new Text("通讯录"),
					actions: new List<Widget>{
						mPopupMenuButton
					}
				),
				body: new ListView(
					children: mList
				),
			floatingActionButton: new FloatingActionButton(
					child: new Icon(Icons.add, size: 35),
					backgroundColor: Theme1.themePrimaryColor,
					onPressed: showEditInfoSheetCallBack
				)
			);

			return mScaffold;
		}

		void ShowEditInfoSheet()
		{
			// 编辑信息页面
			mEditList.Clear();
			// photo
			mEditList.Add(
				new Container(
					child: new Icon(icon: Icons.people, size: 60),
					padding: EdgeInsets.only(top: 20, bottom: 20)
				)
			);
			// infolist
			mEditList.Add(
				new Container(
					child: new Column(
						children: new List<Widget>
						{
							new Divider(height: 35),
							new TextField(
								controller: mNameController,
								decoration: new InputDecoration(hintText:"姓名"),
								autofocus: true
							),
							new Divider(height: 10, color: widgetsUi.Color.fromARGB(0,0,0,0)),
							new TextField(
								controller: mTpController,
								decoration: new InputDecoration(hintText:"电话")
							),
							new Divider(height: 10, color: widgetsUi.Color.fromARGB(0,0,0,0)),
							new TextField(
								controller: mMailController,
								decoration: new InputDecoration(hintText:"邮箱")
							),
							new Divider(height: 10, color: widgetsUi.Color.fromARGB(0,0,0,0)),
							new TextField(
								controller: mOtherController,
								decoration: new InputDecoration(hintText:"备注"),
								maxLines: 5
							),
						}
					),
					margin: EdgeInsets.only(30, 0, 30, 0)
				)
			);
			// submit
			mEditList.Add(
				new Container(
					child: new IconButton(
						icon: new Icon(Icons.check),
						iconSize: 45,
						onPressed: OnSubmitInfo
					),
					margin: EdgeInsets.only(0, 30, 0, 30)
				)
			);

			// 入栈
			Navigator.of(context).push(
				new MaterialPageRoute(
					builder: (context) =>
					{
						return new Scaffold(
							appBar: new AppBar(title: new Text("编辑信息")),
							body: new ListView(
								children: mEditList
							)
						);
					}
				)
			);
		}

		void OnSubmitInfo()
		{
			if (mNameController.text == "")
				return;

			// 出栈
			Navigator.of(context).pop();

			Contact contact = new Contact();
			contact.Name = mNameController.text;
			contact.Telephone = mTpController.text;
			contact.Mail = mMailController.text;
			contact.Other = mOtherController.text;

			mNameController.text = "";
			mTpController.text = "";
			mMailController.text = "";
			mOtherController.text = "";

			AddContact(contact);
		}

		void AddContact(Contact contact)
		{
			mList.Add(
				new ListTile(
					title: new Text(contact.Name, style: Theme1.mediumFont),
					trailing: new IconButton(
						icon: new Icon(Icons.edit),
						onPressed: () => Debug.Log("Edit button Pressed")
					),
					leading: new Icon(Icons.phone),
					subtitle: new Text("上次通话时间:", style: Theme1.smallFont)
				)
			);
		}
	}
}
