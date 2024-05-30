using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel_Manager : MonoBehaviour
{
	[Header("References")]
	public Audio_Manager audio_Manager;

	[Header("RectTransforms")]
	public RectTransform Info_Rect;

	[Header("Buttons")]
	public Button info_Btn;

	[Header("Stas Texts")]
	public TextMeshProUGUI troopInfo_Txt;

	public TextMeshProUGUI troopHealth_Txt;

	public TextMeshProUGUI troopDamage_Txt;

	public TextMeshProUGUI troopAttackSpeed_Txt;

	public TextMeshProUGUI troopMovementSpeed_Txt;

	public TextMeshProUGUI troopHitDistance_Txt;

	public TextMeshProUGUI troopPenetration_Txt;

	[Header("Values")]
	public Sprite inInfo;

	public Sprite outInfo;

	private float time = 0.25f;

	private bool infoState;

	private Vector3 StartPosition;

	private Vector3 LeftOutPosition;

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		info_Btn.onClick.AddListener(delegate
		{
			InfoPanelAnimation();
			audio_Manager.MenuClick();
		});
		InitializePositions();
		ResetInfoPanel();
	}

	private void InitializePositions()
	{
		CanvasScaler componentInChildren = Info_Rect.root.GetComponentInChildren<CanvasScaler>(true);
		Vector2 size = Info_Rect.rect.size;
		float num = 5f;
		float num2 = (float)Screen.width / componentInChildren.referenceResolution.x;
		StartPosition = new Vector3(Info_Rect.position.x, Info_Rect.position.y, Info_Rect.position.z);
		LeftOutPosition = new Vector3((0f - (size.x + num)) * num2, StartPosition.y, StartPosition.z);
	}

	private void InfoPanelAnimation()
	{
		float duration = 0.4f;
		if (infoState)
		{
			Info_Rect.DOKill();
			Info_Rect.DOMove(LeftOutPosition, duration).SetEase(Ease.OutCirc);
			info_Btn.GetComponent<Image>().sprite = inInfo;
			infoState = false;
		}
		else
		{
			Info_Rect.DOKill();
			Info_Rect.DOMove(StartPosition, duration).SetEase(Ease.OutCirc);
			info_Btn.GetComponent<Image>().sprite = outInfo;
			infoState = true;
		}
	}

	public void InfoPanelOpen()
	{
		if (!infoState)
		{
			Info_Rect.DOKill();
			Info_Rect.DOMove(StartPosition, time).SetEase(Ease.OutCirc);
			info_Btn.GetComponent<Image>().sprite = outInfo;
			infoState = true;
		}
	}

	public void InfoPanelClose()
	{
		if (infoState)
		{
			Info_Rect.DOKill();
			Info_Rect.DOMove(LeftOutPosition, time).SetEase(Ease.OutCirc);
			info_Btn.GetComponent<Image>().sprite = inInfo;
			infoState = false;
		}
	}

	public void ResetInfoPanel()
	{
		infoState = true;
		Info_Rect.position = StartPosition;
		info_Btn.GetComponent<Image>().sprite = outInfo;
	}
}
