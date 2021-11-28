namespace MobileGame.Snake
{
    using UnityEngine;

    public class BodyCube : Cube
    {
        public bool IsTail { get { return isTail; } private set { isTail = value; } }
        [SerializeField] private bool isTail;

        public override void SetColor(ColorScheme scheme)
        {
            var renderer = GetComponent<MeshRenderer>();
            if (renderer != null && scheme != null)
                renderer.material.color = scheme.snakeBodyColor;
        }

        public override Color GetColor()
        {
            return GetComponent<MeshRenderer>().material.color;
        }
    }

}