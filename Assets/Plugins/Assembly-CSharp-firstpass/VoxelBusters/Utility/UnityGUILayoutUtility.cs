using UnityEngine;

namespace VoxelBusters.Utility
{
	public class UnityGUILayoutUtility : MonoBehaviour
	{
		public static bool Foldout(GUIContent _label, bool _state)
		{
			GUIStyle gUIStyle = new GUIStyle("label");
			gUIStyle.richText = true;
			GUIContent gUIContent = new GUIContent(_label);
			string text = null;
			text = ((!_state) ? "+" : "-");
			gUIContent.text = string.Format("<b>{0} {1} </b>", text, _label.text);
			GUILayout.BeginHorizontal();
			if (!GUILayout.Toggle(true, gUIContent, gUIStyle))
			{
				_state = !_state;
			}
			GUILayout.EndHorizontal();
			return _state;
		}
	}
}
