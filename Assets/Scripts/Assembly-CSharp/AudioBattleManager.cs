using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBattleManager : MonoBehaviour
{
	public Transform[] audio_Sword;

	private static Queue<AudioInfo> freeAudio_Sword;

	public Transform[] audio_FireArrow;

	private static Queue<AudioInfo> freeAudio_FireArrow;

	public Transform[] audio_FireMusket;

	private static Queue<AudioInfo> freeAudio_FireMusket;

	public Transform[] audio_FireCannon;

	private static Queue<AudioInfo> freeAudio_FireCannon;

	public Transform[] audio_FireCatapult;

	private static Queue<AudioInfo> freeAudio_FireCatapult;

	public Transform[] audio_Giant;

	private static Queue<AudioInfo> freeAudio_Giant;

	public Transform[] audio_Generic;

	private static Queue<AudioInfo> freeAudio_Generic;

	public Transform[] audio_Ballista;

	private static Queue<AudioInfo> freeAudio_Ballista;

	public Transform[] audio_Explosion;

	private static Queue<AudioInfo> freeAudio_Explosion;

	public Transform[] audio_MachineGun;

	private static Queue<AudioInfo> freeAudio_MachineGun;

	public Transform[] audio_FireBall;

	private static Queue<AudioInfo> freeAudio_FireBall;

	public Transform[] audio_FireExplosion;

	private static Queue<AudioInfo> freeAudio_FireExplosion;

	public Transform[] audio_Hwacha;

	private static Queue<AudioInfo> freeAudio_Hwacha;

	private void Awake()
	{
		Init_Sword();
		Init_FireArrow();
		Init_FireMusket();
		Init_FireCannon();
		Init_FireCatapult();
		Init_Giant();
		Init_Generic();
		Init_Ballista();
		Init_Explosion();
		Init_MachineGun();
		Init_FireBall();
		Init_FireExplosion();
		Init_Hwacha();
	}

	private void Init_Sword()
	{
		freeAudio_Sword = new Queue<AudioInfo>();
		for (int i = 0; i < audio_Sword.Length; i++)
		{
			freeAudio_Sword.Enqueue(new AudioInfo(audio_Sword[i]));
		}
	}

	private void Init_FireArrow()
	{
		freeAudio_FireArrow = new Queue<AudioInfo>();
		for (int i = 0; i < audio_FireArrow.Length; i++)
		{
			freeAudio_FireArrow.Enqueue(new AudioInfo(audio_FireArrow[i]));
		}
	}

	private void Init_FireMusket()
	{
		freeAudio_FireMusket = new Queue<AudioInfo>();
		for (int i = 0; i < audio_FireMusket.Length; i++)
		{
			freeAudio_FireMusket.Enqueue(new AudioInfo(audio_FireMusket[i]));
		}
	}

	private void Init_FireCannon()
	{
		freeAudio_FireCannon = new Queue<AudioInfo>();
		for (int i = 0; i < audio_FireCannon.Length; i++)
		{
			freeAudio_FireCannon.Enqueue(new AudioInfo(audio_FireCannon[i]));
		}
	}

	private void Init_FireCatapult()
	{
		freeAudio_FireCatapult = new Queue<AudioInfo>();
		for (int i = 0; i < audio_FireCatapult.Length; i++)
		{
			freeAudio_FireCatapult.Enqueue(new AudioInfo(audio_FireCatapult[i]));
		}
	}

	private void Init_Giant()
	{
		freeAudio_Giant = new Queue<AudioInfo>();
		for (int i = 0; i < audio_Giant.Length; i++)
		{
			freeAudio_Giant.Enqueue(new AudioInfo(audio_Giant[i]));
		}
	}

	private void Init_Generic()
	{
		freeAudio_Generic = new Queue<AudioInfo>();
		for (int i = 0; i < audio_Generic.Length; i++)
		{
			freeAudio_Generic.Enqueue(new AudioInfo(audio_Generic[i]));
		}
	}

	private void Init_Ballista()
	{
		freeAudio_Ballista = new Queue<AudioInfo>();
		for (int i = 0; i < audio_Ballista.Length; i++)
		{
			freeAudio_Ballista.Enqueue(new AudioInfo(audio_Ballista[i]));
		}
	}

	private void Init_Explosion()
	{
		freeAudio_Explosion = new Queue<AudioInfo>();
		for (int i = 0; i < audio_Explosion.Length; i++)
		{
			freeAudio_Explosion.Enqueue(new AudioInfo(audio_Explosion[i]));
		}
	}

	private void Init_MachineGun()
	{
		freeAudio_MachineGun = new Queue<AudioInfo>();
		for (int i = 0; i < audio_MachineGun.Length; i++)
		{
			freeAudio_MachineGun.Enqueue(new AudioInfo(audio_MachineGun[i]));
		}
	}

	private void Init_FireBall()
	{
		freeAudio_FireBall = new Queue<AudioInfo>();
		for (int i = 0; i < audio_FireBall.Length; i++)
		{
			freeAudio_FireBall.Enqueue(new AudioInfo(audio_FireBall[i]));
		}
	}

	private void Init_FireExplosion()
	{
		freeAudio_FireExplosion = new Queue<AudioInfo>();
		for (int i = 0; i < audio_FireExplosion.Length; i++)
		{
			freeAudio_FireExplosion.Enqueue(new AudioInfo(audio_FireExplosion[i]));
		}
	}

	private void Init_Hwacha()
	{
		freeAudio_Hwacha = new Queue<AudioInfo>();
		for (int i = 0; i < audio_Hwacha.Length; i++)
		{
			freeAudio_Hwacha.Enqueue(new AudioInfo(audio_Hwacha[i]));
		}
	}

	public void PlayAudio_Sword(Vector3 position)
	{
		if (freeAudio_Sword.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_Sword.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_Sword", audioInfo);
		}
	}

	public void PlayAudio_FireArrow(Vector3 position)
	{
		if (freeAudio_FireArrow.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_FireArrow.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_FireArrow", audioInfo);
		}
	}

	public void PlayAudio_FireMusket(Vector3 position)
	{
		if (freeAudio_FireMusket.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_FireMusket.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_FireMusket", audioInfo);
		}
	}

	public void PlayAudio_FireCannon(Vector3 position)
	{
		if (freeAudio_FireCannon.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_FireCannon.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_FireCannon", audioInfo);
		}
	}

	public void PlayAudio_FireCatapult(Vector3 position)
	{
		if (freeAudio_FireCatapult.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_FireCatapult.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_FireCatapult", audioInfo);
		}
	}

	public void PlayAudio_Giant(Vector3 position)
	{
		if (freeAudio_Giant.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_Giant.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_Giant", audioInfo);
		}
	}

	public void PlayAudio_Generic(Vector3 position)
	{
		if (freeAudio_Generic.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_Generic.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_Generic", audioInfo);
		}
	}

	public void PlayAudio_Ballista(Vector3 position)
	{
		if (freeAudio_Ballista.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_Ballista.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_Ballista", audioInfo);
		}
	}

	public void PlayAudio_Explosion(Vector3 position)
	{
		if (freeAudio_Explosion.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_Explosion.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_Explosion", audioInfo);
		}
	}

	public void PlayAudio_MachineGun(Vector3 position)
	{
		if (freeAudio_MachineGun.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_MachineGun.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_MachineGun", audioInfo);
		}
	}

	public void PlayAudio_FireBall(Vector3 position)
	{
		if (freeAudio_FireBall.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_FireBall.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_FireBall", audioInfo);
		}
	}

	public void PlayAudio_FireExplosion(Vector3 position)
	{
		if (freeAudio_FireExplosion.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_FireExplosion.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_FireExplosion", audioInfo);
		}
	}

	public void PlayAudio_Hwacha(Vector3 position)
	{
		if (freeAudio_Hwacha.Count > 0)
		{
			AudioInfo audioInfo = freeAudio_Hwacha.Dequeue();
			audioInfo.Play(position);
			StartCoroutine("Reset_Hwacha", audioInfo);
		}
	}

	private IEnumerator Reset_Sword(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_Sword.Enqueue(audio);
	}

	private IEnumerator Reset_FireArrow(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_FireArrow.Enqueue(audio);
	}

	private IEnumerator Reset_FireMusket(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_FireMusket.Enqueue(audio);
	}

	private IEnumerator Reset_FireCannon(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_FireCannon.Enqueue(audio);
	}

	private IEnumerator Reset_FireCatapult(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_FireCatapult.Enqueue(audio);
	}

	private IEnumerator Reset_Giant(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_Giant.Enqueue(audio);
	}

	private IEnumerator Reset_Generic(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_Generic.Enqueue(audio);
	}

	private IEnumerator Reset_Ballista(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_Ballista.Enqueue(audio);
	}

	private IEnumerator Reset_Explosion(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_Explosion.Enqueue(audio);
	}

	private IEnumerator Reset_MachineGun(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_MachineGun.Enqueue(audio);
	}

	private IEnumerator Reset_FireBall(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_FireBall.Enqueue(audio);
	}

	private IEnumerator Reset_FireExplosion(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_FireExplosion.Enqueue(audio);
	}

	private IEnumerator Reset_Hwacha(AudioInfo audio)
	{
		yield return new WaitForSeconds(audio.length);
		freeAudio_Hwacha.Enqueue(audio);
	}
}
