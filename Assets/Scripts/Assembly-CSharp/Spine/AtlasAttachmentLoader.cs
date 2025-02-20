using System;

namespace Spine
{
	public class AtlasAttachmentLoader : AttachmentLoader
	{
		private Atlas[] atlasArray;

		public AtlasAttachmentLoader(params Atlas[] atlasArray)
		{
			if (atlasArray == null)
			{
				throw new ArgumentNullException("atlas array cannot be null.");
			}
			this.atlasArray = atlasArray;
		}

		public RegionAttachment NewRegionAttachment(Skin skin, string name, string path)
		{
			AtlasRegion atlasRegion = FindRegion(path);
			if (atlasRegion == null)
			{
				throw new ArgumentException(string.Format("Region not found in atlas: {0} (region attachment: {1})", path, name));
			}
			RegionAttachment regionAttachment = new RegionAttachment(name);
			regionAttachment.RendererObject = atlasRegion;
			regionAttachment.SetUVs(atlasRegion.u, atlasRegion.v, atlasRegion.u2, atlasRegion.v2, atlasRegion.rotate);
			regionAttachment.regionOffsetX = atlasRegion.offsetX;
			regionAttachment.regionOffsetY = atlasRegion.offsetY;
			regionAttachment.regionWidth = atlasRegion.width;
			regionAttachment.regionHeight = atlasRegion.height;
			regionAttachment.regionOriginalWidth = atlasRegion.originalWidth;
			regionAttachment.regionOriginalHeight = atlasRegion.originalHeight;
			return regionAttachment;
		}

		public MeshAttachment NewMeshAttachment(Skin skin, string name, string path)
		{
			AtlasRegion atlasRegion = FindRegion(path);
			if (atlasRegion == null)
			{
				throw new ArgumentException(string.Format("Region not found in atlas: {0} (region attachment: {1})", path, name));
			}
			MeshAttachment meshAttachment = new MeshAttachment(name);
			meshAttachment.RendererObject = atlasRegion;
			meshAttachment.RegionU = atlasRegion.u;
			meshAttachment.RegionV = atlasRegion.v;
			meshAttachment.RegionU2 = atlasRegion.u2;
			meshAttachment.RegionV2 = atlasRegion.v2;
			meshAttachment.RegionRotate = atlasRegion.rotate;
			meshAttachment.regionOffsetX = atlasRegion.offsetX;
			meshAttachment.regionOffsetY = atlasRegion.offsetY;
			meshAttachment.regionWidth = atlasRegion.width;
			meshAttachment.regionHeight = atlasRegion.height;
			meshAttachment.regionOriginalWidth = atlasRegion.originalWidth;
			meshAttachment.regionOriginalHeight = atlasRegion.originalHeight;
			return meshAttachment;
		}

		public BoundingBoxAttachment NewBoundingBoxAttachment(Skin skin, string name)
		{
			return new BoundingBoxAttachment(name);
		}

		public PathAttachment NewPathAttachment(Skin skin, string name)
		{
			return new PathAttachment(name);
		}

		public PointAttachment NewPointAttachment(Skin skin, string name)
		{
			return new PointAttachment(name);
		}

		public ClippingAttachment NewClippingAttachment(Skin skin, string name)
		{
			return new ClippingAttachment(name);
		}

		public AtlasRegion FindRegion(string name)
		{
			for (int i = 0; i < atlasArray.Length; i++)
			{
				AtlasRegion atlasRegion = atlasArray[i].FindRegion(name);
				if (atlasRegion != null)
				{
					return atlasRegion;
				}
			}
			return null;
		}
	}
}
