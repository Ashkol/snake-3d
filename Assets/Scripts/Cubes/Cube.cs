namespace MobileGame.Snake
{
    using UnityEngine;

    public abstract class Cube : MonoBehaviour, IColorable
    {
        public abstract Color GetColor();

        public abstract void SetColor(ColorScheme scheme);

        protected virtual void OnEnable()
        {
            SetColor(GameManager.instance.CurrentColorScheme);
        }
    }

}
