using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	[TrackColor(0.9960785f, 0.2509804f, 0.003921569f)]
	[TrackClipType(typeof(SpineAnimationStateClip))]
	[TrackBindingType(typeof(SkeletonAnimation))]
	public class SpineAnimationStateTrack : TrackAsset
	{
		public int trackIndex;

		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			ScriptPlayable<SpineAnimationStateMixerBehaviour> scriptPlayable = ScriptPlayable<SpineAnimationStateMixerBehaviour>.Create(graph, inputCount);
			SpineAnimationStateMixerBehaviour behaviour = scriptPlayable.GetBehaviour();
			behaviour.trackIndex = trackIndex;
			return scriptPlayable;
		}
	}
}
