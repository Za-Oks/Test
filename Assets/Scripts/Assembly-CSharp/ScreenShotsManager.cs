using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenShotsManager : MonoBehaviour
{
	public AspectRatio aspectIcon = new AspectRatio(1024, 1024, "Icon");

	public AspectRatio aspectIcon512 = new AspectRatio(512, 512, "Icon512");

	public AspectRatio aspectIcon312x390 = new AspectRatio(312, 390, "Icon312x390");

	public AspectRatio aspectFeature = new AspectRatio(1024, 500, "Feature");

	public AspectRatio aspectAndroidPortrait = new AspectRatio(1600, 2560, "Android Portrait");

	public AspectRatio aspectAndroidLandscape = new AspectRatio(2560, 1600, "Android Landscape");

	public AspectRatio aspectIPhonePortrait = new AspectRatio(9, 16, "iPhone Portrait");

	public AspectRatio aspectIPhoneLandscape = new AspectRatio(16, 9, "iPhone Landscape");

	public AspectRatio aspectIPadPortrait = new AspectRatio(3, 4, "iPad Portrait");

	public AspectRatio aspectIPadLandscape = new AspectRatio(4, 3, "iPad Pro Landscape");

	[HideInInspector]
	public AspectRatio aspectGameview;

	[Header("Screen Values")]
	public Resolutions resolution;

	public Orientation orienation;

	[Header("Values")]
	public bool isTransparent;

	public bool auto;

	public float time;

	[Header("Extra GOs")]
	public GameObject[] ExtraGOs;

	public Camera m_camera;

	private int screenShotsNumber;

	private float tempTime;

	private string pathIcon = string.Empty;

	private string pathIcon512 = string.Empty;

	private string pathIcon312x390 = string.Empty;

	private string pathFeature = string.Empty;

	private string pathAndroid = string.Empty;

	private string path55 = string.Empty;

	private string pathiPadPro = string.Empty;

	private string screenshot = string.Empty;

	private void Awake()
	{
		GameViewValues();
		SetPaths();
		for (int i = 0; i < ExtraGOs.Length; i++)
		{
			ExtraGOs[i].SetActive(true);
		}
	}

	private void Start()
	{
		SetCanvasToCamera();
		QualitySettings.SetQualityLevel(5, true);
	}

	private void Update()
	{
		if (auto)
		{
			tempTime += Time.deltaTime;
			if (tempTime >= time)
			{
				tempTime = 0f;
				ScreenShotLogic();
			}
		}
		if (Input.GetMouseButtonDown(2) || Input.GetKeyDown("space"))
		{
			ScreenShotLogic();
		}
		if (Input.GetKeyDown("p") && Time.timeScale == 1f)
		{
			Time.timeScale = 0f;
		}
		else if (Input.GetKeyDown("p") && Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
	}

	public void GameViewValues()
	{
		if (resolution == Resolutions.Icon)
		{
			aspectGameview = aspectIcon;
		}
		else if (resolution == Resolutions.Icon512)
		{
			aspectGameview = aspectIcon512;
		}
		else if (resolution == Resolutions.Icon312x390)
		{
			aspectGameview = aspectIcon312x390;
		}
		else if (resolution == Resolutions.Feature)
		{
			aspectGameview = aspectFeature;
		}
		else if (resolution == Resolutions.Android && orienation == Orientation.Portrait)
		{
			aspectGameview = aspectAndroidPortrait;
		}
		else if (resolution == Resolutions.Android && orienation == Orientation.Landscape)
		{
			aspectGameview = aspectAndroidLandscape;
		}
		else if (resolution == Resolutions.iPhone && orienation == Orientation.Portrait)
		{
			aspectGameview = aspectIPhonePortrait;
		}
		else if (resolution == Resolutions.iPhone && orienation == Orientation.Landscape)
		{
			aspectGameview = aspectIPhoneLandscape;
		}
		else if (resolution == Resolutions.iPad && orienation == Orientation.Portrait)
		{
			aspectGameview = aspectIPadPortrait;
		}
		else if (resolution == Resolutions.iPad && orienation == Orientation.Landscape)
		{
			aspectGameview = aspectIPadLandscape;
		}
	}

	private void SetPaths()
	{
		pathIcon = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Icon");
		pathIcon512 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Icon512");
		pathIcon312x390 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Icon312x390");
		pathFeature = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Feature");
		pathAndroid = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Android");
		path55 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "5.5 Inch");
		pathiPadPro = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "iPad Pro");
	}

	private void SetCanvasToCamera()
	{
		if (GameObject.Find("Canvas") != null)
		{
			Canvas component = GameObject.Find("Canvas").GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = m_camera;
			component.planeDistance = 1f;
		}
	}

	private void SetScreenShotName()
	{
		screenshot = "\\Screenshot_" + screenShotsNumber + ".png";
	}

	private void SetDirectory(string path)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
	}

	public void ScreenShotLogic()
	{
		if (resolution == Resolutions.Icon)
		{
			Icon();
		}
		else if (resolution == Resolutions.Icon512)
		{
			Icon512();
		}
		else if (resolution == Resolutions.Icon312x390)
		{
			Icon312x390();
		}
		else if (resolution == Resolutions.Feature)
		{
			Feature();
		}
		else if (resolution == Resolutions.Android)
		{
			if (orienation == Orientation.Portrait)
			{
				Android_Portait();
			}
			else if (orienation == Orientation.Landscape)
			{
				Android_Landscape();
			}
		}
		else if (resolution == Resolutions.iPhone || resolution == Resolutions.iPad)
		{
			if (orienation == Orientation.Portrait)
			{
				iPhone_Portait();
			}
			else if (orienation == Orientation.Landscape)
			{
				iPhone_Landscape();
			}
		}
	}

	private void Icon()
	{
		SetScreenShotName();
		SetDirectory(pathIcon);
		TakeScreenShot(pathIcon + screenshot, 1024, 1024);
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void Icon512()
	{
		SetScreenShotName();
		SetDirectory(pathIcon512);
		TakeScreenShot(pathIcon512 + screenshot, 512, 512);
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void Icon312x390()
	{
		SetScreenShotName();
		SetDirectory(pathIcon312x390);
		TakeScreenShot(pathIcon312x390 + screenshot, 312, 390);
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void Feature()
	{
		SetScreenShotName();
		SetDirectory(pathFeature);
		TakeScreenShot(pathFeature + screenshot, 1024, 500);
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void iPhone_Portait()
	{
		SetScreenShotName();
		if (resolution == Resolutions.iPhone)
		{
			SetDirectory(path55);
			TakeScreenShot(path55 + screenshot, 1242, 2208);
		}
		else if (resolution == Resolutions.iPad)
		{
			SetDirectory(pathiPadPro);
			TakeScreenShot(pathiPadPro + screenshot, 2048, 2732);
		}
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void iPhone_Landscape()
	{
		SetScreenShotName();
		if (resolution == Resolutions.iPhone)
		{
			SetDirectory(path55);
			TakeScreenShot(path55 + screenshot, 2208, 1242);
		}
		else if (resolution == Resolutions.iPad)
		{
			SetDirectory(pathiPadPro);
			TakeScreenShot(pathiPadPro + screenshot, 2732, 2048);
		}
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void Android_Portait()
	{
		SetScreenShotName();
		SetDirectory(pathAndroid);
		TakeScreenShot(pathAndroid + screenshot, 1600, 2560);
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void Android_Landscape()
	{
		SetScreenShotName();
		SetDirectory(pathAndroid);
		TakeScreenShot(pathAndroid + screenshot, 2560, 1600);
		MonoBehaviour.print("Screenshot_" + screenShotsNumber + "  was taken");
		screenShotsNumber++;
	}

	private void TakeScreenShot(string filename, int resWidthN, int resHeightN)
	{
		RenderTexture renderTexture = new RenderTexture(resWidthN, resHeightN, 24);
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			camera.targetTexture = renderTexture;
		}
		TextureFormat format = ((!isTransparent) ? TextureFormat.RGB24 : TextureFormat.ARGB32);
		Texture2D texture2D = new Texture2D(resWidthN, resHeightN, format, false);
		Camera[] allCameras2 = Camera.allCameras;
		foreach (Camera camera2 in allCameras2)
		{
			camera2.Render();
		}
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, resWidthN, resHeightN), 0, 0);
		Camera[] allCameras3 = Camera.allCameras;
		foreach (Camera camera3 in allCameras3)
		{
			camera3.targetTexture = null;
		}
		RenderTexture.active = null;
		byte[] bytes = texture2D.EncodeToPNG();
		File.WriteAllBytes(filename, bytes);
	}

	private IEnumerator ReadPixelScreenShot_CR(ScaleMethod scaleMethod, string path, int width, int height)
	{
		yield return new WaitForEndOfFrame();
		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		texture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
		switch (scaleMethod)
		{
		case ScaleMethod.Point:
			TextureScale.Point(texture, width, height);
			break;
		case ScaleMethod.Bilinear:
			TextureScale.Bilinear(texture, width, height);
			break;
		}
		yield return null;
		byte[] bytes = texture.EncodeToPNG();
		File.WriteAllBytes(path, bytes);
		UnityEngine.Object.DestroyObject(texture);
	}
}
