using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	[HideInInspector]
	public bool isTouching;

	private int dragFingerID = -2;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (dragFingerID == -2)
		{
			dragFingerID = eventData.pointerId;
		}
		else if (eventData.pointerId != dragFingerID)
		{
			return;
		}
		isTouching = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			isTouching = false;
			dragFingerID = -2;
		}
	}
}
