using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[AddComponentMenu("Spine/BoneFollower")]
	public class BoneFollower : MonoBehaviour
	{
		public SkeletonRenderer skeletonRenderer;

		[SpineBone("", "skeletonRenderer", true, false)]
		[SerializeField]
		public string boneName;

		public bool followZPosition = true;

		public bool followBoneRotation = true;

		[Tooltip("Follows the skeleton's flip state by controlling this Transform's local scale.")]
		public bool followSkeletonFlip = true;

		[Tooltip("Follows the target bone's local scale. BoneFollower cannot inherit world/skewed scale because of UnityEngine.Transform property limitations.")]
		public bool followLocalScale;

		[FormerlySerializedAs("resetOnAwake")]
		public bool initializeOnAwake = true;

		[NonSerialized]
		public bool valid;

		[NonSerialized]
		public Bone bone;

		private Transform skeletonTransform;

		private bool skeletonTransformIsParent;

		public SkeletonRenderer SkeletonRenderer
		{
			get
			{
				return skeletonRenderer;
			}
			set
			{
				skeletonRenderer = value;
				Initialize();
			}
		}

		public bool SetBone(string name)
		{
			bone = skeletonRenderer.skeleton.FindBone(name);
			if (bone == null)
			{
				Debug.LogError("Bone not found: " + name, this);
				return false;
			}
			boneName = name;
			return true;
		}

		public void Awake()
		{
			if (initializeOnAwake)
			{
				Initialize();
			}
		}

		public void HandleRebuildRenderer(SkeletonRenderer skeletonRenderer)
		{
			Initialize();
		}

		public void Initialize()
		{
			bone = null;
			valid = skeletonRenderer != null && skeletonRenderer.valid;
			if (valid)
			{
				skeletonTransform = skeletonRenderer.transform;
				skeletonRenderer.OnRebuild -= HandleRebuildRenderer;
				skeletonRenderer.OnRebuild += HandleRebuildRenderer;
				skeletonTransformIsParent = object.ReferenceEquals(skeletonTransform, base.transform.parent);
				if (!string.IsNullOrEmpty(boneName))
				{
					bone = skeletonRenderer.skeleton.FindBone(boneName);
				}
			}
		}

		private void OnDestroy()
		{
			if (skeletonRenderer != null)
			{
				skeletonRenderer.OnRebuild -= HandleRebuildRenderer;
			}
		}

		public void LateUpdate()
		{
			if (!valid)
			{
				Initialize();
				return;
			}
			if (bone == null)
			{
				if (string.IsNullOrEmpty(boneName))
				{
					return;
				}
				bone = skeletonRenderer.skeleton.FindBone(boneName);
				if (!SetBone(boneName))
				{
					return;
				}
			}
			Transform transform = base.transform;
			if (skeletonTransformIsParent)
			{
				transform.localPosition = new Vector3(bone.worldX, bone.worldY, (!followZPosition) ? transform.localPosition.z : 0f);
				if (followBoneRotation)
				{
					float num = Mathf.Atan2(bone.c, bone.a) * 0.5f;
					if (followLocalScale && bone.scaleX < 0f)
					{
						num += (float)Math.PI / 2f;
					}
					Quaternion localRotation = default(Quaternion);
					localRotation.z = Mathf.Sin(num);
					localRotation.w = Mathf.Cos(num);
					transform.localRotation = localRotation;
				}
			}
			else
			{
				Vector3 position = skeletonTransform.TransformPoint(new Vector3(bone.worldX, bone.worldY, 0f));
				if (!followZPosition)
				{
					position.z = transform.position.z;
				}
				float num2 = bone.WorldRotationX;
				Transform parent = transform.parent;
				if (parent != null)
				{
					Matrix4x4 localToWorldMatrix = parent.localToWorldMatrix;
					if (localToWorldMatrix.m00 * localToWorldMatrix.m11 - localToWorldMatrix.m01 * localToWorldMatrix.m10 < 0f)
					{
						num2 = 0f - num2;
					}
				}
				if (followBoneRotation)
				{
					Vector3 eulerAngles = skeletonTransform.rotation.eulerAngles;
					if (followLocalScale && bone.scaleX < 0f)
					{
						num2 += 180f;
					}
					transform.SetPositionAndRotation(position, Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + num2));
				}
				else
				{
					transform.position = position;
				}
			}
			Vector3 localScale = ((!followLocalScale) ? new Vector3(1f, 1f, 1f) : new Vector3(bone.scaleX, bone.scaleY, 1f));
			if (followSkeletonFlip)
			{
				localScale.y *= Mathf.Sign(bone.skeleton.scaleX * bone.skeleton.scaleY);
			}
			transform.localScale = localScale;
		}
	}
}
