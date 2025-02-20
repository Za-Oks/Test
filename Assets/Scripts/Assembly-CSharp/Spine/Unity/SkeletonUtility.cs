using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(ISkeletonAnimation))]
	public sealed class SkeletonUtility : MonoBehaviour
	{
		public delegate void SkeletonUtilityDelegate();

		public Transform boneRoot;

		[HideInInspector]
		public SkeletonRenderer skeletonRenderer;

		[HideInInspector]
		public ISkeletonAnimation skeletonAnimation;

		[NonSerialized]
		public List<SkeletonUtilityBone> boneComponents = new List<SkeletonUtilityBone>();

		[NonSerialized]
		public List<SkeletonUtilityConstraint> constraintComponents = new List<SkeletonUtilityConstraint>();

		private bool hasOverrideBones;

		private bool hasConstraints;

		private bool needToReprocessBones;

		public event SkeletonUtilityDelegate OnReset;

		public static PolygonCollider2D AddBoundingBoxGameObject(Skeleton skeleton, string skinName, string slotName, string attachmentName, Transform parent, bool isTrigger = true)
		{
			Skin skin = ((!string.IsNullOrEmpty(skinName)) ? skeleton.data.FindSkin(skinName) : skeleton.data.defaultSkin);
			if (skin == null)
			{
				Debug.LogError("Skin " + skinName + " not found!");
				return null;
			}
			Attachment attachment = skin.GetAttachment(skeleton.FindSlotIndex(slotName), attachmentName);
			if (attachment == null)
			{
				Debug.LogFormat("Attachment in slot '{0}' named '{1}' not found in skin '{2}'.", slotName, attachmentName, skin.name);
				return null;
			}
			BoundingBoxAttachment boundingBoxAttachment = attachment as BoundingBoxAttachment;
			if (boundingBoxAttachment != null)
			{
				Slot slot = skeleton.FindSlot(slotName);
				return AddBoundingBoxGameObject(boundingBoxAttachment.Name, boundingBoxAttachment, slot, parent, isTrigger);
			}
			Debug.LogFormat("Attachment '{0}' was not a Bounding Box.", attachmentName);
			return null;
		}

		public static PolygonCollider2D AddBoundingBoxGameObject(string name, BoundingBoxAttachment box, Slot slot, Transform parent, bool isTrigger = true)
		{
			GameObject gameObject = new GameObject("[BoundingBox]" + ((!string.IsNullOrEmpty(name)) ? name : box.Name));
			Transform transform = gameObject.transform;
			transform.parent = parent;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			return AddBoundingBoxAsComponent(box, slot, gameObject, isTrigger);
		}

		public static PolygonCollider2D AddBoundingBoxAsComponent(BoundingBoxAttachment box, Slot slot, GameObject gameObject, bool isTrigger = true)
		{
			if (box == null)
			{
				return null;
			}
			PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
			polygonCollider2D.isTrigger = isTrigger;
			SetColliderPointsLocal(polygonCollider2D, slot, box);
			return polygonCollider2D;
		}

		public static void SetColliderPointsLocal(PolygonCollider2D collider, Slot slot, BoundingBoxAttachment box)
		{
			if (box != null)
			{
				if (box.IsWeighted())
				{
					Debug.LogWarning("UnityEngine.PolygonCollider2D does not support weighted or animated points. Collider points will not be animated and may have incorrect orientation. If you want to use it as a collider, please remove weights and animations from the bounding box in Spine editor.");
				}
				Vector2[] localVertices = box.GetLocalVertices(slot, null);
				collider.SetPath(0, localVertices);
			}
		}

		public static Bounds GetBoundingBoxBounds(BoundingBoxAttachment boundingBox, float depth = 0f)
		{
			float[] vertices = boundingBox.Vertices;
			int num = vertices.Length;
			Bounds result = default(Bounds);
			result.center = new Vector3(vertices[0], vertices[1], 0f);
			for (int i = 2; i < num; i += 2)
			{
				result.Encapsulate(new Vector3(vertices[i], vertices[i + 1], 0f));
			}
			Vector3 size = result.size;
			size.z = depth;
			result.size = size;
			return result;
		}

		public static Rigidbody2D AddBoneRigidbody2D(GameObject gameObject, bool isKinematic = true, float gravityScale = 0f)
		{
			Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
			if (rigidbody2D == null)
			{
				rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
				rigidbody2D.isKinematic = isKinematic;
				rigidbody2D.gravityScale = gravityScale;
			}
			return rigidbody2D;
		}

		private void Update()
		{
			Skeleton skeleton = skeletonRenderer.skeleton;
			if (skeleton != null && boneRoot != null)
			{
				boneRoot.localScale = new Vector3(skeleton.scaleX, skeleton.scaleY, 1f);
			}
		}

		public void ResubscribeEvents()
		{
			OnDisable();
			OnEnable();
		}

		private void OnEnable()
		{
			if (skeletonRenderer == null)
			{
				skeletonRenderer = GetComponent<SkeletonRenderer>();
			}
			if (skeletonAnimation == null)
			{
				skeletonAnimation = GetComponent(typeof(ISkeletonAnimation)) as ISkeletonAnimation;
			}
			skeletonRenderer.OnRebuild -= HandleRendererReset;
			skeletonRenderer.OnRebuild += HandleRendererReset;
			if (skeletonAnimation != null)
			{
				skeletonAnimation.UpdateLocal -= UpdateLocal;
				skeletonAnimation.UpdateLocal += UpdateLocal;
			}
			CollectBones();
		}

		private void Start()
		{
			CollectBones();
		}

		private void OnDisable()
		{
			skeletonRenderer.OnRebuild -= HandleRendererReset;
			if (skeletonAnimation != null)
			{
				skeletonAnimation.UpdateLocal -= UpdateLocal;
				skeletonAnimation.UpdateWorld -= UpdateWorld;
				skeletonAnimation.UpdateComplete -= UpdateComplete;
			}
		}

		private void HandleRendererReset(SkeletonRenderer r)
		{
			if (this.OnReset != null)
			{
				this.OnReset();
			}
			CollectBones();
		}

		public void RegisterBone(SkeletonUtilityBone bone)
		{
			if (!boneComponents.Contains(bone))
			{
				boneComponents.Add(bone);
				needToReprocessBones = true;
			}
		}

		public void UnregisterBone(SkeletonUtilityBone bone)
		{
			boneComponents.Remove(bone);
		}

		public void RegisterConstraint(SkeletonUtilityConstraint constraint)
		{
			if (!constraintComponents.Contains(constraint))
			{
				constraintComponents.Add(constraint);
				needToReprocessBones = true;
			}
		}

		public void UnregisterConstraint(SkeletonUtilityConstraint constraint)
		{
			constraintComponents.Remove(constraint);
		}

		public void CollectBones()
		{
			Skeleton skeleton = skeletonRenderer.skeleton;
			if (skeleton == null)
			{
				return;
			}
			if (boneRoot != null)
			{
				List<object> list = new List<object>();
				ExposedList<IkConstraint> ikConstraints = skeleton.IkConstraints;
				int i = 0;
				for (int count = ikConstraints.Count; i < count; i++)
				{
					list.Add(ikConstraints.Items[i].target);
				}
				ExposedList<TransformConstraint> transformConstraints = skeleton.TransformConstraints;
				int j = 0;
				for (int count2 = transformConstraints.Count; j < count2; j++)
				{
					list.Add(transformConstraints.Items[j].target);
				}
				List<SkeletonUtilityBone> list2 = boneComponents;
				int k = 0;
				for (int count3 = list2.Count; k < count3; k++)
				{
					SkeletonUtilityBone skeletonUtilityBone = list2[k];
					if (skeletonUtilityBone.bone != null)
					{
						hasOverrideBones |= skeletonUtilityBone.mode == SkeletonUtilityBone.Mode.Override;
						hasConstraints |= list.Contains(skeletonUtilityBone.bone);
					}
				}
				hasConstraints |= constraintComponents.Count > 0;
				if (skeletonAnimation != null)
				{
					skeletonAnimation.UpdateWorld -= UpdateWorld;
					skeletonAnimation.UpdateComplete -= UpdateComplete;
					if (hasOverrideBones || hasConstraints)
					{
						skeletonAnimation.UpdateWorld += UpdateWorld;
					}
					if (hasConstraints)
					{
						skeletonAnimation.UpdateComplete += UpdateComplete;
					}
				}
				needToReprocessBones = false;
			}
			else
			{
				boneComponents.Clear();
				constraintComponents.Clear();
			}
		}

		private void UpdateLocal(ISkeletonAnimation anim)
		{
			if (needToReprocessBones)
			{
				CollectBones();
			}
			List<SkeletonUtilityBone> list = boneComponents;
			if (list != null)
			{
				int i = 0;
				for (int count = list.Count; i < count; i++)
				{
					list[i].transformLerpComplete = false;
				}
				UpdateAllBones(SkeletonUtilityBone.UpdatePhase.Local);
			}
		}

		private void UpdateWorld(ISkeletonAnimation anim)
		{
			UpdateAllBones(SkeletonUtilityBone.UpdatePhase.World);
			int i = 0;
			for (int count = constraintComponents.Count; i < count; i++)
			{
				constraintComponents[i].DoUpdate();
			}
		}

		private void UpdateComplete(ISkeletonAnimation anim)
		{
			UpdateAllBones(SkeletonUtilityBone.UpdatePhase.Complete);
		}

		private void UpdateAllBones(SkeletonUtilityBone.UpdatePhase phase)
		{
			if (boneRoot == null)
			{
				CollectBones();
			}
			List<SkeletonUtilityBone> list = boneComponents;
			if (list != null)
			{
				int i = 0;
				for (int count = list.Count; i < count; i++)
				{
					list[i].DoUpdate(phase);
				}
			}
		}

		public Transform GetBoneRoot()
		{
			if (boneRoot != null)
			{
				return boneRoot;
			}
			boneRoot = new GameObject("SkeletonUtility-SkeletonRoot").transform;
			boneRoot.parent = base.transform;
			boneRoot.localPosition = Vector3.zero;
			boneRoot.localRotation = Quaternion.identity;
			boneRoot.localScale = Vector3.one;
			return boneRoot;
		}

		public GameObject SpawnRoot(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GetBoneRoot();
			Skeleton skeleton = skeletonRenderer.skeleton;
			GameObject result = SpawnBone(skeleton.RootBone, boneRoot, mode, pos, rot, sca);
			CollectBones();
			return result;
		}

		public GameObject SpawnHierarchy(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GetBoneRoot();
			Skeleton skeleton = skeletonRenderer.skeleton;
			GameObject result = SpawnBoneRecursively(skeleton.RootBone, boneRoot, mode, pos, rot, sca);
			CollectBones();
			return result;
		}

		public GameObject SpawnBoneRecursively(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GameObject gameObject = SpawnBone(bone, parent, mode, pos, rot, sca);
			ExposedList<Bone> children = bone.Children;
			int i = 0;
			for (int count = children.Count; i < count; i++)
			{
				Bone bone2 = children.Items[i];
				SpawnBoneRecursively(bone2, gameObject.transform, mode, pos, rot, sca);
			}
			return gameObject;
		}

		public GameObject SpawnBone(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GameObject gameObject = new GameObject(bone.Data.Name);
			Transform transform = gameObject.transform;
			transform.parent = parent;
			SkeletonUtilityBone skeletonUtilityBone = gameObject.AddComponent<SkeletonUtilityBone>();
			skeletonUtilityBone.hierarchy = this;
			skeletonUtilityBone.position = pos;
			skeletonUtilityBone.rotation = rot;
			skeletonUtilityBone.scale = sca;
			skeletonUtilityBone.mode = mode;
			skeletonUtilityBone.zPosition = true;
			skeletonUtilityBone.Reset();
			skeletonUtilityBone.bone = bone;
			skeletonUtilityBone.boneName = bone.Data.Name;
			skeletonUtilityBone.valid = true;
			if (mode == SkeletonUtilityBone.Mode.Override)
			{
				if (rot)
				{
					transform.localRotation = Quaternion.Euler(0f, 0f, skeletonUtilityBone.bone.AppliedRotation);
				}
				if (pos)
				{
					transform.localPosition = new Vector3(skeletonUtilityBone.bone.X, skeletonUtilityBone.bone.Y, 0f);
				}
				transform.localScale = new Vector3(skeletonUtilityBone.bone.scaleX, skeletonUtilityBone.bone.scaleY, 0f);
			}
			return gameObject;
		}
	}
}
