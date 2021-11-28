namespace MobileGame.Snake
{
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        // 1 - four directions, 0 - two directions

        public static InputManager instance;
        public bool FourDirectionControlOn
        {
            get { return fourDirectionControlOn; }
            set
            {
                fourDirectionControlOn = value;
                //if (fourDirectionControlOn)
                //{
                //    PlayerPrefs.SetInt("controller_mode", 1);
                //}
                //else
                //{
                //    PlayerPrefs.SetInt("controller_mode", 0);
                //}
                //PlayerPrefs.Save();
            }
        }
        private bool fourDirectionControlOn;

        private void Awake()
        {
            instance = this;
        }
    }

}