using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BotRagdollDeath : MonoBehaviour
{
	[Header("COMMON")]
	public Rigidbody forceRB;

	public GameObject shadow;

	public SkinnedMeshRenderer[] skinnedMeshes;

	[Header("VALUES")]
	public float forceMultiplier = 1f;

	public float hideY = -5f;

	public float waitForBury = 3f;

	[Header("NOT COMMON")]
	public Transform ragdollParent;

	public Animator ragdollAnimator;

	public Collider staticCollider;

	public Transform ragdollChest;

	[Header("PREVIEW")]
	public Ragdoll thisRagdoll;

	private AllReferencesManager referencesManager;

	private float burySpeed;

	private void Awake()
	{
		burySpeed = 0.5f;
		referencesManager = GetComponent<AllReferencesManager>();
	}

	public void Init()
	{
		if (ragdollAnimator == null)
		{
			ragdollAnimator = GetComponent<Animator>();
		}
		if (ragdollParent == null)
		{
			ragdollParent = base.transform;
		}
		if (skinnedMeshes == null || skinnedMeshes.Length == 0)
		{
			skinnedMeshes = ragdollParent.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		}
		if (shadow == null)
		{
			shadow = base.transform.Find("SpriteShadow").gameObject;
		}
		thisRagdoll = new Ragdoll();
		thisRagdoll.Init(ragdollParent, skinnedMeshes, ragdollChest);
		if (forceRB == null && thisRagdoll.ragdollHead != null)
		{
			forceRB = thisRagdoll.ragdollHead.rigidbody;
		}
	}

	public void Die(Vector3 fromPosition, float force)
	{
		if (shadow != null)
		{
			shadow.SetActive(false);
		}
		ragdollAnimator.enabled = false;
		if (staticCollider != null)
		{
			staticCollider.enabled = true;
		}
		thisRagdoll.MakeWalkable();
		forceRB.AddForce((base.transform.position - fromPosition).normalized * force * forceMultiplier + Vector3.up * force * forceMultiplier, ForceMode.Impulse);
		if (thisRagdoll.IsVisible())
		{
			StartCoroutine("Βury");
			return;
		}
		Vector3 position = base.transform.position;
		position.y = 0f;
		AllReferencesManager.CROSS_POOL_MANAGER.Trigger(position, Quaternion.Euler(new Vector3(0f, base.transform.rotation.eulerAngles.y, 0f)), Vector3.one * 1.2f, referencesManager.IsPlayer());
		referencesManager.ResetAnimator();
		base.gameObject.SetActive(false);
	}

	public void ResetEverything()
	{
		if (shadow != null)
		{
			shadow.gameObject.SetActive(true);
		}
		StopCoroutine("Βury");
		thisRagdoll.Reset();
	}

	private IEnumerator Βury()
	{
		yield return new WaitForSeconds(waitForBury);
		Vector3 pos = thisRagdoll.ragdollChest.position;
		pos.y = 0f;
		Transform cross = AllReferencesManager.CROSS_POOL_MANAGER.Trigger(pos, Quaternion.Euler(new Vector3(0f, thisRagdoll.ragdollChest.rotation.eulerAngles.y, 0f)), Vector3.zero, referencesManager.IsPlayer()).trans;
		cross.DOScale(Vector3.one * 1.2f, 3f).SetEase(Ease.OutElastic);
		thisRagdoll.Bury();
		while (referencesManager.thisTR.position.y > hideY)
		{
			referencesManager.thisTR.position -= Vector3.up * burySpeed * Time.deltaTime;
			yield return null;
		}
		referencesManager.ResetAnimator();
		base.gameObject.SetActive(false);
	}
}
