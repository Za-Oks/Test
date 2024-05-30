using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	[AddComponentMenu("DOTween/DOTween Path")]
	public class DOTweenPath : ABSAnimationComponent
	{
		public float delay;

		public float duration = 1f;

		public Ease easeType = Ease.OutQuad;

		public AnimationCurve easeCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

		public int loops = 1;

		public string id = "";

		public LoopType loopType;

		public OrientType orientType;

		public Transform lookAtTransform;

		public Vector3 lookAtPosition;

		public float lookAhead = 0.01f;

		public bool autoPlay = true;

		public bool autoKill = true;

		public bool relative;

		public bool isLocal;

		public bool isClosedPath;

		public int pathResolution = 10;

		public PathMode pathMode = PathMode.Full3D;

		public AxisConstraint lockRotation;

		public bool assignForwardAndUp;

		public Vector3 forwardDirection = Vector3.forward;

		public Vector3 upDirection = Vector3.up;

		public bool tweenRigidbody;

		public List<Vector3> wps = new List<Vector3>();

		public List<Vector3> fullWps = new List<Vector3>();

		public Path path;

		public DOTweenInspectorMode inspectorMode;

		public PathType pathType;

		public HandlesType handlesType;

		public bool livePreview = true;

		public HandlesDrawMode handlesDrawMode;

		public float perspectiveHandleSize = 0.5f;

		public bool showIndexes = true;

		public bool showWpLength;

		public Color pathColor = new Color(1f, 1f, 1f, 0.5f);

		public Vector3 lastSrcPosition;

		public bool wpsDropdown;

		public float dropToFloorOffset;

		private void Awake()
		{
			if (path == null || wps.Count < 1 || inspectorMode == DOTweenInspectorMode.OnlyPath)
			{
				return;
			}
			path.AssignDecoder(path.type);
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(path.Draw);
				path.gizmoColor = pathColor;
			}
			if (isLocal)
			{
				Transform transform = base.transform;
				if (transform.parent != null)
				{
					transform = transform.parent;
					Vector3 position = transform.position;
					int num = path.wps.Length;
					for (int i = 0; i < num; i++)
					{
						path.wps[i] = path.wps[i] - position;
					}
					num = path.controlPoints.Length;
					for (int j = 0; j < num; j++)
					{
						ControlPoint controlPoint = path.controlPoints[j];
						controlPoint.a -= position;
						controlPoint.b -= position;
						path.controlPoints[j] = controlPoint;
					}
				}
			}
			if (relative)
			{
				ReEvaluateRelativeTween();
			}
			if (pathMode == PathMode.Full3D && GetComponent<SpriteRenderer>() != null)
			{
				pathMode = PathMode.TopDown2D;
			}
			Rigidbody component = GetComponent<Rigidbody>();
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = ((!tweenRigidbody || !(component != null)) ? (isLocal ? base.transform.DOLocalPath(path, duration, pathMode).SetOptions(isClosedPath, AxisConstraint.None, lockRotation) : base.transform.DOPath(path, duration, pathMode).SetOptions(isClosedPath, AxisConstraint.None, lockRotation)) : (isLocal ? component.DOLocalPath(path, duration, pathMode).SetOptions(isClosedPath, AxisConstraint.None, lockRotation) : component.DOPath(path, duration, pathMode).SetOptions(isClosedPath, AxisConstraint.None, lockRotation)));
			switch (orientType)
			{
			case OrientType.LookAtTransform:
				if (lookAtTransform != null)
				{
					if (assignForwardAndUp)
					{
						tweenerCore.SetLookAt(lookAtTransform, forwardDirection, upDirection);
					}
					else
					{
						tweenerCore.SetLookAt(lookAtTransform);
					}
				}
				break;
			case OrientType.LookAtPosition:
				if (assignForwardAndUp)
				{
					tweenerCore.SetLookAt(lookAtPosition, forwardDirection, upDirection);
				}
				else
				{
					tweenerCore.SetLookAt(lookAtPosition);
				}
				break;
			case OrientType.ToPath:
				if (assignForwardAndUp)
				{
					tweenerCore.SetLookAt(lookAhead, forwardDirection, upDirection);
				}
				else
				{
					tweenerCore.SetLookAt(lookAhead);
				}
				break;
			}
			tweenerCore.SetDelay(delay).SetLoops(loops, loopType).SetAutoKill(autoKill)
				.SetUpdate(updateType)
				.OnKill(delegate
				{
					tween = null;
				});
			if (isSpeedBased)
			{
				tweenerCore.SetSpeedBased();
			}
			if (easeType == Ease.INTERNAL_Custom)
			{
				tweenerCore.SetEase(easeCurve);
			}
			else
			{
				tweenerCore.SetEase(easeType);
			}
			if (!string.IsNullOrEmpty(id))
			{
				tweenerCore.SetId(id);
			}
			if (hasOnStart)
			{
				if (onStart != null)
				{
					tweenerCore.OnStart(onStart.Invoke);
				}
			}
			else
			{
				onStart = null;
			}
			if (hasOnPlay)
			{
				if (onPlay != null)
				{
					tweenerCore.OnPlay(onPlay.Invoke);
				}
			}
			else
			{
				onPlay = null;
			}
			if (hasOnUpdate)
			{
				if (onUpdate != null)
				{
					tweenerCore.OnUpdate(onUpdate.Invoke);
				}
			}
			else
			{
				onUpdate = null;
			}
			if (hasOnStepComplete)
			{
				if (onStepComplete != null)
				{
					tweenerCore.OnStepComplete(onStepComplete.Invoke);
				}
			}
			else
			{
				onStepComplete = null;
			}
			if (hasOnComplete)
			{
				if (onComplete != null)
				{
					tweenerCore.OnComplete(onComplete.Invoke);
				}
			}
			else
			{
				onComplete = null;
			}
			if (hasOnRewind)
			{
				if (onRewind != null)
				{
					tweenerCore.OnRewind(onRewind.Invoke);
				}
			}
			else
			{
				onRewind = null;
			}
			if (autoPlay)
			{
				tweenerCore.Play();
			}
			else
			{
				tweenerCore.Pause();
			}
			tween = tweenerCore;
			if (hasOnTweenCreated && onTweenCreated != null)
			{
				onTweenCreated.Invoke();
			}
		}

		private void Reset()
		{
			path = new Path(pathType, wps.ToArray(), 10, pathColor);
		}

		private void OnDestroy()
		{
			if (tween != null && tween.active)
			{
				tween.Kill();
			}
			tween = null;
		}

		public override void DOPlay()
		{
			tween.Play();
		}

		public override void DOPlayBackwards()
		{
			tween.PlayBackwards();
		}

		public override void DOPlayForward()
		{
			tween.PlayForward();
		}

		public override void DOPause()
		{
			tween.Pause();
		}

		public override void DOTogglePause()
		{
			tween.TogglePause();
		}

		public override void DORewind()
		{
			tween.Rewind();
		}

		public override void DORestart(bool fromHere = false)
		{
			if (tween == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(tween);
				}
				return;
			}
			if (fromHere && relative && !isLocal)
			{
				ReEvaluateRelativeTween();
			}
			tween.Restart();
		}

		public override void DOComplete()
		{
			tween.Complete();
		}

		public override void DOKill()
		{
			tween.Kill();
		}

		public Tween GetTween()
		{
			if (tween == null || !tween.active)
			{
				if (Debugger.logPriority > 1)
				{
					if (tween == null)
					{
						Debugger.LogNullTween(tween);
					}
					else
					{
						Debugger.LogInvalidTween(tween);
					}
				}
				return null;
			}
			return tween;
		}

		public Vector3[] GetDrawPoints()
		{
			if (path.wps == null || path.nonLinearDrawWps == null)
			{
				Debugger.LogWarning("Draw points not ready yet. Returning NULL");
				return null;
			}
			if (pathType == PathType.Linear)
			{
				return path.wps;
			}
			return path.nonLinearDrawWps;
		}

		internal Vector3[] GetFullWps()
		{
			int count = wps.Count;
			int num = count + 1;
			if (isClosedPath)
			{
				num++;
			}
			Vector3[] array = new Vector3[num];
			array[0] = base.transform.position;
			for (int i = 0; i < count; i++)
			{
				array[i + 1] = wps[i];
			}
			if (isClosedPath)
			{
				array[num - 1] = array[0];
			}
			return array;
		}

		private void ReEvaluateRelativeTween()
		{
			Vector3 position = base.transform.position;
			if (!(position == lastSrcPosition))
			{
				Vector3 vector = position - lastSrcPosition;
				int num = path.wps.Length;
				for (int i = 0; i < num; i++)
				{
					path.wps[i] = path.wps[i] + vector;
				}
				num = path.controlPoints.Length;
				for (int j = 0; j < num; j++)
				{
					ControlPoint controlPoint = path.controlPoints[j];
					controlPoint.a += vector;
					controlPoint.b += vector;
					path.controlPoints[j] = controlPoint;
				}
				lastSrcPosition = position;
			}
		}
	}
}
