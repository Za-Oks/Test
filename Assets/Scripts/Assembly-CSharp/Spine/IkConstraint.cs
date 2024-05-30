using System;

namespace Spine
{
	public class IkConstraint : IConstraint, IUpdatable
	{
		internal IkConstraintData data;

		internal ExposedList<Bone> bones = new ExposedList<Bone>();

		internal Bone target;

		internal int bendDirection;

		internal bool compress;

		internal bool stretch;

		internal float mix;

		public IkConstraintData Data
		{
			get
			{
				return data;
			}
		}

		public int Order
		{
			get
			{
				return data.order;
			}
		}

		public ExposedList<Bone> Bones
		{
			get
			{
				return bones;
			}
		}

		public Bone Target
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

		public IkConstraint(IkConstraintData data, Skeleton skeleton)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.data = data;
			mix = data.mix;
			bendDirection = data.bendDirection;
			compress = data.compress;
			stretch = data.stretch;
			bones = new ExposedList<Bone>(data.bones.Count);
			foreach (BoneData bone in data.bones)
			{
				bones.Add(skeleton.FindBone(bone.name));
			}
			target = skeleton.FindBone(data.target.name);
		}

		public void Apply()
		{
			Update();
		}

		public void Update()
		{
			Bone bone = target;
			ExposedList<Bone> exposedList = bones;
			switch (exposedList.Count)
			{
			case 1:
				Apply(exposedList.Items[0], bone.worldX, bone.worldY, compress, stretch, data.uniform, mix);
				break;
			case 2:
				Apply(exposedList.Items[0], exposedList.Items[1], bone.worldX, bone.worldY, bendDirection, stretch, mix);
				break;
			}
		}

		public override string ToString()
		{
			return data.name;
		}

		public static void Apply(Bone bone, float targetX, float targetY, bool compress, bool stretch, bool uniform, float alpha)
		{
			if (!bone.appliedValid)
			{
				bone.UpdateAppliedTransform();
			}
			Bone parent = bone.parent;
			float num = 1f / (parent.a * parent.d - parent.b * parent.c);
			float num2 = targetX - parent.worldX;
			float num3 = targetY - parent.worldY;
			float num4 = (num2 * parent.d - num3 * parent.b) * num - bone.ax;
			float num5 = (num3 * parent.a - num2 * parent.c) * num - bone.ay;
			float num6 = (float)Math.Atan2(num5, num4) * (180f / (float)Math.PI) - bone.ashearX - bone.arotation;
			if (bone.ascaleX < 0f)
			{
				num6 += 180f;
			}
			if (num6 > 180f)
			{
				num6 -= 360f;
			}
			else if (num6 < -180f)
			{
				num6 += 360f;
			}
			float num7 = bone.ascaleX;
			float num8 = bone.ascaleY;
			if (compress || stretch)
			{
				float num9 = bone.data.length * num7;
				float num10 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
				if ((compress && num10 < num9) || (stretch && num10 > num9 && num9 > 0.0001f))
				{
					float num11 = (num10 / num9 - 1f) * alpha + 1f;
					num7 *= num11;
					if (uniform)
					{
						num8 *= num11;
					}
				}
			}
			bone.UpdateWorldTransform(bone.ax, bone.ay, bone.arotation + num6 * alpha, num7, num8, bone.ashearX, bone.ashearY);
		}

