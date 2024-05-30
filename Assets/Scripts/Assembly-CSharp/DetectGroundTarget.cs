using UnityEngine;

public class DetectGroundTarget : MonoBehaviour
{
	public Animator thisAnimator;

	private void OnTriggerEnter()
	{
		thisAnimator.SetTrigger(AnimationHash.FLY_ATTACK);
	}

	public void Begin(bool isPlayer)
	{
		if (isPlayer)
		{
			base.gameObject.layer = LayerMask.NameToLayer("DetectBot2");
		}
		else
		{
			base.gameObject.layer = LayerMask.NameToLayer("DetectBot");
		}
		base.gameObject.SetActive(true);
	}

	public void Stop()
	{
		base.gameObject.SetActive(false);
	}
}
