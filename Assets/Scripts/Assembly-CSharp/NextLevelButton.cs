using UnityEngine;
using UnityEngine.EventSystems;

public class NextLevelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public bool isNext;

	private int pointerID = -2;

	private bool isHolding;

	private float timeStartedHolding = -100f;

	private float lastTime_HoldEvent = -100f;

	private float timeBetween_HoldEvent = 0.1f;

	private Ui_Manager uimanager;

	private void Awake()
	{
		uimanager = GameObject.FindGameObjectWithTag("Ui_Manager").GetComponent<Ui_Manager>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (pointerID == -2)
		{
			pointerID = eventData.pointerId;
			isHolding = true;
			timeStartedHolding = Time.time;
			Event();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (pointerID == eventData.pointerId)
		{
			pointerID = -2;
			isHolding = false;
		}
	}

	private void Update()
	{
		if (isHolding && Time.time - timeStartedHolding >= 0.75f && Time.time - lastTime_HoldEvent >= timeBetween_HoldEvent)
		{
			lastTime_HoldEvent = Time.time;
			Event();
		}
	}

	private void Event()
	{
		if (isNext)
		{
			uimanager.NextLevel();
		}
		else
		{
			uimanager.PreviousLevel();
		}
	}
}
