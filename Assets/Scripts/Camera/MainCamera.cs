namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.Assertions;

    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour, IColorable
    {
        public Color BackgroundColor
        { 
            get { return mainCamera.backgroundColor; }
            set { mainCamera.backgroundColor = value; }
        }
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
            Assert.IsNotNull(mainCamera, "No camera attached");
        }

        private void OnEnable()
        {
            if (GameManager.instance)
            {
                mainCamera.backgroundColor = GameManager.instance.Settings.currentColorScheme.backgroundColor;
            }
        }

        public void SetColor(ColorScheme scheme)
        {
            mainCamera.backgroundColor = scheme.backgroundColor;
        }

        public Color GetColor()
        {
            return mainCamera.backgroundColor;
        }
    }
}