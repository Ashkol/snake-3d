namespace MobileGame.Snake
{
    using UnityEngine;

    public class TerrainCube : Cube
    {
        [SerializeField] private bool IsFallOff;

        private void Awake()
        {
            if (IsFallOff)
                Destroy(gameObject, 5f);
        }

        public override void SetColor(ColorScheme scheme)
        {
            if (scheme != null)
                GetComponent<MeshRenderer>().material.color = scheme.terrainColor;
        }

        public override Color GetColor()
        {
            return GetComponent<MeshRenderer>().material.color;
        }

    }

}
