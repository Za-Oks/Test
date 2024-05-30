using UnityEngine;

namespace VoxelBusters.Utility
{
	public class GUIScrollView : MonoBehaviour
	{
		private Vector2 m_scrollPosition = Vector2.zero;

		private float m_scrollSpeed = 5f;

		private Rect m_rect = new Rect(0f, 0f, 0f, 0f);

		public void BeginScrollView(GUIStyle _style, params GUILayoutOption[] _options)
		{
			m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, _style, _options);
		}

		public void BeginScrollView(params GUILayoutOption[] _options)
		{
			BeginScrollView(GUI.skin.scrollView, _options);
		}

		public void EndScrollView()
		{
			GUILayout.EndScrollView();
			if (Event.current.type == EventType.Repaint)
			{
				m_rect = GUILayoutUtility.GetLastRect();
			}
		}

		public void Reset()
		{
			m_scrollPosition = Vector2.zero;
		}

		private void Update()
		{
			UpdateScroll();
		}

		private void UpdateScroll()
		{
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				Touch touch = touches[i];
				Vector2 position = touch.position;
				position.y = (float)Screen.height - position.y;
				if (touch.phase == TouchPhase.Moved && m_rect.Contains(position))
				{
					m_scrollPosition += touch.deltaPosition * m_scrollSpeed;
					break;
				}
			}
		}
	}
}
