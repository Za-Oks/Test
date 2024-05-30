public class BotDistance
{
	public float distance = float.MaxValue;

	public BotInfo enemy;

	public BotInfo player;

	public BotDistance(BotInfo player)
	{
		this.player = player;
		distance = float.MaxValue;
		enemy = null;
	}

	public BotDistance(BotInfo player, BotInfo enemy, float distance)
	{
		this.player = player;
		this.distance = distance;
		this.enemy = enemy;
	}

	public void CalculateDistance()
	{
		if (enemy == null || enemy.referencesManager.botMovement == null)
		{
			distance = float.MaxValue;
		}
		else if (enemy.referencesManager.botMovement.isDead)
		{
			distance = float.MaxValue;
		}
		else
		{
			distance = GameController.CalculateDistance(player, enemy);
		}
	}
}
