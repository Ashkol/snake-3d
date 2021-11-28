namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject customizationScreen;
        [SerializeField] private GameObject settingsScreen;
        [SerializeField] private GameObject chooseLevelScreen;


        private void Start()
        {
            SceneManager.SetActiveScene(gameObject.scene);
        }

        public void OpenMainMenu()
        {
            mainMenuScreen.SetActive(true);
            settingsScreen.SetActive(false);
            customizationScreen.SetActive(false);
            chooseLevelScreen.SetActive(false);
        }

        public void OpenCustomizationScreen()
        {
            mainMenuScreen.SetActive(false);
            settingsScreen.SetActive(false);
            customizationScreen.SetActive(true);
            chooseLevelScreen.SetActive(false);
        }

        public void OpenSettingsScreen()
        {
            mainMenuScreen.SetActive(false);
            settingsScreen.SetActive(true);
            customizationScreen.SetActive(false);
            chooseLevelScreen.SetActive(false);
        }

        public void OpenChooseLevelScreen()
        {
            mainMenuScreen.SetActive(false);
            settingsScreen.SetActive(false);
            customizationScreen.SetActive(false);
            chooseLevelScreen.SetActive(true);
        }

        public void StartGame()
        {
            SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
            SceneManager.LoadScene((int)SceneIndexes.LEVEL_1, LoadSceneMode.Additive);
        }

        public void StartLevel(int level)
        {
            GameManager.instance.LoadScene(level + 1);
            //SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
            //SceneManager.LoadScene(level + 1, LoadSceneMode.Additive); // 0 for Master Scene, 1 for Main Menu 
        }

        public void StartColorPickerScene()
        {
            GameManager.instance.LoadScene((int)SceneIndexes.COLOR_PICKER);
        }
    }
}
