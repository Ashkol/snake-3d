namespace MobileGame.Snake
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIColorPartToggleGroup : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> hexadecimalColorValues;
        [SerializeField] private List<UIColorPartToggle> colorToggles;

        private void Awake()
        {
            foreach (var toggle in GetComponentsInChildren<UIColorPartToggle>())
            {
                if (!colorToggles.Contains(toggle))
                    colorToggles.Add(toggle);
            }
        }

        public void SetColorValues(Color color)
        {
            foreach (var UIColorPartToggle in colorToggles)
            {
                if (UIColorPartToggle.isOn)
                {
                    UIColorPartToggle.SetHexadecimalColorLabel(color);
                }
            }
        }
    }
}