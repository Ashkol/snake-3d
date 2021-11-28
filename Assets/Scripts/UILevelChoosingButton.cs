namespace MobileGame.Snake
{
    using UnityEngine;
    using TMPro;

    public class UILevelChoosingButton : MonoBehaviour
    {
        [SerializeField] private int level;
        [SerializeField] private TextMeshProUGUI bestScoreText;
       

        void Start()
        {
            if (bestScoreText != null)
            {
                bestScoreText.text = $"Best: {GameManager.instance.ScoreBoard.GetScore(level).ToString()}";
            }
        }
    }
}