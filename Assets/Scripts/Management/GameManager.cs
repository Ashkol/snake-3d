namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Advertisements;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public AudioManager audioManager;
        public InputManager inputManager;

        public int PlayedGames
        {
            get { return playedGames; }
            set { playedGames = value; }
        }
        private int playedGames;
        [SerializeField] private int GamesBetweenAds = 5;

        public ColorScheme CurrentColorScheme
        {
            get
            {
                return GameManager.instance.Settings.currentColorScheme;
            }
            set
            {
                GameManager.instance.Settings.currentColorScheme = value;
            }
        }
        public List<ColorScheme> colorSchemes;
        public ColorScheme defaultColorScheme;
        public Pickup CurrentPickup { get; set; }
        public Pickup defaultPickup;

        public GameSettings Settings { get; set; }
        public GameSave GameSave { get; set; }
        public ScoreBoard ScoreBoard { get { return _scoreBoard; } private set { _scoreBoard = value; } }
        private ScoreBoard _scoreBoard;

        private void Awake()
        {
            //PlayerPrefs.DeleteAll();
            instance = this;
            Settings = new GameSettings(colorSchemes);
            GameSave = new GameSave();
            //SceneManager.UnloadSceneAsync((int)SceneIndexes.MASTER);
            SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
            //CurrentColorScheme = defaultColorScheme;
            CurrentPickup = defaultPickup;

            LoadSettings();
            LoadGameSave();
        }

        public void LoadSettings()
        {
            Settings.Load();
            audioManager.MusicOn = Settings.MusicOn;
            audioManager.SoundEffectsOn = Settings.SoundEffectsOn;
            inputManager.FourDirectionControlOn = Settings.fourDirectionControllerOn;
            CurrentColorScheme = Settings.currentColorScheme;
        }

        public void LoadGameSave()
        {
            GameSave.Load();
            PlayedGames = GameSave.PlayedGames;
            ScoreBoard = GameSave.ScoreBoard;

        }

        public void UpdateSettings()
        {
            Settings.MusicOn = audioManager.MusicOn;
            Settings.SoundEffectsOn = audioManager.SoundEffectsOn;
            Settings.fourDirectionControllerOn = inputManager.FourDirectionControlOn;
            Settings.currentColorScheme = CurrentColorScheme;
        }

        public void UpdateSaveGame()
        {
            GameSave.PlayedGames = playedGames;
            GameSave.Save();
        }

        private void Start()
        {

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.MAIN_MENU));
        }

        public void LoadScene(int buildIndex)
        {
            //SceneManager.UnloadScene(SceneManager.GetActiveScene)
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        }

        public void PlayInterstitialAd()
        {
            if (PlayedGames >= GamesBetweenAds)
            {
                PlayedGames = 0;
                Advertisement.Show();
                //insterstitialAd.ShowAd();
            }
        }

        public void SavePlayerPrefs()
        {
            UpdateSettings();
            UpdateSaveGame();
            Settings.Save();
            GameSave.Save();
        }

        void OnApplicationQuit()
        {
            SavePlayerPrefs();
        }
    }
}
