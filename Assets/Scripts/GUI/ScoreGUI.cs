namespace MobileGame.Snake
{
    using UnityEngine;
    using TMPro;

    public class ScoreGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void SetScore(int score)
        {
            text.text = score.ToString();
        }
    }
}