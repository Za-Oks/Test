using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelStats_Gui
{
	[Header("Shop Default Items")]
	public GameObject[] defaultItems;

	[Header("Shop Mask Items")]
	public ScrollRect maskScroll;

	public RectTransform scrollView;

	public RectTransform img1_Row1;

	public RectTransform img2_Row1;

	public RectTransform img1_Row2;

	[Header("Values")]
	public Sprite completed;

	public Sprite incompleted;

	public Vector2 img_Size { get; set; }

	public float imgDiffY { get; set; }

	public float img1PosX { get; set; }

	public float img2PosX { get; set; }

	public float imgY { get; set; }

	public void InitializeImagesValues()
	{
		img_Size = img1_Row1.rect.size;
		imgDiffY = Mathf.Abs(img1_Row2.localPosition.y - img1_Row1.localPosition.y);
		img1PosX = img1_Row1.localPosition.x;
		img2PosX = img2_Row1.localPosition.x;
		imgY = img1_Row1.localPosition.y;
	}

	public void CloseDefault_Items()
	{
		GameObject[] array = defaultItems;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(false);
		}
	}
}
