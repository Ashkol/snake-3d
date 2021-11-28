using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIThumb : EventTrigger
{
	private bool dragging;
	private RectTransform rectTransform;
	[SerializeField] private Collider2D backgroundCollider;
	[SerializeField] private ColorHuePicker huePicker;
	[SerializeField] private ColorSaturationBrightnessPicker saturationBrightnessPicker;
	private float thumbPosX, thumbPosY; // normalized

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		backgroundCollider = transform.parent.GetComponent<Collider2D>();
		huePicker = transform.parent.GetComponent<ColorHuePicker>();
		saturationBrightnessPicker = transform.parent.GetComponent<ColorSaturationBrightnessPicker>();
		Debug.Log(backgroundCollider.gameObject.name);
	}

	private void FixedUpdate()
	{
		if (dragging)
		{
			var point = Input.mousePosition;
			point = backgroundCollider.ClosestPoint(point);
			transform.position = point;

			thumbPosX = (transform.position.x - backgroundCollider.bounds.min.x) / (backgroundCollider.bounds.max.x - backgroundCollider.bounds.min.x);
			thumbPosX = Mathf.Clamp(thumbPosX, 0f, 1f);
			thumbPosY = (transform.position.y - backgroundCollider.bounds.min.y) / (backgroundCollider.bounds.max.y - backgroundCollider.bounds.min.y);
			thumbPosY = Mathf.Clamp(thumbPosY, 0f, 1f);

			if (huePicker)
				huePicker.SetHue(thumbPosX);

			if (saturationBrightnessPicker)
			{
				saturationBrightnessPicker.SetSaturation(thumbPosX);
				saturationBrightnessPicker.SetBrightness(thumbPosY);
			}
		}
	}

	public void SetPosition(Vector2 position)
	{
		transform.position = (backgroundCollider.bounds.max - backgroundCollider.bounds.min) * position + (Vector2)backgroundCollider.bounds.min;
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		dragging = true;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		dragging = false;
	}
}
