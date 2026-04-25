using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerControllerScript : MonoBehaviour
    {
        private static bool _resetTimer;

        private static bool _timerIsRunning = true;

        [SerializeField] [Header("Timer Settings (Given in seconds)")]
        private float timerDuration = 10f;

        [Header("Sound Settings")] [SerializeField]
        private AudioSource timerAudioSource;

        [SerializeField] private AudioClip loweTimeSound;

        [SerializeField] private float loweTimeSoundVolume;

        [SerializeField] private float loweTimeThreshold = 25f;
        [SerializeField] private float beepEvery = 1f;
        private float nextBeepTime;

        [Header("References")] [SerializeField]
        private TextMeshProUGUI timerText;

        private float _resetToTimer;


        private void Start()
        {
            _resetToTimer = timerDuration;
        }

        private void LateUpdate()
        {
            if (_resetTimer)
            {
                timerDuration = _resetToTimer;
                _resetTimer = false;
                _timerIsRunning = false;
            }

            if (!_timerIsRunning) return;

            timerDuration -= Time.deltaTime;

            timerText.text = ToTimerText(timerDuration);
            SoundTimer();
        }

        private static string ToTimerText(float time)
        {
            var sec = (int)time;
            var m = sec / 60;
            var s = sec - m * 60;
            var ms = (int)((time - sec) * 100);
            return $"{m:00}:{s:00}.{ms:00}";
        }
        
        private void SoundTimer()
        {
            if (!(timerDuration <= loweTimeThreshold)) return;
            if (timerDuration > nextBeepTime) return;
            
            nextBeepTime = timerDuration - beepEvery;
            Debug.Log("Beep!");
            //timerAudioSource.PlayOneShot(loweTimeSound, loweTimeSoundVolume);
        }

        public static void ResetTimer()
        {
            _resetTimer = true;
        }

        public static void StartTimer()
        {
            _timerIsRunning = true;
        }

        public static void StopTimer()
        {
            _timerIsRunning = false;
        }
    }
}