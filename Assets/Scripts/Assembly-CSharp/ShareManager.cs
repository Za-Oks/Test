using UnityEngine;

public class ShareManager : MonoBehaviour
{
	private PackagesManager packagesManager;

	private int currentScore;

	private string text = "I just passed level SCORE_HERE on Epic Battle Simulator 2! That was so cool! ";

	private Texture2D screenTexture;

	private void Start()
	{
		if (GameObject.FindWithTag("PackagesManager") != null)
		{
			packagesManager = GameObject.FindWithTag("PackagesManager").GetComponent<PackagesManager>();
		}
	}

	public void Share(int score)
	{
	}

	private string BuildMessage()
	{
		string text = this.text.Replace("SCORE_HERE", currentScore + string.Empty);
		return text + packagesManager.GetShareURL();
	}
}
