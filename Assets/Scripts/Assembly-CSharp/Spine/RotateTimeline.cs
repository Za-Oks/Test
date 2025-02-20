namespace Spine
{
	public class RotateTimeline : CurveTimeline, IBoneTimeline
	{
		public const int ENTRIES = 2;

		internal const int PREV_TIME = -2;

		internal const int PREV_ROTATION = -1;

		internal const int ROTATION = 1;

		internal int boneIndex;

		internal float[] frames;

		public int BoneIndex
		{
			get
			{
				return boneIndex;
			}
			set
			{
				boneIndex = value;
			}
		}

		public float[] Frames
		{
			get
			{
				return frames;
			}
			set
			{
				frames = value;
			}
		}

		public override int PropertyId
		{
			get
			{
				return boneIndex;
			}
		}

		public RotateTimeline(int frameCount)
			: base(frameCount)
		{
			frames = new float[frameCount << 1];
		}

		public void SetFrame(int frameIndex, float time, float degrees)
		{
			frameIndex <<= 1;
			frames[frameIndex] = time;
			frames[frameIndex + 1] = degrees;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[boneIndex];
			float[] array = frames;
			if (time < array[0])
			{
				switch (blend)
				{
				case MixBlend.Setup:
					bone.rotation = bone.data.rotation;
					break;
				case MixBlend.First:
				{
					float num = bone.data.rotation - bone.rotation;
					bone.rotation += (num - (float)((16384 - (int)(16384.499999999996 - (double)(num / 360f))) * 360)) * alpha;
					break;
				}
				}
				return;
			}
			if (time >= array[array.Length - 2])
			{
				float num2 = array[array.Length + -1];
				switch (blend)
				{
				default:
					return;
				case MixBlend.Setup:
					bone.rotation = bone.data.rotation + num2 * alpha;
					return;
				case MixBlend.First:
				case MixBlend.Replace:
					num2 += bone.data.rotation - bone.rotation;
					num2 -= (float)((16384 - (int)(16384.499999999996 - (double)(num2 / 360f))) * 360);
					break;
				case MixBlend.Add:
					break;
				}
				bone.rotation += num2 * alpha;
				return;
			}
			int num3 = Animation.BinarySearch(array, time, 2);
			float num4 = array[num3 + -1];
			float num5 = array[num3];
			float curvePercent = GetCurvePercent((num3 >> 1) - 1, 1f - (time - num5) / (array[num3 + -2] - num5));
			float num6 = array[num3 + 1] - num4;
			num6 = num4 + (num6 - (float)((16384 - (int)(16384.499999999996 - (double)(num6 / 360f))) * 360)) * curvePercent;
			switch (blend)
			{
			default:
				return;
			case MixBlend.Setup:
				bone.rotation = bone.data.rotation + (num6 - (float)((16384 - (int)(16384.499999999996 - (double)(num6 / 360f))) * 360)) * alpha;
				return;
			case MixBlend.First:
			case MixBlend.Replace:
				num6 += bone.data.rotation - bone.rotation;
				break;
			case MixBlend.Add:
				break;
			}
			bone.rotation += (num6 - (float)((16384 - (int)(16384.499999999996 - (double)(num6 / 360f))) * 360)) * alpha;
		}
	}
}
