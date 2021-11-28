namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameSettings
    {
        public bool MusicOn;
        public bool SoundEffectsOn;
        public bool fourDirectionControllerOn;
        public ColorScheme currentColorScheme;

        private List<ColorScheme> colorPalettes;

        public GameSettings(List<ColorScheme> colorPalettes)
        {
            this.colorPalettes = colorPalettes;
        }

        public void Load()
        {
            currentColorScheme = LoadColorPlayerPrefs();
            MusicOn = LoadMusicPlayerPrefs();
            SoundEffectsOn = LoadSoundEffectsPlayerPrefs();
            fourDirectionControllerOn = LoadControllerModePlayerPrefs();
        }

        public void Save()
        {
            if (colorPalettes.IndexOf(currentColorScheme) < 0)
                PlayerPrefs.SetInt("color", 0);
            else
                PlayerPrefs.SetInt("color", colorPalettes.IndexOf(currentColorScheme));
            Debug.Log("MusicOn? " + MusicOn);
            PlayerPrefs.SetInt("music", MusicOn ? 1 : 0);
            PlayerPrefs.SetInt("sound_effects", SoundEffectsOn ? 1 : 0);
            PlayerPrefs.SetInt("controller_mode", fourDirectionControllerOn ? 1 : 0);
            PlayerPrefs.Save();
        }


        #region Loading
        private ColorScheme LoadColorPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("color"))
            {
                Debug.Log(PlayerPrefs.GetInt("color"));
                return colorPalettes[PlayerPrefs.GetInt("color")];
            }
            else
            {
                PlayerPrefs.SetInt("color", 0);
                return colorPalettes[0];
            }
        }

        private bool LoadMusicPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("music"))
            {
                Debug.Log("Prefs " + PlayerPrefs.GetInt("music"));
                return PlayerPrefs.GetInt("music") == 1;
            }
            else
            {
                PlayerPrefs.SetInt("music", 1);
                return true;
            }
        }

        private bool LoadControllerModePlayerPrefs()
        {
            if (PlayerPrefs.HasKey("controller_mode"))
            {
                return PlayerPrefs.GetInt("controller_mode") == 1;
            }
            else
            {
                PlayerPrefs.SetInt("controller_mode", 1);
                return true;
            }
        }

        private bool LoadSoundEffectsPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("sound_effects"))
            {
                return PlayerPrefs.GetInt("sound_effects") == 1;
            }
            else
            {
                PlayerPrefs.SetInt("sound_effects", 1);
                return true;
            }
        }
#endregion
    }


}