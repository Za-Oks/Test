using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
	private static DebugCanvas INSTANCE;

	public Text text;

	public static Text m_text;

	private static string old_msg = string.Empty;

	private static int counter = 1;

	private static bool debug;

	private void Awake()
	{
		if (INSTANCE == null)
		{
			INSTANCE = this;
			Object.DontDestroyOnLoad(base.gameObject);
			m_text = text;
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void ClearText()
	{
		text.text = string.Empty;
	}

	public static void Log(string msg)
	{
		if (!debug)
		{
			return;
		}
		if (m_text != null)
		{
			if (old_msg == msg)
			{
				counter++;
				if (counter == 2)
				{
					m_text.text += " (2)";
				}
				else
				{
					m_text.text = ReplaceLastOccurrence(m_text.text, "(" + (counter - 1) + ")", "(" + counter + ")");
				}
			}
			else
			{
				counter = 1;
				Text obj = m_text;
				obj.text = obj.text + "\n-> " + msg;
				old_msg = msg;
			}
		}
		Debug.Log(msg);
	}

	public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
	{
		int num = Source.LastIndexOf(Find);
		if (num == -1)
		{
			return Source;
		}
		return Source.Remove(num, Find.Length).Insert(num, Replace);
	}
}
