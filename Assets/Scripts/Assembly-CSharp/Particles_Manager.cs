using UnityEngine;

public class Particles_Manager : MonoBehaviour
{
	[Header("Values")]
	public GameObject[] PistolParticles;

	public GameObject[] CannonParticles;

	public GameObject[] GiantParticles;

	public GameObject[] MachineGunParticles;

	public GameObject[] HwachaParticles;

	private ParticlesValues[] RifleParticlesValues;

	private ParticlesValues[] CannonParticleValues;

	private ParticlesValues[] GiantParticlesValues;

	private ParticlesValues[] MachineGunParticlesValues;

	private ParticlesValues[] HwachaParticlesValues;

	private void Awake()
	{
		InitializeParticlesValues();
	}

	private void InitializeParticlesValues()
	{
		RifleParticlesValues = new ParticlesValues[PistolParticles.Length];
		CannonParticleValues = new ParticlesValues[CannonParticles.Length];
		GiantParticlesValues = new ParticlesValues[GiantParticles.Length];
		MachineGunParticlesValues = new ParticlesValues[MachineGunParticles.Length];
		HwachaParticlesValues = new ParticlesValues[HwachaParticles.Length];
		for (int i = 0; i < PistolParticles.Length; i++)
		{
			ParticlesValues particlesValues = new ParticlesValues();
			particlesValues.thisGO = PistolParticles[i];
			particlesValues.thisTransform = PistolParticles[i].transform;
			particlesValues.thisParticle = PistolParticles[i].GetComponent<ParticleSystem>();
			RifleParticlesValues[i] = particlesValues;
		}
		for (int j = 0; j < CannonParticles.Length; j++)
		{
			ParticlesValues particlesValues2 = new ParticlesValues();
			particlesValues2.thisGO = CannonParticles[j];
			particlesValues2.thisTransform = CannonParticles[j].transform;
			particlesValues2.thisParticle = CannonParticles[j].GetComponent<ParticleSystem>();
			CannonParticleValues[j] = particlesValues2;
		}
		for (int k = 0; k < GiantParticles.Length; k++)
		{
			ParticlesValues particlesValues3 = new ParticlesValues();
			particlesValues3.thisGO = GiantParticles[k];
			particlesValues3.thisTransform = GiantParticles[k].transform;
			particlesValues3.thisParticle = GiantParticles[k].GetComponent<ParticleSystem>();
			GiantParticlesValues[k] = particlesValues3;
		}
		for (int l = 0; l < MachineGunParticles.Length; l++)
		{
			ParticlesValues particlesValues4 = new ParticlesValues();
			particlesValues4.thisGO = MachineGunParticles[l];
			particlesValues4.thisTransform = MachineGunParticles[l].transform;
			particlesValues4.thisParticle = MachineGunParticles[l].GetComponent<ParticleSystem>();
			MachineGunParticlesValues[l] = particlesValues4;
		}
		for (int m = 0; m < HwachaParticles.Length; m++)
		{
			ParticlesValues particlesValues5 = new ParticlesValues();
			particlesValues5.thisGO = HwachaParticles[m];
			particlesValues5.thisTransform = HwachaParticles[m].transform;
			particlesValues5.thisParticle = HwachaParticles[m].GetComponent<ParticleSystem>();
			HwachaParticlesValues[m] = particlesValues5;
		}
	}

	public void PlayPistolParticle(Transform pointer)
	{
		Vector3 position = pointer.position;
		Quaternion rotation = pointer.rotation;
		bool flag = true;
		int num = RifleParticlesValues.Length;
		for (int i = 0; i < num; i++)
		{
			if (!RifleParticlesValues[i].thisParticle.isPlaying)
			{
				RifleParticlesValues[i].thisGO.SetActive(true);
				RifleParticlesValues[i].thisTransform.position = position;
				RifleParticlesValues[i].thisTransform.rotation = rotation;
				RifleParticlesValues[i].thisParticle.Play();
				flag = false;
				break;
			}
		}
		if (!flag)
		{
		}
	}

	public void PlayCannonParticle(Transform pointer)
	{
		Vector3 position = pointer.position;
		Quaternion rotation = pointer.rotation;
		bool flag = true;
		int num = CannonParticleValues.Length;
		for (int i = 0; i < num; i++)
		{
			if (!CannonParticleValues[i].thisParticle.isPlaying)
			{
				CannonParticleValues[i].thisGO.SetActive(true);
				CannonParticleValues[i].thisTransform.position = position;
				CannonParticleValues[i].thisTransform.rotation = rotation;
				CannonParticleValues[i].thisParticle.Play();
				flag = false;
				break;
			}
		}
		if (!flag)
		{
		}
	}

	public void PlayGiantParticle(Vector3 position)
	{
		bool flag = true;
		int num = GiantParticlesValues.Length;
		for (int i = 0; i < num; i++)
		{
			if (!GiantParticlesValues[i].thisParticle.isPlaying)
			{
				GiantParticlesValues[i].thisGO.SetActive(true);
				GiantParticlesValues[i].thisTransform.position = position;
				GiantParticlesValues[i].thisParticle.Play();
				flag = false;
				break;
			}
		}
		if (!flag)
		{
		}
	}

	public void PlayMachineGunParticle(Transform pointer)
	{
		Vector3 position = pointer.position;
		Quaternion rotation = pointer.rotation;
		bool flag = true;
		int num = MachineGunParticlesValues.Length;
		for (int i = 0; i < num; i++)
		{
			if (!MachineGunParticlesValues[i].thisParticle.isPlaying)
			{
				MachineGunParticlesValues[i].thisGO.SetActive(true);
				MachineGunParticlesValues[i].thisTransform.position = position;
				MachineGunParticlesValues[i].thisTransform.rotation = rotation;
				MachineGunParticlesValues[i].thisParticle.Play();
				flag = false;
				break;
			}
		}
		if (!flag)
		{
		}
	}

	public void PlayHwachaParticle(Vector3 position)
	{
		bool flag = true;
		int num = HwachaParticlesValues.Length;
		for (int i = 0; i < num; i++)
		{
			if (!HwachaParticlesValues[i].thisParticle.isPlaying)
			{
				HwachaParticlesValues[i].thisGO.SetActive(true);
				HwachaParticlesValues[i].thisTransform.position = position;
				HwachaParticlesValues[i].thisParticle.Play();
				flag = false;
				break;
			}
		}
		if (!flag)
		{
		}
	}
}
