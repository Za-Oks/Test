using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InAppProduct_Info
{
	public string title = string.Empty;

	public string descr = string.Empty;

	public TextMeshProUGUI priceInApp_Text;

	public Button infoInApp_Button;

	public TextMeshProUGUI priceShop_Text;

	public Button infoShop_Button;

	public void Init(Transform tr_inapp)
	{
		Init(tr_inapp, null);
	}

	public void Init(Transform tr_inapp, Transform tr_shop)
	{
		priceInApp_Text = tr_inapp.Find("Price_Txt").GetComponent<TextMeshProUGUI>();
		infoInApp_Button = tr_inapp.Find("Info_Btn").GetComponent<Button>();
		infoInApp_Button.onClick.AddListener(delegate
		{
			AllReferencesManager.UI_MANAGER.InfoButtonClicked(this);
		});
		if (tr_shop != null)
		{
			priceShop_Text = tr_shop.Find("Price_Txt").GetComponent<TextMeshProUGUI>();
			infoShop_Button = tr_shop.Find("Info_Btn").GetComponent<Button>();
			infoShop_Button.onClick.AddListener(delegate
			{
				AllReferencesManager.UI_MANAGER.InfoButtonClicked(this);
			});
		}
	}
}
