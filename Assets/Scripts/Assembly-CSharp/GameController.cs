using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[Header("REFERENCES")]
	public Ui_Manager ui_Manager;

	public Level_Manager level_Manager;

	public BasicPoolManager poolManager_ArrowBow;

	public BasicPoolManager poolManager_BulletCannon;

	public BasicPoolManager poolManager_BulletCatapult;

	public CrossPoolManager crossPoolManager;

	[Header("CAMERA SHAKE")]
	public Transform cameraShake;

	public float cameraShake_MinDistance = 5f;

	public float cameraShake_MaxDistance = 20f;

	public float cameraShake_MinStrength = 0.1f;

	public float cameraShake_MaxStrength = 1.5f;

	[Header("BOUNDS")]
	public float bounds_x = 70f;

	public float bounds_z = 50f;

	[Header("FLY")]
	public float sizeX = 100f;

	[Header("FLY")]
	public float sizeZ = 100f;

	private Dictionary<string, BotInfo> bots = new Dictionary<string, BotInfo>();

	private List<BotInfo> player_bots;

	private List<BotInfo> enemy_bots;

	[HideInInspector]
	public bool canPlay;

	private int allPlayerBots;

	private int allEnemyBots;

	private int playerBotsDied;

	private int enemyBotsDied;

	private int allPlayerStationaryBots;

	private int allEnemyStationaryBots;

	private bool stationaryDestroyed;

	private bool stationaryUnreachable;

	private Audio_Manager audioManager;

	[Header("TUTORIAL")]
	public TutorialManager tutorialManager;

	private bool isTutorial;

	[Header("DEBUG")]
	public bool runInBG;

	private WaitForSeconds wait_2s = new WaitForSeconds(2f);

	[HideInInspector]
	public float blockSizeX;

	[HideInInspector]
	public float blockSizeZ;

	private float halfSizeX;

	private float halfSizeZ;

	private float offsetX;

	private float offsetZ;

	private int[,] grid = new int[8, 8];

	private List<FlyHeatMap> flyHeatMap;

	private float cameraShake_lastTime = -100f;

	[HideInInspector]
	public BotMovement lastBotCameraOnHead;

	private void Awake()
	{
		audioManager = GameObject.FindWithTag("Audio_Manager").GetComponent<Audio_Manager>();
		isTutorial = SceneManager.GetActiveScene().name == "TutorialScene";
		halfSizeX = sizeX / 2f;
		halfSizeZ = sizeZ / 2f;
		blockSizeX = sizeX / 8f;
		blockSizeZ = sizeZ / 8f;
		offsetX = 0f - halfSizeX + blockSizeX / 2f;
		offsetZ = 0f - halfSizeZ + blockSizeZ / 2f;
	}

	public void Begin()
	{
		StartCoroutine("UpdateBotDistances");
		audioManager.SwitchBackgroundMusic(BackgroundMusic.FIGHT);
		foreach (BotInfo value in bots.Values)
		{
			value.referencesManager.botMovement.Begin();
			if (value.isPlayer)
			{
				allPlayerBots++;
				if (value.referencesManager.IsStationary())
				{
					allPlayerStationaryBots++;
				}
			}
			else
			{
				allEnemyBots++;
				if (value.referencesManager.IsStationary())
				{
					allEnemyStationaryBots++;
				}
			}
		}
		canPlay = true;
		CheckStationaryUnreachable();
		if (stationaryUnreachable)
		{
			DestroyStationary();
		}
		if (allEnemyBots + allPlayerBots == 0)
		{
			StartCoroutine("AutoGameOver", BattleResult.Draw);
		}
		else if (allEnemyBots == 0)
		{
			if (level_Manager.isPlayer)
			{
				StartCoroutine("AutoGameOver", BattleResult.Win);
			}
			else
			{
				StartCoroutine("AutoGameOver", BattleResult.Defeat);
			}
		}
		else if (allPlayerBots == 0)
		{
			if (level_Manager.isPlayer)
			{
				StartCoroutine("AutoGameOver", BattleResult.Defeat);
			}
			else
			{
				StartCoroutine("AutoGameOver", BattleResult.Win);
			}
		}
	}

	public void SetupLevel(Dictionary<string, BotInfo> bots)
	{
		this.bots = bots;
		player_bots = new List<BotInfo>();
		enemy_bots = new List<BotInfo>();
		foreach (BotInfo value in bots.Values)
		{
			value.referencesManager.botMovement.ResetVariables();
			value.InitStats();
			if (value.isPlayer)
			{
				player_bots.Add(value);
			}
			else
			{
				enemy_bots.Add(value);
			}
		}
	}

	public void DisableBots()
	{
		foreach (BotInfo value in bots.Values)
		{
			value.gameObject.SetActive(false);
		}
	}

	public BotMovement GetBotMovement(string name)
	{
		if (bots.ContainsKey(name))
		{
			return bots[name].referencesManager.botMovement;
		}
		return null;
	}

	public BotInfo GetNearestTarget(Vector3 pos, BotInfo localBot)
	{
		if (localBot.closestEnemy != null && !localBot.closestEnemy.enemy.referencesManager.botMovement.isDead)
		{
			return localBot.closestEnemy.enemy;
		}
		return null;
	}

	public void BotDied(BotInfo bot)
	{
		if (bot == null || bot.referencesManager == null)
		{
			return;
		}
		if (!bot.isPlayer)
		{
			enemyBotsDied++;
			if (bot.referencesManager.IsStationary())
			{
				allEnemyStationaryBots--;
			}
		}
		else
		{
			playerBotsDied++;
			if (bot.referencesManager.IsStationary())
			{
				allPlayerStationaryBots--;
			}
		}
		if (!stationaryDestroyed)
		{
			CheckStationaryUnreachable();
		}
		if (stationaryUnreachable)
		{
			DestroyStationary();
		}
		else if (enemyBotsDied == allEnemyBots || playerBotsDied == allPlayerBots)
		{
			audioManager.SwitchBackgroundMusic(BackgroundMusic.MENU);
			StopCoroutine("DelayGameOver");
			StartCoroutine("DelayGameOver");
		}
		else if (audioManager.CanPlayBackgroundMusic(BackgroundMusic.FIGHT_END) && ((float)enemyBotsDied / (float)allEnemyBots >= 0.7f || (float)playerBotsDied / (float)allPlayerBots >= 0.7f))
		{
			audioManager.SwitchBackgroundMusic(BackgroundMusic.FIGHT_END);
		}
	}

	private IEnumerator UpdateBotDistances()
	{
		int maxLoopsPerFrame = 100;
		int currectLoops = 0;
		bool firstTime = true;
		while (true)
		{
			int playerListLength = player_bots.Count;
			int enemyListLength = enemy_bots.Count;
			int loopsPerFrame = playerListLength * enemyListLength;
			if (loopsPerFrame > maxLoopsPerFrame)
			{
				loopsPerFrame = maxLoopsPerFrame;
			}
			for (int i = 0; i < playerListLength; i++)
			{
				BotInfo player = player_bots[i];
				if (player.referencesManager.botMovement.isDead || player.referencesManager.botMovement.currentMovementState == MovementState.FLY)
				{
					continue;
				}
				SetupClosestEnemies(player);
				for (int j = 0; j < enemyListLength; j++)
				{
					BotInfo enemy = enemy_bots[j];
					if (enemy.referencesManager.botMovement.isDead || enemy.referencesManager.botMovement.currentMovementState == MovementState.FLY)
					{
						continue;
					}
					SetupClosestEnemies(enemy);
					float dist = CalculateDistance(player, enemy);
					CheckPutClosestTarget(player, enemy, dist);
					CheckPutClosestTarget(enemy, player, dist);
					if (!firstTime)
					{
						currectLoops++;
						if (currectLoops >= loopsPerFrame)
						{
							currectLoops = 0;
							yield return null;
						}
					}
				}
			}
			firstTime = false;
			yield return null;
		}
	}

	private void CheckPutClosestTarget(BotInfo toWhom, BotInfo who, float dist)
	{
		bool flag = false;
		if (toWhom.closestEnemy == null)
		{
			flag = true;
		}
		else if (toWhom.closestEnemy.enemy == null)
		{
			flag = true;
		}
		else if (toWhom.closestEnemy.enemy.referencesManager.botMovement == null)
		{
			flag = true;
		}
		else if (toWhom.closestEnemy.enemy.referencesManager.botMovement.isDead || (double)(toWhom.closestEnemy.distance - dist) > 0.1)
		{
			flag = true;
		}
		if (flag)
		{
			toWhom.closestEnemy = new BotDistance(toWhom, who, dist);
		}
	}

	private void SetupClosestEnemies(BotInfo toWhom)
	{
		if (toWhom.closestEnemy != null)
		{
			toWhom.closestEnemy.CalculateDistance();
		}
	}

	public static float CalculateDistance(BotInfo b1, BotInfo b2)
	{
		float num = Vector2.Distance(b1.referencesManager.botMovement.GetPosition(b2.referencesManager.thisTR.position), b2.referencesManager.botMovement.GetPosition(b1.referencesManager.thisTR.position));
		if (b1.referencesManager.botMovement.hasBigCollider)
		{
			num += 0.75f;
		}
		if (b2.referencesManager.botMovement.hasBigCollider)
		{
			num += 0.75f;
		}
		return num;
	}

	public bool IsInsideBounds(float x, float z)
	{
		return x > 0f - bounds_x && x < bounds_x && z > 0f - bounds_z && z < bounds_z;
	}

	private void CheckStationaryUnreachable()
	{
		stationaryUnreachable = false;
		float num = 0f;
		bool flag = false;
		if (allPlayerStationaryBots == 0 || allEnemyStationaryBots == 0 || allPlayerBots - playerBotsDied != allPlayerStationaryBots || allEnemyBots - enemyBotsDied != allEnemyStationaryBots)
		{
			return;
		}
		foreach (BotInfo player_bot in player_bots)
		{
			if (player_bot.referencesManager.botMovement.isDead)
			{
				continue;
			}
			foreach (BotInfo enemy_bot in enemy_bots)
			{
				if (!enemy_bot.referencesManager.botMovement.isDead)
				{
					num = player_bot.referencesManager.botMovement.hitDistance;
					if (enemy_bot.referencesManager.botMovement.hitDistance > num)
					{
						num = enemy_bot.referencesManager.botMovement.hitDistance;
					}
					if (CalculateDistance(player_bot, enemy_bot) <= num)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				continue;
			}
			break;
		}
		if (!flag)
		{
			stationaryUnreachable = true;
		}
	}

	private void DestroyStationary()
	{
		stationaryUnreachable = false;
		stationaryDestroyed = true;
		StartCoroutine("DestroyStationary_CR");
	}

	private IEnumerator DestroyStationary_CR()
	{
		yield return wait_2s;
		foreach (BotInfo value in bots.Values)
		{
			if (!value.referencesManager.botMovement.isDead)
			{
				value.referencesManager.botMovement.ReceiveDamage(99999f, value.referencesManager.thisTR.forward, value.referencesManager.botAttack.force);
			}
		}
	}

	private void GameOver(BattleResult tempResult)
	{
		if (!canPlay)
		{
			return;
		}
		StopCoroutine("DestroyStationary_CR");
		StopCoroutine("UpdateBotDistances");
		foreach (BotInfo value in bots.Values)
		{
			value.referencesManager.botMovement.MakeKinematic();
		}
		if (level_Manager.IsProfile || level_Manager.IsBalance)
		{
			return;
		}
		canPlay = false;
		int levelIndex = level_Manager.levelIndex;
		if (isTutorial)
		{
			tutorialManager.NextStep();
			return;
		}
		ui_Manager.GameOverBegin();
		ui_Manager.SaveGameOver(tempResult);
		ui_Manager.SetGameOver(tempResult);
		ui_Manager.SetConsumablesReward(tempResult);
		ui_Manager.SetConsumablesTexts();
		if (tempResult == BattleResult.Win)
		{
			ui_Manager.OpenRatePopUp(levelIndex);
		}
		audioManager.SwitchBackgroundMusic(BackgroundMusic.MENU);
	}

	private IEnumerator DelayGameOver()
	{
		yield return new WaitForSeconds(2f);
		if (enemyBotsDied == allEnemyBots && playerBotsDied == allPlayerBots)
		{
			GameOver(BattleResult.Draw);
		}
		else if (enemyBotsDied == allEnemyBots)
		{
			if (level_Manager.isPlayer)
			{
				GameOver(BattleResult.Win);
			}
			else
			{
				GameOver(BattleResult.Defeat);
			}
		}
		else if (playerBotsDied == allPlayerBots)
		{
			if (level_Manager.isPlayer)
			{
				GameOver(BattleResult.Defeat);
			}
			else
			{
				GameOver(BattleResult.Win);
			}
		}
	}

	private IEnumerator AutoGameOver(BattleResult result)
	{
		yield return new WaitForSeconds(2f);
		GameOver(result);
	}

	public void ResetEverything()
	{
		canPlay = false;
		StopCoroutine("UpdateBotDistances");
		StopCoroutine("AutoGameOver");
		allEnemyBots = 0;
		allPlayerBots = 0;
		playerBotsDied = 0;
		enemyBotsDied = 0;
		allPlayerStationaryBots = 0;
		allEnemyStationaryBots = 0;
		stationaryDestroyed = false;
		foreach (BotInfo value in bots.Values)
		{
			value.referencesManager.botMovement.ResetEverything();
			value.gameObject.SetActive(false);
		}
		poolManager_ArrowBow.ResetEverything();
		poolManager_BulletCannon.ResetEverything();
		poolManager_BulletCatapult.ResetEverything();
		crossPoolManager.ResetEverything();
	}

	public void Pause(bool pause)
	{
		foreach (BotInfo value in bots.Values)
		{
			value.referencesManager.PauseWeaponEffectAudio(pause);
		}
	}

	private void OnDrawGizmos()
	{
	}

	public List<FlyHeatMap> GetFlyPath(bool isPlayer)
	{
		List<BotInfo> list = ((!isPlayer) ? player_bots : enemy_bots);
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				grid[i, j] = 0;
			}
		}
		int num = 0;
		foreach (BotInfo item in list)
		{
			if (!(item.referencesManager.botMovement == null) && !item.referencesManager.botMovement.isDead && item.referencesManager.botMovement.currentMovementState != MovementState.FLY)
			{
				num++;
				int num2 = (int)((item.referencesManager.transform.position.x + halfSizeX) / blockSizeX);
				int num3 = (int)((item.referencesManager.transform.position.z + halfSizeZ) / blockSizeZ);
				if (num2 < 0)
				{
					num2 = 0;
				}
				else if (num2 > 7)
				{
					num2 = 7;
				}
				if (num3 < 0)
				{
					num3 = 0;
				}
				else if (num3 > 7)
				{
					num3 = 7;
				}
				grid[num2, num3]++;
			}
		}
		flyHeatMap = new List<FlyHeatMap>();
		for (int k = 0; k < 8; k++)
		{
			for (int l = 0; l < 8; l++)
			{
				float xPos = offsetX + (float)k * blockSizeX;
				float zPos = offsetZ + (float)l * blockSizeZ;
				flyHeatMap.Add(new FlyHeatMap((float)grid[k, l] / (float)num, k, l, xPos, zPos));
			}
		}
		flyHeatMap = flyHeatMap.OrderBy((FlyHeatMap o) => o.percent * -1f).ToList();
		return flyHeatMap;
	}

	public void ShakeCamera(Vector3 pos)
	{
		if (Time.time - cameraShake_lastTime < 0f)
		{
			return;
		}
		float num = Vector3.Distance(Camera.main.transform.position, pos);
		if (!(num > cameraShake_MaxDistance))
		{
			if (num < cameraShake_MinDistance)
			{
				num = cameraShake_MinDistance;
			}
			cameraShake_lastTime = Time.time;
			float num2 = cameraShake_MaxDistance - cameraShake_MinDistance;
			num -= cameraShake_MinDistance;
			float strength = Mathf.Lerp(cameraShake_MinStrength, cameraShake_MaxStrength, 1f - num / num2);
			cameraShake.DOShakeRotation(1f, strength);
		}
	}

	public int GetLivingBots(bool player)
	{
		if (player)
		{
			return allPlayerBots - playerBotsDied;
		}
		return allEnemyBots - enemyBotsDied;
	}

	public float GetPercentEnemiesKilled()
	{
		int num = 0;
		foreach (BotInfo value in bots.Values)
		{
			if (!value.isPlayer)
			{
				num++;
			}
		}
		return (float)enemyBotsDied / (float)num;
	}

	public void PutCameraOnBot(string botName)
	{
		if (!bots.ContainsKey(botName))
		{
			return;
		}
		BotInfo botInfo = bots[botName];
		if (bots[botName] != null && !botInfo.referencesManager.botMovement.isDead)
		{
			Transform headCamera = botInfo.referencesManager.botMovement.headCamera;
			if (headCamera != null)
			{
				ui_Manager.game_Gui.cameraHead.Init(headCamera);
				ui_Manager.ChangeCamera(CustomCameraType.Head);
				RemoveHeadCameraFromBot();
				botInfo.referencesManager.botMovement.hasCameraOnHead = true;
				lastBotCameraOnHead = botInfo.referencesManager.botMovement;
			}
		}
	}

	public void RemoveHeadCameraFromBot()
	{
		if (lastBotCameraOnHead != null)
		{
			lastBotCameraOnHead.hasCameraOnHead = false;
		}
	}

	public void RushGameOver(BattleResult result)
	{
		GameOver(result);
	}

	public void Win()
	{
		GameOver(BattleResult.Win);
	}

	public void Lose()
	{
		GameOver(BattleResult.Defeat);
	}
}
