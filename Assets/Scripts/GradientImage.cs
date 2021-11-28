namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(RawImage))]
    [ExecuteInEditMode]
    public class GradientImage : MonoBehaviour
    {
        public RawImage img;
        [SerializeField] private Color skyColor, equatorColor, groundColor;

        private Texture2D backgroundTexture;

        void Awake()
        {
            if (img == null)
                img = GetComponent<RawImage>();
            backgroundTexture = new Texture2D(1, 3);
            backgroundTexture.wrapMode = TextureWrapMode.Clamp;
            backgroundTexture.filterMode = FilterMode.Bilinear;
            if (skyColor == null)
                skyColor = Color.white;
            if (groundColor == null)
                groundColor = Color.black;
            SetColor(skyColor, equatorColor,  groundColor);
        }

        public void SetColor(Color color1, Color color2, Color color3)
        {
            backgroundTexture.SetPixels(new Color[] { color1, color2, color3 });
            backgroundTexture.Apply();
            img.texture = backgroundTexture;
            Debug.Log("XD");
        }
    }

}

