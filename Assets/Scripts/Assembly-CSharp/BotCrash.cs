using UnityEngine;

public class BotCrash : MonoBehaviour
{
	public AllReferencesManager referencesManager;

	public float timeBetween_Collide = 1f;

	public float start_damage = 25f;

	public float receive_damage = 20f;

	private BotMovement temp_bot;

	private Transform parent;

	private bool isPlayer;

	private float lastTime_Collide = -100f;

	private void OnCollisionEnter(Collision other)
	{
		if (referencesManager == null || other.transform == parent || !(Time.time - lastTime_Collide >= timeBetween_Collide) || !other.gameObject.CompareTag("Player"))
		{
			return;
		}
		temp_bot = AllReferencesManager.GAME_CONTROLLER.GetBotMovement(other.transform.name);
		if (temp_bot != null && isPlayer != temp_bot.isPlayer && !temp_bot.isDead)
		{
			lastTime_Collide = Time.time;
			if (isPlayer != temp_bot.isPlayer && !temp_bot.isDead)
			{
				temp_bot.ReceiveDamage(start_damage, parent.position, 0.04f);
				referencesManager.Crashed();
				referencesManager.botMovement.ReceiveDamage(receive_damage, other.transform.position, 0.04f);
			}
		}
	}

	public void InitBot(AllReferencesManager referencesManager, Transform parent, bool isPlayer)
	{
		this.referencesManager = referencesManager;
		this.parent = parent;
		this.isPlayer = isPlayer;
	}
}
