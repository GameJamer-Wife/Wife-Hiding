using MainScreen;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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

        [Header("References")] [SerializeField]
        private TextMeshProUGUI timerText;

        private float _resetToTimer;
        private float _nextBeepTime;

        [SerializeField]
        private GameOverShowScript gameOverShowScript;


        private void Start()
        {
            _resetToTimer = timerDuration;
        }

        private void LateUpdate()
        {
            if (timerDuration <= 0)
            {
                gameOverShowScript.ShowGameOver("Time's up!", "You ran  out of time! Better luck next time.");
                return;
            }
            
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
            if (timerDuration > _nextBeepTime) return;

            _nextBeepTime = timerDuration - beepEvery;
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