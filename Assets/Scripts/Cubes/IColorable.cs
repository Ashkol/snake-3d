namespace MobileGame.Snake
{
    using UnityEngine;
    public interface IColorable
    {
        void SetColor(ColorScheme scheme);

        Color GetColor();
    }
}
