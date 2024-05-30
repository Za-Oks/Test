using UnityEngine;

public class BotAudio : MonoBehaviour
{
	public AudioBattleType audioType;

	public AudioBattleType[] secondary_AudioTypes;

	private AllReferencesManager referencesManager;

	private void Awake()
	{
		referencesManager = GetComponent<AllReferencesManager>();
	}

	public void PlayAudioAttack()
	{
		PlayAudioAttack(0);
	}

	public void PlayAudioAttack(int id)
	{
		switch ((id != 0) ? secondary_AudioTypes[id - 1] : audioType)
		{
		case AudioBattleType.SWORD:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_Sword(referencesManager.thisTR.position);
			break;
		case AudioBattleType.ARROW:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireArrow(referencesManager.thisTR.position);
			break;
		case AudioBattleType.CANNON:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireCannon(referencesManager.thisTR.position);
			break;
		case AudioBattleType.CATAPULT:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireCatapult(referencesManager.thisTR.position);
			break;
		case AudioBattleType.MUSKET:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireMusket(referencesManager.thisTR.position);
			break;
		case AudioBattleType.GIANT:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_Giant(referencesManager.thisTR.position);
			break;
		case AudioBattleType.GENERIC:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_Generic(referencesManager.thisTR.position);
			break;
		case AudioBattleType.BALLISTA:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_Ballista(referencesManager.thisTR.position);
			break;
		case AudioBattleType.EXPLOSION:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_Explosion(referencesManager.thisTR.position);
			break;
		case AudioBattleType.MACHINE_GUN:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_MachineGun(referencesManager.thisTR.position);
			break;
		case AudioBattleType.FIRE_BALL:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireBall(referencesManager.thisTR.position);
			break;
		case AudioBattleType.FIRE_EXPLOSION:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireExplosion(referencesManager.thisTR.position);
			break;
		case AudioBattleType.HWACHA:
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_Hwacha(referencesManager.thisTR.position);
			break;
		}
	}
}
