namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PickerCustomizer : MonoBehaviour
    {
        public ColorPart part;

        [SerializeField] private Transform snake;
        [SerializeField] private Transform terrain;
        [SerializeField] private MainCamera sceneCamera;
        [SerializeField] private List<Pickup> pickups;
        [SerializeField] private CustomizationManager customizationManager;
        [SerializeField] private ColorIndicator colorIndicator;

        public ColorScheme Palette
        {
            get { return palette; }
            set
            {
                switch (part)
                {
                    case ColorPart.Head:
                        palette.snakeHeadColor = value.snakeHeadColor;
                        break;
                    case ColorPart.Body:
                        palette.snakeBodyColor = value.snakeBodyColor;
                        break;
                    case ColorPart.Terrain:
                        palette.terrainColor = value.terrainColor;
                        break;
                    case ColorPart.PickUp:
                        palette.pickupColor = value.pickupColor;
                        break;
                    case ColorPart.Background:
                        palette.backgroundColor = value.backgroundColor;
                        break;
                    default:
                        break;
                }
                customizationManager.SetColors(palette);
            }
        }
        private ColorScheme palette;

        private void Awake()
        {
            if (GameManager.instance)
            {
                palette = Instantiate(GameManager.instance.CurrentColorScheme);
            }
            else
            {
                palette = new ColorScheme();
            }
        }

        public enum ColorPart
        {
            Head,
            Body,
            Terrain,
            PickUp,
            Background
        }

        public void SetColorPart(int i)
        {
            part = (ColorPart)i;
            switch (part)
            {
                case ColorPart.Head:
                    colorIndicator.SetColor(palette.snakeHeadColor);
                    break;
                case ColorPart.Body:
                    colorIndicator.SetColor(palette.snakeBodyColor);
                    break;
                case ColorPart.Terrain:
                    colorIndicator.SetColor(palette.terrainColor);
                    break;
                case ColorPart.PickUp:
                    colorIndicator.SetColor(palette.pickupColor);
                    break;
                case ColorPart.Background:
                    colorIndicator.SetColor(palette.backgroundColor);
                    break;
                default:
                    break;
            }
        }

        public void SetPaletteColor(Color color)
        {
            switch (part)
            {
                case ColorPart.Head:
                    palette.snakeHeadColor = color;
                    break;
                case ColorPart.Body:
                    palette.snakeBodyColor = color;
                    break;
                case ColorPart.Terrain:
                    palette.terrainColor = color;
                    break;
                case ColorPart.PickUp:
                    palette.pickupColor = color;
                    break;
                case ColorPart.Background:
                    palette.backgroundColor = color;
                    break;
                default:
                    break;
            }
            customizationManager.SetColors(palette);
        }
    }
}