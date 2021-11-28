namespace MobileGame.Snake
{
    using UnityEngine;

    public class BackAndroidBehaviour : MonoBehaviour
    {
        private float timerMax = 2f;
        private float _timer;

        void Awake()
        {
            _timer = 0f;
        }

        void Update()
        {
#if UNITY_ANDROID
            //if (Event.current.Equals(Event.KeyboardEvent("Escape")))
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                {
                    if (Time.time - _timer < timerMax)
                    {
                        Application.Quit();
                    }
                    else
                    {
                        _timer = Time.time;
                        ShowAndroidToast("Press again to exit");
                    }
                }
#endif
            }
        }

        private void ShowAndroidToast(string toastText)
        {
            //create a Toast class object
            AndroidJavaClass toastClass =
                        new AndroidJavaClass("android.widget.Toast");

            //create an array and add params to be passed
            object[] toastParams = new object[3];
            AndroidJavaClass unityActivity =
              new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            toastParams[0] =
                         unityActivity.GetStatic<AndroidJavaObject>
                                   ("currentActivity");
            toastParams[1] = toastText;
            toastParams[2] = toastClass.GetStatic<int>
                                   ("LENGTH_SHORT");

            //call static function of Toast class, makeText
            AndroidJavaObject toastObject =
                            toastClass.CallStatic<AndroidJavaObject>
                                          ("makeText", toastParams);

            //show toast
            toastObject.Call("show");

        }
    }

}
