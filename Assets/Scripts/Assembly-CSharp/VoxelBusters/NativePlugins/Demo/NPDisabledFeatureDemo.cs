using UnityEngine;
using VoxelBusters.UASUtils.Demo;

namespace VoxelBusters.NativePlugins.Demo
{
	public class NPDisabledFeatureDemo : DemoSubMenu
	{
		private string m_enableFeatureInfoText;

		protected override void Start()
		{
			base.Start();
			string arg = base.gameObject.name;
			m_enableFeatureInfoText = string.Format("For accessing {0} functionalities, you must enable this feature in NPSettings->Application Settings->Supported Features. Once you are done, please don't forget to save the changes.", arg);
		}

		protected override void OnGUIWindow()
		{
			base.OnGUIWindow();
			GUILayout.Box(m_enableFeatureInfoText);
			GUILayout.FlexibleSpace();
			DrawPopButton();
		}
	}
}
