using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_Single_Reposition : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public bool isDraging;

	private int dragFingerID = -2;

	public Camera_Best cameraBest;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (dragFingerID == -2)
		{
			Reset();
			dragFingerID = eventData.pointerId;
			isDraging = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			Reset();
			dragFingerID = -2;
			isDraging = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			isDraging = true;
			cameraBest.Rotate(eventData.delta);
		}
	}

	public void Reset()
	{
		isDraging = false;
		dragFingerID = -2;
	}
}