		public static void Apply(Bone parent, Bone child, float targetX, float targetY, int bendDir, bool stretch, float alpha)
		{
			if (alpha == 0f)
			{
				child.UpdateWorldTransform();
				return;
			}
			if (!parent.appliedValid)
			{
				parent.UpdateAppliedTransform();
			}
			if (!child.appliedValid)
			{
				child.UpdateAppliedTransform();
			}
			float ax = parent.ax;
			float ay = parent.ay;
			float num = parent.ascaleX;
			float num2 = num;
			float num3 = parent.ascaleY;
			float num4 = child.ascaleX;
			int num5;
			int num6;
			if (num < 0f)
			{
				num = 0f - num;
				num5 = 180;
				num6 = -1;
			}
			else
			{
				num5 = 0;
				num6 = 1;
			}
			if (num3 < 0f)
			{
				num3 = 0f - num3;
				num6 = -num6;
			}
			int num7;
			if (num4 < 0f)
			{
				num4 = 0f - num4;
				num7 = 180;
			}
			else
			{
				num7 = 0;
			}
			float ax2 = child.ax;
			float a = parent.a;
			float b = parent.b;
			float c = parent.c;
			float d = parent.d;
			bool flag = Math.Abs(num - num3) <= 0.0001f;
			float num8;
			float num9;
			float num10;
			if (!flag)
			{
				num8 = 0f;
				num9 = a * ax2 + parent.worldX;
				num10 = c * ax2 + parent.worldY;
			}
			else
			{
				num8 = child.ay;
				num9 = a * ax2 + b * num8 + parent.worldX;
				num10 = c * ax2 + d * num8 + parent.worldY;
			}
			Bone parent2 = parent.parent;
			a = parent2.a;
			b = parent2.b;
			c = parent2.c;
			d = parent2.d;
			float num11 = 1f / (a * d - b * c);
			float num12 = targetX - parent2.worldX;
			float num13 = targetY - parent2.worldY;
			float num14 = (num12 * d - num13 * b) * num11 - ax;
			float num15 = (num13 * a - num12 * c) * num11 - ay;
			float num16 = num14 * num14 + num15 * num15;
			num12 = num9 - parent2.worldX;
			num13 = num10 - parent2.worldY;
			float num17 = (num12 * d - num13 * b) * num11 - ax;
			float num18 = (num13 * a - num12 * c) * num11 - ay;
			float num19 = (float)Math.Sqrt(num17 * num17 + num18 * num18);
			float num20 = child.data.length * num4;
			float num23;
			float num22;
			if (flag)
			{
				num20 *= num;
				float num21 = (num16 - num19 * num19 - num20 * num20) / (2f * num19 * num20);
				if (num21 < -1f)
				{
					num21 = -1f;
				}
				else if (num21 > 1f)
				{
					num21 = 1f;
					if (stretch && num19 + num20 > 0.0001f)
					{
						num2 *= ((float)Math.Sqrt(num16) / (num19 + num20) - 1f) * alpha + 1f;
					}
				}
				num22 = (float)Math.Acos(num21) * (float)bendDir;
				a = num19 + num20 * num21;
				b = num20 * (float)Math.Sin(num22);
				num23 = (float)Math.Atan2(num15 * a - num14 * b, num14 * a + num15 * b);
			}
			else
			{
				a = num * num20;
				b = num3 * num20;
				float num24 = a * a;
				float num25 = b * b;
				float num26 = (float)Math.Atan2(num15, num14);
				c = num25 * num19 * num19 + num24 * num16 - num24 * num25;
				float num27 = -2f * num25 * num19;
				float num28 = num25 - num24;
				d = num27 * num27 - 4f * num28 * c;
				if (d >= 0f)
				{
					float num29 = (float)Math.Sqrt(d);
					if (num27 < 0f)
					{
						num29 = 0f - num29;
					}
					num29 = (0f - (num27 + num29)) / 2f;
					float num30 = num29 / num28;
					float num31 = c / num29;
					float num32 = ((!(Math.Abs(num30) < Math.Abs(num31))) ? num31 : num30);
					if (num32 * num32 <= num16)
					{
						num13 = (float)Math.Sqrt(num16 - num32 * num32) * (float)bendDir;
						num23 = num26 - (float)Math.Atan2(num13, num32);
						num22 = (float)Math.Atan2(num13 / num3, (num32 - num19) / num);
						goto IL_053b;
					}
				}
				float num33 = (float)Math.PI;
				float num34 = num19 - a;
				float num35 = num34 * num34;
				float num36 = 0f;
				float num37 = 0f;
				float num38 = num19 + a;
				float num39 = num38 * num38;
				float num40 = 0f;
				c = (0f - a) * num19 / (num24 - num25);
				if (c >= -1f && c <= 1f)
				{
					c = (float)Math.Acos(c);
					num12 = a * (float)Math.Cos(c) + num19;
					num13 = b * (float)Math.Sin(c);
					d = num12 * num12 + num13 * num13;
					if (d < num35)
					{
						num33 = c;
						num35 = d;
						num34 = num12;
						num36 = num13;
					}
					if (d > num39)
					{
						num37 = c;
						num39 = d;
						num38 = num12;
						num40 = num13;
					}
				}
				if (num16 <= (num35 + num39) / 2f)
				{
					num23 = num26 - (float)Math.Atan2(num36 * (float)bendDir, num34);
					num22 = num33 * (float)bendDir;
				}
				else
				{
					num23 = num26 - (float)Math.Atan2(num40 * (float)bendDir, num38);
					num22 = num37 * (float)bendDir;
				}
			}
			goto IL_053b;
			IL_053b:
			float num41 = (float)Math.Atan2(num8, ax2) * (float)num6;
			float arotation = parent.arotation;
			num23 = (num23 - num41) * (180f / (float)Math.PI) + (float)num5 - arotation;
			if (num23 > 180f)
			{
				num23 -= 360f;
			}
			else if (num23 < -180f)
			{
				num23 += 360f;
			}
			parent.UpdateWorldTransform(ax, ay, arotation + num23 * alpha, num2, parent.ascaleY, 0f, 0f);
			arotation = child.arotation;
			num22 = ((num22 + num41) * (180f / (float)Math.PI) - child.ashearX) * (float)num6 + (float)num7 - arotation;
			if (num22 > 180f)
			{
				num22 -= 360f;
			}
			else if (num22 < -180f)
			{
				num22 += 360f;
			}
			child.UpdateWorldTransform(ax2, num8, arotation + num22 * alpha, child.ascaleX, child.ascaleY, child.ashearX, child.ashearY);
		}
	}
}
