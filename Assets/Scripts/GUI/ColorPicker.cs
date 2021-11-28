namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ColorPicker : MonoBehaviour
    {
        [SerializeField] private ColorScheme colorScheme;
        
        [SerializeField] private GameObject headColor;
        [SerializeField] private GameObject bodyColor;
        [SerializeField] private GameObject terrainColor;
        [SerializeField] private GameObject pickUpColor;
        [SerializeField] private GameObject backgroundColor;

        private void OnEnable()
        {
            headColor.GetComponent<Image>().color = colorScheme.snakeHeadColor;
            bodyColor.GetComponent<Image>().color = colorScheme.snakeBodyColor;
            terrainColor.GetComponent<Image>().color = colorScheme.terrainColor;
            pickUpColor.GetComponent<Image>().color = colorScheme.pickupColor;
            backgroundColor.GetComponent<Image>().color = colorScheme.backgroundColor;
        }
    }
}