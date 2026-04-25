using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        [SerializeField] private AudioSource musicSource;
        
        [SerializeField] private float musicVolume = 1f;
        [SerializeField] private float soundVolume = 1f;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        
        public static void SetMusicVolume(float volume)
        {
            if (_instance == null) return;
            
            _instance.musicVolume = volume;
            if (_instance.musicSource != null)
                _instance.musicSource.volume = volume;
        }
        public static void SetSoundVolume(float volume)
        {
            if (_instance == null) return;
            
            _instance.soundVolume = volume;
        }
        
        public static float GetMusicVolume()
        {
            return _instance != null ? _instance.musicVolume : 1f;
        }
        public static float GetSoundVolume() {
            return _instance != null ? _instance.soundVolume : 1f;
        }
        
        public static float PlaySound(AudioClip clip) {
            if (_instance == null) return 0f;

            if (Camera.main != null) 
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, _instance.soundVolume);
            
            return clip.length;
            
        }

        public static float PlaySound(AudioClip clip, float volume)
        {
            if (_instance == null) return 0f;

            if (Camera.main != null) 
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
            
            return clip.length;
        }
        
        
        public static void PlayYMusic(AudioClip clip)
        {
            if (_instance == null || _instance.musicSource == null) return;
            
            _instance.musicSource.clip = clip;
            _instance.musicSource.volume = _instance.musicVolume;
            _instance.musicSource.Play();
        }

        public static void PlayMusic(AudioClip clip, float volume)
        {
            if (_instance == null || _instance.musicSource == null) return;
            
            _instance.musicSource.clip = clip;
            _instance.musicSource.volume = volume;
            _instance.musicSource.Play();
        }
        
        public static void StopMusic()
        {
            if (_instance == null || _instance.musicSource == null) return;
            
            _instance.musicSource.Stop();
        }
    }
}
