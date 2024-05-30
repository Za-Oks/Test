using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
	[Header("Panels")]
	public GameObject termsPanel;

	[Header("Sliders")]
	public Image loadingBar;

	[Header("Buttons")]
	public Button terms_Btn;

	public Button accept_Btn;

	private void Awake()
	{
		PlayerPrefs_Saves.CreateTermsAccepted();
	}

	private void Start()
	{
		terms_Btn.onClick.AddListener(delegate
		{
			Application.OpenURL("http://rappidstudios.com/index.php/privacy-policy");
		});
		accept_Btn.onClick.AddListener(delegate
		{
			AcceptTerms();
		});
		if (PlayerPrefs_Saves.LoadTermsAccepted())
		{
			Loading();
		}
		else
		{
			SetTermsPanel(true);
		}
	}

	private void SetTermsPanel(bool state)
	{
		termsPanel.SetActive(state);
	}

	private void AcceptTerms()
	{
		PlayerPrefs_Saves.SaveTermsAccepted(true);
		PlayerPrefs.Save();
		SetTermsPanel(false);
		Loading();
	}

	private void Loading()
	{
		StartCoroutine("Loading_CR");
	}

	private IEnumerator Loading_CR()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
		while (!async.isDone)
		{
			loadingBar.fillAmount = async.progress;
			yield return null;
		}
	}
}
