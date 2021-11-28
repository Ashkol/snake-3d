namespace AshkolTools.UI
{
    using UnityEngine.UI;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ToggleImageSwap : Toggle
    {
        public Sprite onImage;
        public Sprite offImage;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            
            if (isOn)
                image.sprite = onImage;
            else
                image.sprite = offImage;
        }

        //protected override void OnEnable()
        protected override void Start()
        {
            base.Start();
            if (isOn)
                image.sprite = onImage;
            else
                image.sprite = offImage;
        }

        public void RefreshImage()
        {
            if (isOn)
                image.sprite = onImage;
            else
                image.sprite = offImage;
        }
    }
}
