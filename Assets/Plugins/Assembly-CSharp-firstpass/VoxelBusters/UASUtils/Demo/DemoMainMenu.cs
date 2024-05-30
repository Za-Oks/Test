using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.UASUtils.Demo
{
	public class DemoMainMenu : DemoGUIWindow
	{
		private Dictionary<string, List<DemoSubMenu>> m_subMenuCollection = new Dictionary<string, List<DemoSubMenu>>();

		private DemoSubMenu m_currentSubMenu;

		protected override void Start()
		{
			base.Start();
			CollectSubMenuItems();
			DisableAllSubMenus();
		}

		private void Update()
		{
			if (m_currentSubMenu != null && !m_currentSubMenu.gameObject.activeSelf)
			{
				m_currentSubMenu = null;
			}
		}

		private void CollectSubMenuItems()
		{
			DemoSubMenu[] componentsInChildren = GetComponentsInChildren<DemoSubMenu>(true);
			DemoSubMenu[] array = componentsInChildren;
			foreach (DemoSubMenu demoSubMenu in array)
			{
				string key = demoSubMenu.transform.parent.name;
				List<DemoSubMenu> value;
				if (!m_subMenuCollection.TryGetValue(key, out value))
				{
					value = new List<DemoSubMenu>();
					m_subMenuCollection[key] = value;
				}
				value.Add(demoSubMenu);
				if (base.UISkin != null && demoSubMenu.UISkin == null)
				{
					demoSubMenu.UISkin = base.UISkin;
				}
			}
		}

		private void DisableAllSubMenus()
		{
			foreach (List<DemoSubMenu> value in m_subMenuCollection.Values)
			{
				foreach (DemoSubMenu item in value)
				{
					item.gameObject.SetActive(false);
				}
			}
		}

		private void EnableSubMenu(DemoSubMenu _enabledSubMenu)
		{
			DisableAllSubMenus();
			_enabledSubMenu.gameObject.SetActive(true);
			m_currentSubMenu = _enabledSubMenu;
		}

		protected override void OnGUIWindow()
		{
			if (!(m_currentSubMenu == null))
			{
				return;
			}
			base.RootScrollView.BeginScrollView();
			foreach (string key in m_subMenuCollection.Keys)
			{
				GUILayout.Box(key);
				foreach (DemoSubMenu item in m_subMenuCollection[key])
				{
					if (GUILayout.Button(item.name))
					{
						EnableSubMenu(item);
						break;
					}
				}
			}
			base.RootScrollView.EndScrollView();
			GUILayout.FlexibleSpace();
		}
	}
}
