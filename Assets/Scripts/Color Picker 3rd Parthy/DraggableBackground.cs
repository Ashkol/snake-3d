using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBackground : EventTrigger
{
	public bool fixX;
	public bool fixY;
	public Transform thumb;	
	
	private bool dragging;
	private Collider2D backgroundCollider;

	private void Awake()
	{
		backgroundCollider = GetComponent<Collider2D>();
	}

	void FixedUpdate()
	{
		//if (Input.GetMouseButtonDown(0)) {
		//	dragging = false;
		//	var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//	RaycastHit hit;
		//	backgroundCollider.Raycast()
		//	if (backgroundCollider.Raycast(ray, out hit, 100)) {
		//		dragging = true;
		//	}
		//}
		//if (Input.GetMouseButtonUp(0)) dragging = false;
		//if (dragging && Input.GetMouseButton(0)) {
		//	var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	point = backgroundCollider.ClosestPointOnBounds(point);
		//	SetThumbPosition(point);
		//	SendMessage("OnDrag", Vector3.one - (thumb.position - GetComponent<Collider>().bounds.min) / GetComponent<Collider>().bounds.size.x);
		//}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		dragging = true;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		dragging = false;
	}

	void SetDragPoint(Vector3 point)
	{
		point = (Vector3.one - point) * GetComponent<Collider>().bounds.size.x + GetComponent<Collider>().bounds.min;
		SetThumbPosition(point);
	}

	void SetThumbPosition(Vector3 point)
	{
		thumb.position = new Vector3(fixX ? thumb.position.x : point.x, fixY ? thumb.position.y : point.y, thumb.position.z);
	}
}
