using System;
using System.Collections.Generic;

namespace Spine
{
	public class IkConstraintData
	{
		internal string name;

		internal int order;

		internal List<BoneData> bones = new List<BoneData>();

		internal BoneData target;

		internal int bendDirection = 1;

		internal bool compress;

		internal bool stretch;

		internal bool uniform;

		internal float mix = 1f;

		public string Name
		{
			get
			{
				return name;
			}
		}

		public int Order
		{
			get
			{
				return order;
			}
			set
			{
				order = value;
			}
		}

		public List<BoneData> Bones
		{
			get
			{
				return bones;
			}
		}

		public BoneData Target
		{
			get
			{
				return target;
			}
			set
			{
				target = value;
			}
		}

		public float Mix
		{
			get
			{
				return mix;
			}
			set
			{
				mix = value;
			}
		}

		public int BendDirection
		{
			get
			{
				return bendDirection;
			}
			set
			{
				bendDirection = value;
			}
		}

		public bool Compress
		{
			get
			{
				return compress;
			}
			set
			{
				compress = value;
			}
		}

		public bool Stretch
		{
			get
			{
				return stretch;
			}
			set
			{
				stretch = value;
			}
		}

		public bool Uniform
		{
			get
			{
				return uniform;
			}
			set
			{
				uniform = value;
			}
		}

		public IkConstraintData(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			this.name = name;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
