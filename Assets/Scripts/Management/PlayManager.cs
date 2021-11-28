namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayManager : MonoBehaviour
    {
        public static PlayManager instance;

        [SerializeField] private int currentLevel;
        [SerializeField] private ScoreGUI scoreGUI;
        [SerializeField] private SnakeMovement snake;
        [SerializeField] private CameraPivot cameraPivot;
        [SerializeField] private GameObject gameOverScreen;
        [Header("Controllers")]
        [SerializeField] private List<GameObject> controllers;
        private int score = 0;

        private void Awake()
        {
            instance = this;
        }

        private void SetController(int controller)
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                controllers[i].SetActive(i == controller);
            }
        }

        private void Start()
        {
            if (cameraPivot == null)
                cameraPivot = FindObjectOfType<CameraPivot>();
            SceneManager.SetActiveScene(gameObject.scene);
            GameManager.instance.PlayedGames += 1;
            SetController(GameManager.instance.Settings.fourDirectionControllerOn ? 1 : 0);
        }

        public void IncreaseScore(int points)
        {
            score += points;
            scoreGUI.SetScore(score);
        }

        public void ReturnToMainMenu()
        {
            GameManager.instance.PlayInterstitialAd();
            GameManager.instance.LoadScene((int)SceneIndexes.MAIN_MENU);
        }

        public void GameOver()
        {
            cameraPivot.StartRandomRotation();
            gameOverScreen.SetActive(true);
            GameManager.instance.ScoreBoard.SetBestScore(currentLevel, score);
            GameManager.instance.SavePlayerPrefs();
        }

        public void GameOverContinue()
        // USed in On Click event
        {
            ReturnToMainMenu();
        }
    }
}