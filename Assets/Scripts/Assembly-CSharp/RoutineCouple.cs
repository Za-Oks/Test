using System;
using System.Collections;

[Serializable]
public class RoutineCouple
{
	public IEnumerator routine;

	public AudioInfo audio;

	public RoutineCouple(IEnumerator routine, AudioInfo audio)
	{
		this.routine = routine;
		this.audio = audio;
	}
}
