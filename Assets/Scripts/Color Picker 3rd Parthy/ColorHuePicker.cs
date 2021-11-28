using UnityEngine;

public class ColorHuePicker : MonoBehaviour
{
	[SerializeField] private ColorIndicator colorIndicator;
	[SerializeField] private UIThumb thumb;

	public void SetHue(float hue)
	{
		colorIndicator.SetHue((hue * 359).ToString());
	}

	public void SetThumb(Vector2 thumbPosition)
	{
		thumb.SetPosition(thumbPosition);
	}

	void SetColor(HSBColor color)
	{
		SendMessage("SetDragPoint", new Vector3(color.h, 0, 0));
	}	

    void OnDrag(Vector3 point)
    {
		transform.parent.BroadcastMessage("SetHue", point.x);
    }
}
