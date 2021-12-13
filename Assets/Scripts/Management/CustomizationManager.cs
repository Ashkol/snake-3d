namespace MobileGame.Snake
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class CustomizationManager : MonoBehaviour
    {

        [SerializeField] private Transform snake;
        [SerializeField] private Transform terrain;
        [SerializeField] private MainCamera sceneCamera;
        [SerializeField] private List<Pickup> pickups;

        public event Action<ColorScheme> OnCurrentColorChange;

        public ColorScheme CurrentColorScheme 
        { 
            get 
            { 
                return GameManager.instance.CurrentColorScheme; 
            } 
            set 
            { 
                GameManager.instance.CurrentColorScheme = value;
                OnCurrentColorChange?.Invoke(value);
                RefreshCustomization();
            } 
        }

        public ColorScheme defaultColorScheme;
        public Pickup CurrentPickup
        {
            get
            {
                return GameManager.instance.CurrentPickup;
            }
            set
            {
                GameManager.instance.CurrentPickup = value;
                RefreshCustomization();
            }
        }
        public Pickup defaultPickup;

        private void Awake()
        {
            if (GameManager.instance != null)
                CurrentColorScheme = GameManager.instance.CurrentColorScheme;
            else
                CurrentColorScheme = defaultColorScheme;
            CurrentPickup = defaultPickup;
        }

        public void RefreshCustomization()
        {
            foreach (var obj in snake.GetComponentsInChildren<IColorable>())
            {
                obj.SetColor(GameManager.instance.CurrentColorScheme);
                //obj.gameObject.SetActive(false);
                //obj.gameObject.SetActive(true);
            }

            foreach (var obj in terrain.GetComponentsInChildren<IColorable>())
            {
                obj.SetColor(GameManager.instance.CurrentColorScheme);

                //obj.gameObject.SetActive(false);
                //obj.gameObject.SetActive(true);
            }

            sceneCamera.gameObject.SetActive(false);
            sceneCamera.gameObject.SetActive(true);
        }

        public void SetColors(ColorScheme palette)
        {
            foreach (var obj in snake.GetComponentsInChildren<IColorable>())
            {
                obj.SetColor(palette);
            }

            foreach (var obj in terrain.GetComponentsInChildren<IColorable>())
            {
                obj.SetColor(palette);
            }

            sceneCamera.SetColor(palette);
        }
    }
}