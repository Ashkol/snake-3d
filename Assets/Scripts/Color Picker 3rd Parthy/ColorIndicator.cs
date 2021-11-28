using System;
using UnityEngine;
using UnityEngine.UI;
using MobileGame.Snake;
using TMPro;
using System.Collections.Generic;

public class ColorIndicator : MonoBehaviour {

	[SerializeField] private ColorSaturationBrightnessPicker saturationBrightnessPicker;
	[SerializeField] private ColorHuePicker huePicker;
	[SerializeField] private PickerCustomizer customizer;
	[SerializeField] private TextMeshProUGUI hexadecimalColor;
	[SerializeField] private UIColorPartToggleGroup colorPartToggles;
	[SerializeField] private List<Pickup> pickups;
	[SerializeField] private List<TMP_InputField> HSVFields;
	private HSBColor color;
	private ColorScheme palette;

	void Start() {
		//palette = new ColorScheme();
		palette = GameManager.instance.CurrentColorScheme;
		color = HSBColor.FromColor(GetComponent<Image>().material.GetColor("_Color"));
		//transform.parent.BroadcastMessage("SetColor", color);
	}

	void ApplyColor ()
	{
		GetComponent<Image>().material.SetColor ("_Color", color.ToColor());
		saturationBrightnessPicker.SetHue(color);

		SetThumbs();
		SetHSVFieldsValues();
		customizer.SetPaletteColor(color.ToColor());
		//hexadecimalColor.text = ColorExtensions.ColorToHexadecimal(color.ToColor());
		colorPartToggles.SetColorValues(color.ToColor());
		SetPickupsColor();
		//transform.parent.BroadcastMessage("OnColorChange", color, SendMessageOptions.DontRequireReceiver);
	}

	public void SetHSVFieldsValues()
	{
		if (HSVFields.Count == 3)
		{
			HSVFields[0].text = ((int)(color.h * 359)).ToString();
			HSVFields[1].text = ((int)(color.s * 100)).ToString();
			HSVFields[2].text = ((int)(color.b * 100)).ToString();
		}
	}

	private void SetThumbs()
	{
		huePicker.SetThumb(new Vector2(color.h, 0));
		saturationBrightnessPicker.SetThumb(new Vector2(color.s, color.b));
	}

	private void SetPickupsColor()
	{
		foreach (var pickup in pickups)
		{
			pickup.SetColor(customizer.Palette);
		}
	}

	public void SetHue(string hue)
	{
		try
		{
			color.h = float.Parse(hue) / 359f;
			ApplyColor();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);

		}
	}

	public void SetSaturation(string saturation)
	{
		try
		{
			color.s = float.Parse(saturation) / 100f;
			ApplyColor();
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
	}

	public void SetBrightness(string brightness)
	{
		try
		{
			color.b = float.Parse(brightness) / 100f;
			ApplyColor();
		}
		catch (Exception ex)
		{
			Debug.Log(ex);

		}
	}

	public void SetColor(Color color)
	{
		Color.RGBToHSV(color, out this.color.h, out this.color.s, out this.color.b);
		GetComponent<Image>().material.SetColor("_Color", color);
		saturationBrightnessPicker.SetHue(this.color);
		SetThumbs();
		//hexadecimalColor.text = ColorExtensions.ColorToHexadecimal(color);
	}

	void SetSaturationBrightness(Vector2 sb) {
		color.s = sb.x;
		color.b = sb.y;
		ApplyColor();
	}
}
