namespace MobileGame.Snake
{
    using UnityEngine;

    public class GameSave
    {
        public ScoreBoard ScoreBoard { get { return _scoreBoard; } set { _scoreBoard = value; } }
        private ScoreBoard _scoreBoard;
        public int PlayedGames { get { return _playedGames; } set { _playedGames = value; } }
        private int _playedGames;

        public void Load()
        {
            _scoreBoard = LoadScoreBoard();
            _playedGames = LoadPlayedGames();
        }

        public void Save()
        {
            PlayerPrefs.SetString("scoreboard", JsonUtility.ToJson(_scoreBoard));
            PlayerPrefs.SetInt("played_games", _playedGames);
            PlayerPrefs.Save();
        }

        #region Loading
        private int LoadPlayedGames()
        {
            if (PlayerPrefs.HasKey("played_games"))
            {
                return PlayerPrefs.GetInt("played_games");
            }
            else
            {
                PlayerPrefs.SetInt("played_games", 0);
                PlayerPrefs.Save();
                return 0;
            }
        }

        private ScoreBoard LoadScoreBoard()
        {
            ScoreBoard scoreBoard;
            if (PlayerPrefs.HasKey("scoreboard"))
            {
                scoreBoard = (ScoreBoard)JsonUtility.FromJson(PlayerPrefs.GetString("scoreboard"), typeof(ScoreBoard));
                if (scoreBoard == null)
                {
                    scoreBoard = new ScoreBoard(9);
                    PlayerPrefs.SetString("scoreboard", JsonUtility.ToJson(scoreBoard));
                    PlayerPrefs.Save();
                }
            }
            else
            {
                scoreBoard = new ScoreBoard(9);
                PlayerPrefs.SetString("scoreboard", JsonUtility.ToJson(scoreBoard));
                PlayerPrefs.Save();
            }

            return scoreBoard;
        }
    }
    #endregion
}
