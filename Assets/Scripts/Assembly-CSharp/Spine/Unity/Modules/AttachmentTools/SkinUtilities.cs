using System;
using System.Collections.Generic;

namespace Spine.Unity.Modules.AttachmentTools
{
	public static class SkinUtilities
	{
		public static Skin UnshareSkin(this Skeleton skeleton, bool includeDefaultSkin, bool unshareAttachments, AnimationState state = null)
		{
			Skin clonedSkin = skeleton.GetClonedSkin("cloned skin", includeDefaultSkin, unshareAttachments);
			skeleton.SetSkin(clonedSkin);
			if (state != null)
			{
				skeleton.SetToSetupPose();
				state.Apply(skeleton);
			}
			return clonedSkin;
		}

		public static Skin GetClonedSkin(this Skeleton skeleton, string newSkinName, bool includeDefaultSkin = false, bool cloneAttachments = false, bool cloneMeshesAsLinked = true)
		{
			Skin skin = new Skin(newSkinName);
			Skin defaultSkin = skeleton.data.DefaultSkin;
			Skin skin2 = skeleton.skin;
			if (includeDefaultSkin)
			{
				defaultSkin.CopyTo(skin, true, cloneAttachments, cloneMeshesAsLinked);
			}
			if (skin2 != null)
			{
				skin2.CopyTo(skin, true, cloneAttachments, cloneMeshesAsLinked);
			}
			return skin;
		}

		public static Skin GetClone(this Skin original)
		{
			Skin skin = new Skin(original.name + " clone");
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = skin.Attachments;
			foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> attachment in original.Attachments)
			{
				attachments[attachment.Key] = attachment.Value;
			}
			return skin;
		}

		public static void SetAttachment(this Skin skin, string slotName, string keyName, Attachment attachment, Skeleton skeleton)
		{
			int num = skeleton.FindSlotIndex(slotName);
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Slot '{0}' does not exist in skeleton.", slotName), "slotName");
			}
			skin.AddAttachment(num, keyName, attachment);
		}

		public static void AddAttachments(this Skin skin, Skin otherSkin)
		{
			if (otherSkin != null)
			{
				otherSkin.CopyTo(skin, true, false);
			}
		}

		public static Attachment GetAttachment(this Skin skin, string slotName, string keyName, Skeleton skeleton)
		{
			int num = skeleton.FindSlotIndex(slotName);
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Slot '{0}' does not exist in skeleton.", slotName), "slotName");
			}
			return skin.GetAttachment(num, keyName);
		}

		public static void SetAttachment(this Skin skin, int slotIndex, string keyName, Attachment attachment)
		{
			skin.AddAttachment(slotIndex, keyName, attachment);
		}

		public static void RemoveAttachment(this Skin skin, string slotName, string keyName, SkeletonData skeletonData)
		{
			int num = skeletonData.FindSlotIndex(slotName);
			if (skeletonData == null)
			{
				throw new ArgumentNullException("skeletonData", "skeletonData cannot be null.");
			}
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Slot '{0}' does not exist in skeleton.", slotName), "slotName");
			}
			skin.RemoveAttachment(num, keyName);
		}

		public static void Clear(this Skin skin)
		{
			skin.Attachments.Clear();
		}

		public static void Append(this Skin destination, Skin source)
		{
			source.CopyTo(destination, true, false);
		}

		public static void CopyTo(this Skin source, Skin destination, bool overwrite, bool cloneAttachments, bool cloneMeshesAsLinked = true)
		{
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = source.Attachments;
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments2 = destination.Attachments;
			if (cloneAttachments)
			{
				if (overwrite)
				{
					foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> item in attachments)
					{
						attachments2[item.Key] = item.Value.GetClone(cloneMeshesAsLinked);
					}
					return;
				}
				{
					foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> item2 in attachments)
					{
						if (!attachments2.ContainsKey(item2.Key))
						{
							attachments2.Add(item2.Key, item2.Value.GetClone(cloneMeshesAsLinked));
						}
					}
					return;
				}
			}
			if (overwrite)
			{
				foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> item3 in attachments)
				{
					attachments2[item3.Key] = item3.Value;
				}
				return;
			}
			foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> item4 in attachments)
			{
				if (!attachments2.ContainsKey(item4.Key))
				{
					attachments2.Add(item4.Key, item4.Value);
				}
			}
		}
	}
}
