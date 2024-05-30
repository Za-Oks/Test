using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ragdoll
{
	public List<RagdollPart> parts = new List<RagdollPart>();

	public Transform ragdollChest;

	public RagdollPart ragdollHead;

	public Transform skeletonRagdollRoot;

	public Transform skeletonRoot;

	public SkinnedMeshRenderer[] skinnedMeshRenderers;

	public Transform[] skeleton_bones;

	public Transform[] ragdollSkeleton_bones;

	public void Init(Transform root, SkinnedMeshRenderer[] skinnedMeshRenderers, Transform ragdollChest)
	{
		if (skinnedMeshRenderers.Length != 0)
		{
			this.ragdollChest = ragdollChest;
			this.skinnedMeshRenderers = skinnedMeshRenderers;
			skeleton_bones = skinnedMeshRenderers[0].bones;
			ragdollSkeleton_bones = new Transform[skeleton_bones.Length];
			skeletonRagdollRoot = root.Find("Skeleton Ragdoll");
			skeletonRoot = root.Find("Skeleton");
			if (!(skeletonRagdollRoot == null) && !(skeletonRoot == null))
			{
				FindPart(skeletonRagdollRoot);
				Reset();
			}
		}
	}

	public void FindPart(Transform skeleton_ragdoll)
	{
		if (ragdollChest == null && skeleton_ragdoll.name == "chest")
		{
			ragdollChest = skeleton_ragdoll;
		}
		int num = skeleton_bones.Length;
		for (int i = 0; i < num; i++)
		{
			if (!(skeleton_bones[i] == null) && skeleton_ragdoll.name == skeleton_bones[i].gameObject.name)
			{
				ragdollSkeleton_bones[i] = skeleton_ragdoll;
				break;
			}
		}
		Rigidbody component = skeleton_ragdoll.GetComponent<Rigidbody>();
		if (component != null && skeleton_ragdoll.gameObject.layer == 17)
		{
			RagdollPart item = new RagdollPart(skeleton_ragdoll, component);
			parts.Add(item);
			if (ragdollHead == null && (skeleton_ragdoll.name == "head" || skeleton_ragdoll.name == "cranium"))
			{
				ragdollHead = item;
			}
		}
		if (skeleton_ragdoll.childCount > 0)
		{
			int childCount = skeleton_ragdoll.childCount;
			for (int j = 0; j < childCount; j++)
			{
				FindPart(skeleton_ragdoll.GetChild(j));
			}
		}
	}

	public void MakeWalkable()
	{
		skeletonRagdollRoot.position = skeletonRoot.position;
		int count = parts.Count;
		for (int i = 0; i < count; i++)
		{
			ragdollSkeleton_bones[i].localPosition = skeleton_bones[i].localPosition;
			parts[i].collider.enabled = true;
			parts[i].rigidbody.isKinematic = false;
		}
		skeletonRagdollRoot.gameObject.SetActive(true);
		skeletonRoot.gameObject.SetActive(false);
		int num = skinnedMeshRenderers.Length;
		for (int j = 0; j < num; j++)
		{
			skinnedMeshRenderers[j].bones = ragdollSkeleton_bones;
		}
	}

	public void Reset()
	{
		for (int i = 0; i < parts.Count; i++)
		{
			parts[i].collider.enabled = false;
			parts[i].rigidbody.isKinematic = true;
		}
		skeletonRagdollRoot.gameObject.SetActive(false);
		skeletonRoot.gameObject.SetActive(true);
		int num = skinnedMeshRenderers.Length;
		for (int j = 0; j < num; j++)
		{
			skinnedMeshRenderers[j].bones = skeleton_bones;
		}
	}

	public void Bury()
	{
		for (int i = 0; i < parts.Count; i++)
		{
			parts[i].collider.enabled = false;
			parts[i].rigidbody.isKinematic = true;
		}
	}

	public bool IsVisible()
	{
		int num = skinnedMeshRenderers.Length;
		for (int i = 0; i < num; i++)
		{
			if (skinnedMeshRenderers[i].isVisible)
			{
				return true;
			}
		}
		return false;
	}
}
