namespace MobileGame.Snake
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    public class ColorPicker : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ColorScheme colorScheme;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private GameObject headColor;
        [SerializeField] private GameObject bodyColor;
        [SerializeField] private GameObject terrainColor;
        [SerializeField] private GameObject pickUpColor;
        [SerializeField] private GameObject backgroundColor;
        [SerializeField] private Button button;
        [Header("Customization manager")]
        [SerializeField] private CustomizationManager customizationManager;

        #endregion

        #region MonoBehaviour methods

        private void Start()
        {
            if (customizationManager == null)
                customizationManager = FindObjectOfType<CustomizationManager>();

            headColor.GetComponent<Image>().color = colorScheme.snakeHeadColor;
            bodyColor.GetComponent<Image>().color = colorScheme.snakeBodyColor;
            terrainColor.GetComponent<Image>().color = colorScheme.terrainColor;
            pickUpColor.GetComponent<Image>().color = colorScheme.pickupColor;
            backgroundColor.GetComponent<Image>().color = colorScheme.backgroundColor;

            SetUp();
        }

        #endregion

        #region Public methods

        public void Activate(bool isOn)
        {
            label.color = isOn ? Color.white : Color.gray;
        }

        #endregion

        #region Private methods

        private void SetUp()
        {
            Activate(customizationManager.CurrentColorScheme == colorScheme);
            button.onClick.AddListener(Button_OnClick);
            customizationManager.OnCurrentColorChange += CustomizationManager_OnCurrentColorChange;
        }

        #endregion

        #region Event callback

        private void Button_OnClick()
        {
            Activate(customizationManager.CurrentColorScheme == colorScheme);
            transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, vibrato:1);
        }

        private void CustomizationManager_OnCurrentColorChange(ColorScheme currentColorScheme)
        {
            Activate(customizationManager.CurrentColorScheme == colorScheme);
        }

        #endregion
    }
}