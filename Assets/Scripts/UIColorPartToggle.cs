namespace MobileGame.Snake
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIColorPartToggle : Toggle
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TextMeshProUGUI hexadecimalColorValue;

        public void SetHexadecimalColorLabel(Color color)
        {
            hexadecimalColorValue.text = ColorExtensions.ColorToHexadecimal(color);
        }
    }

}