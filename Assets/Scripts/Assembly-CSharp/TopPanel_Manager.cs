using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel_Manager : MonoBehaviour
{
	[Header("References")]
	public Audio_Manager audio_Manager;

	[Header("RectTransforms")]
	public RectTransform TopPanel_Rect;

	[Header("Buttons")]
	public Button topPanel_Btn;

	public Button lockPanel_Btn;

	public Image topPanel_Image;

	[Header("Values")]
	public Sprite inTopPanel;

	public Sprite outTopPanel;

	public Sprite lockPanel;

	public Sprite unlockPanel;

	private float time = 0.25f;

	private bool isLocked;

	private bool topPanelState;

	private Vector3 StartPosition;

	private Vector3 TopOutPosition;

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		isLocked = PlayerPrefs_Settings.LoadLockedTopPanel();
		topPanel_Btn.onClick.AddListener(delegate
		{
			TopPanelAnimation();
			audio_Manager.MenuClick();
		});
		lockPanel_Btn.onClick.AddListener(delegate
		{
			SetStateLock();
			SetLock();
			audio_Manager.MenuClick();
		});
		InitializePositions();
		ResetTopPanelPanel();
		SetLock();
	}

	private void InitializePositions()
	{
		CanvasScaler componentInChildren = TopPanel_Rect.root.GetComponentInChildren<CanvasScaler>(true);
		Vector2 size = TopPanel_Rect.rect.size;
		float num = 0f;
		float num2 = (float)Screen.width / componentInChildren.referenceResolution.x;
		StartPosition = new Vector3(TopPanel_Rect.position.x, TopPanel_Rect.position.y, TopPanel_Rect.position.z);
		TopOutPosition = new Vector3(StartPosition.x, (float)Screen.height + (size.y + num) * num2, StartPosition.z);
		TopPanel_Rect.parent.parent.gameObject.SetActive(true);
	}

	private void SetStateLock()
	{
		if (topPanelState)
		{
			isLocked = !isLocked;
			PlayerPrefs_Settings.SaveLockedTopPanel(isLocked);
		}
	}

	private void SetLock()
	{
		if (isLocked)
		{
			lockPanel_Btn.image.sprite = lockPanel;
			topPanel_Btn.gameObject.SetActive(false);
		}
		else
		{
			lockPanel_Btn.image.sprite = unlockPanel;
			topPanel_Btn.gameObject.SetActive(true);
		}
	}

	private void TopPanelAnimation()
	{
		if (!isLocked)
		{
			float duration = 0.4f;
			if (topPanelState)
			{
				TopPanel_Rect.DOKill();
				TopPanel_Rect.DOMove(TopOutPosition, duration).SetEase(Ease.OutCirc);
				topPanel_Image.sprite = inTopPanel;
				topPanelState = false;
			}
			else
			{
				TopPanel_Rect.DOKill();
				TopPanel_Rect.DOMove(StartPosition, duration).SetEase(Ease.OutCirc);
				topPanel_Image.sprite = outTopPanel;
				topPanelState = true;
			}
		}
	}

	public void TopPanelOpen()
	{
		if (!isLocked && !topPanelState)
		{
			TopPanel_Rect.DOKill();
			TopPanel_Rect.DOMove(StartPosition, time).SetEase(Ease.OutCirc);
			topPanel_Image.sprite = outTopPanel;
			topPanelState = true;
		}
	}

	public void TopPanelClose()
	{
		if (!isLocked && topPanelState)
		{
			TopPanel_Rect.DOKill();
			TopPanel_Rect.DOMove(TopOutPosition, time).SetEase(Ease.OutCirc);
			topPanel_Image.sprite = inTopPanel;
			topPanelState = false;
		}
	}

	public void ResetTopPanelPanel()
	{
		topPanelState = true;
		TopPanel_Rect.position = StartPosition;
		topPanel_Image.sprite = outTopPanel;
	}
}
