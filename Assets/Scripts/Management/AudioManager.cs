namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.Assertions;

    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        [SerializeField] private AudioSource musicSource;

        public bool MusicOn 
        {
            get { return musicOn; }
            set
            {
                Debug.Log($"MusicOn {value}");
                musicOn = value;
                if (musicOn)
                {
                    //PlayerPrefs.SetInt("music", 1);
                    musicSource.volume = 1f;
                }
                else
                {
                    //PlayerPrefs.SetInt("music", 0);
                    musicSource.volume = 0f;
                }
                //PlayerPrefs.Save();
            } 
        }
        private bool musicOn;
        public bool SoundEffectsOn 
        { 
            get { return soundEffectsOn; }
            set
            {
                soundEffectsOn = value;
                //if (soundEffectsOn)
                //{
                //    PlayerPrefs.SetInt("sound_effects", 1);
                //}
                //else
                //{
                //    PlayerPrefs.SetInt("sound_effects", 0);
                //}
                //PlayerPrefs.Save();
            }
        }
        private bool soundEffectsOn;


        void Awake()
        {
            if (musicSource == null)
                musicSource = GetComponent<AudioSource>();

            instance = this;
        }

        
    }
}