namespace MobileGame.Snake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class ScoreBoard 
    {
        //Dictionary<int, int> levelScoreDict;
        [SerializeField]
        List<int> bestScores;

        public ScoreBoard()
        {
            bestScores = new List<int>();
        }

        public ScoreBoard(int numberOfLevels)
        {
            bestScores = new List<int>();
            for (int i = 0; i < numberOfLevels; i++)
            {
                bestScores.Add(0);
            }
        }

        public void SetBestScore(int level, int score)
        {
            if (bestScores[level-1] <= score)
            {
                bestScores[level-1] = score;
            }
        }

        public int GetScore(int level)
        {
            if (level <= bestScores.Count && level > 0)
                return bestScores[level-1];
            else
                return 0;
        }

        public void ResetScore()
        {
            for (int i = 0; i < bestScores.Count; i++)
            {
                bestScores[i] = 0;
            }
        }
    }
}

