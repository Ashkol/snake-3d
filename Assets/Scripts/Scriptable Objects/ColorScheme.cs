namespace MobileGame.Snake
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Color Scheme", menuName = "ScriptableObjects/ColorScheme", order = 1)]
    public class ColorScheme : ScriptableObject
    {
        public string colorSchemeName;

        public Color snakeHeadColor;
        public Color snakeBodyColor;
        public Color terrainColor;
        public Color pickupColor;
        public Color backgroundColor;
    }
}
