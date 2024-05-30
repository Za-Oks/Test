using UnityEngine;
using UnityEngine.EventSystems;

public class DetectTap : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public TapManager tapManager;

	public bool isDraging;

	private int dragFingerID = -2;

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			isDraging = true;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (dragFingerID == -2)
		{
			dragFingerID = eventData.pointerId;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			if (!isDraging)
			{
				tapManager.Tap(eventData);
			}
			dragFingerID = -2;
			isDraging = false;
		}
	}
}
