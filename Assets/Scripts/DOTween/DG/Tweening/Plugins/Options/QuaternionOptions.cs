using UnityEngine;

namespace DG.Tweening.Plugins.Options
{
	public struct QuaternionOptions : IPlugOptions
	{
		internal RotateMode rotateMode;

		internal AxisConstraint axisConstraint;

		internal Vector3 up;

		public void Reset()
		{
			rotateMode = RotateMode.Fast;
			axisConstraint = AxisConstraint.None;
			up = Vector3.zero;
		}
	}
}
