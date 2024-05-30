using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BotFly : MonoBehaviour
{
	private AllReferencesManager referencesManager;

	private Transform thisTR;

	private List<Vector3> points;

	private void Awake()
	{
		referencesManager = GetComponent<AllReferencesManager>();
		thisTR = GetComponent<Transform>();
	}

	public void Begin()
	{
		SetupStartingPoints();
		if (points.Count == 0)
		{
			Completed2();
			return;
		}
		thisTR.DOPath(points.ToArray(), GetTimeFromDistance(points), PathType.CatmullRom).SetLookAt(0f).SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				Completed();
			});
		referencesManager.FlyBegan();
	}

	private void Completed()
	{
		points = new List<Vector3>();
		points.Add(thisTR.position);
		Vector3 item = thisTR.position + thisTR.forward.normalized * 20f;
		item.y = 0f;
		points.Add(item);
		thisTR.DOPath(points.ToArray(), GetTimeFromDistance(points) / 2f, PathType.Linear, PathMode.Ignore).SetLookAt(0f).SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				Completed2();
			});
	}

	private void Completed2()
	{
		thisTR.rotation = Quaternion.Euler(0f, thisTR.rotation.eulerAngles.y, 0f);
		referencesManager.FlyEnded();
	}

	private void SetupStartingPoints()
	{
		List<FlyHeatMap> flyPath = AllReferencesManager.GAME_CONTROLLER.GetFlyPath(referencesManager.botMovement.isPlayer);
		List<FlyHeatMap> list = new List<FlyHeatMap>();
		points = new List<Vector3>();
		int num = 0;
		bool flag = false;
		FlyHeatMap flyHeatMap = null;
		int count = flyPath.Count;
		for (int i = 0; i < count; i++)
		{
			float num2 = Vector2.Distance(new Vector2(flyPath[i].xPos, flyPath[i].zPos), new Vector2(thisTR.position.x, thisTR.position.z));
			float num3 = ((flyHeatMap != null) ? Vector2.Distance(new Vector2(flyPath[i].xPos, flyPath[i].zPos), new Vector2(flyHeatMap.xPos, flyHeatMap.zPos)) : 999f);
			if (num2 > AllReferencesManager.GAME_CONTROLLER.blockSizeX * 3f && num3 > AllReferencesManager.GAME_CONTROLLER.blockSizeX)
			{
				if (flyPath[i].percent >= 0.2f)
				{
					flag = true;
				}
				else if (num == 1 && num3 < AllReferencesManager.GAME_CONTROLLER.blockSizeX * 2f)
				{
					flag = true;
				}
				else if (flyPath[i].percent >= 0.1f && num <= 2)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					flyPath[i].distanceFromDragon = num2;
					list.Add(flyPath[i]);
					num++;
					flyHeatMap = flyPath[i];
				}
				else if (num > 1)
				{
					break;
				}
			}
		}
		list = list.OrderBy((FlyHeatMap o) => o.distanceFromDragon).ToList();
		count = list.Count;
		for (int j = 0; j < count; j++)
		{
			points.Add(new Vector3(list[j].xPos, 5f, list[j].zPos));
		}
		if (points.Count != 0)
		{
			Vector3 item = points[count - 1] + (thisTR.position - points[count - 1]).normalized * 10f;
			item.y = 5f;
			points.Add(item);
		}
	}

	private float GetTimeFromDistance(List<Vector3> points)
	{
		float num = 0f;
		float num2 = 6f;
		int count = points.Count;
		for (int i = 1; i < count; i++)
		{
			num += Vector3.Distance(points[i - 1], points[i]);
		}
		return num / num2;
	}

	public void ResetEverything()
	{
		thisTR.DOKill();
		Completed2();
	}
}
