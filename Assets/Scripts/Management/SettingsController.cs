namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using AshkolTools.UI;

    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private ToggleImageSwap soundOn;
        [SerializeField] private ToggleImageSwap musicOn;
        [SerializeField] private ToggleImageSwap fourDirectionsControllerOn;

        private void Start()
        {
            //SceneManager.sceneLoaded += ConnectButtonsToSettings;
            soundOn.onValueChanged.AddListener((value) => AudioManager.instance.SoundEffectsOn = value);
            soundOn.onValueChanged.AddListener((_) => GameManager.instance.SavePlayerPrefs());
            soundOn.isOn = AudioManager.instance.SoundEffectsOn;
            soundOn.RefreshImage();

            musicOn.onValueChanged.AddListener((value) => AudioManager.instance.MusicOn = value);
            musicOn.onValueChanged.AddListener((_) => GameManager.instance.SavePlayerPrefs());
            musicOn.isOn = AudioManager.instance.MusicOn;
            musicOn.RefreshImage();

            fourDirectionsControllerOn.onValueChanged.AddListener((value) => InputManager.instance.FourDirectionControlOn = value);
            fourDirectionsControllerOn.onValueChanged.AddListener((_) => GameManager.instance.SavePlayerPrefs());
            fourDirectionsControllerOn.isOn = InputManager.instance.FourDirectionControlOn;
            fourDirectionsControllerOn.RefreshImage();
        }

        private void ConnectButtonsToSettings(Scene scene, LoadSceneMode mode)
        // TO DO
        {
            Debug.Log("Connecting");
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ConnectButtonsToSettings;
        }
    }
}