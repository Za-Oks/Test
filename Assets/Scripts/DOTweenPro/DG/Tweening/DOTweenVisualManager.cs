using DG.Tweening.Core;
using UnityEngine;

namespace DG.Tweening
{
	[AddComponentMenu("")]
	public class DOTweenVisualManager : MonoBehaviour
	{
		public VisualManagerPreset preset;

		public OnEnableBehaviour onEnableBehaviour;

		public OnDisableBehaviour onDisableBehaviour;

		private bool _requiresRestartFromSpawnPoint;

		private void Update()
		{
			if (_requiresRestartFromSpawnPoint)
			{
				_requiresRestartFromSpawnPoint = false;
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (!(component == null))
				{
					component.DORestart(true);
				}
			}
		}

		private void OnEnable()
		{
			switch (onEnableBehaviour)
			{
			case OnEnableBehaviour.Play:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DOPlay();
				}
				break;
			}
			case OnEnableBehaviour.Restart:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DORestart();
				}
				break;
			}
			case OnEnableBehaviour.RestartFromSpawnPoint:
				_requiresRestartFromSpawnPoint = true;
				break;
			}
		}

		private void OnDisable()
		{
			_requiresRestartFromSpawnPoint = false;
			switch (onDisableBehaviour)
			{
			case OnDisableBehaviour.Pause:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DOPause();
				}
				break;
			}
			case OnDisableBehaviour.Rewind:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DORewind();
				}
				break;
			}
			case OnDisableBehaviour.Kill:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DOKill();
				}
				break;
			}
			case OnDisableBehaviour.KillAndComplete:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DOComplete();
					component.DOKill();
				}
				break;
			}
			case OnDisableBehaviour.DestroyGameObject:
			{
				ABSAnimationComponent component = GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DOKill();
				}
				Object.Destroy(base.gameObject);
				break;
			}
			}
		}
	}
}
