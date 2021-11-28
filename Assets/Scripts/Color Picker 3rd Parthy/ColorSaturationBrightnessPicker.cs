using UnityEngine;
using UnityEngine.UI;

public class ColorSaturationBrightnessPicker : MonoBehaviour 
{
	[SerializeField] private ColorIndicator colorIndicator;
	[SerializeField] private UIThumb thumb;
	public Material backgroundMaterial;

	private void Awake()
	{
		backgroundMaterial = GetComponent<Image>().material;
	}

	public void SetBrightness(float brightness)
	{
		colorIndicator.SetBrightness((brightness * 100f).ToString());
	}

	public void SetSaturation(float saturation)
	{
		colorIndicator.SetSaturation((saturation * 100f).ToString());
	}

	public void SetHue(HSBColor color)
	{
		backgroundMaterial.SetColor("_Color", new HSBColor(color.h, 1, 1).ToColor());
		//SendMessage("SetDragPoint", new Vector3(color.s, color.b, 0));
	}

	public void SetThumb(Vector2 thumbPosition)
	{
		thumb.SetPosition(thumbPosition);
	}

	void OnDrag(Vector3 point)
    {
		transform.parent.BroadcastMessage("SetSaturationBrightness", new Vector2(point.x, point.y));
    }

  //  void SetHue(float hue)
  //  {
		//backgroundMaterial.SetColor("_Color", new HSBColor(hue, 1, 1).ToColor());
  //  }	
}
