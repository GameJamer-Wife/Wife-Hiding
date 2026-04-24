using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace Wife
{
    public class WifeManagerScript : MonoBehaviour
    {
        [SerializeField] private float wifeMaxMoveDistance = 10f;
        [SerializeField] private float wifeMoveSmoothTime = 0.2f;
        [SerializeField] private float wifeWaitTime = 3f;
        [SerializeField] private float timeUntilWife;

        [SerializeField] private RawImage _wifeImage;
        [SerializeField] private List<GameObject> _wifeSpawnPoints;

        [SerializeField] private WifeSpawnPointScript _currentWifeSpawnPoint;

        [SerializeField] private bool _wifeIsLeaving;

        private Vector2 _wifeVelocity;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _wifeImage.enabled = false;
            foreach (var spawnPoint in _wifeSpawnPoints.ToList())
            {
                spawnPoint.SetActive(false);
                if (spawnPoint.GetComponent<WifeSpawnPointScript>()) continue;

                Debug.LogError($"Spawn point {spawnPoint.name} missing WifeSpawnPointScript");
                _wifeSpawnPoints.Remove(spawnPoint);
            }

            WakeUpWife();
        }

        // Update is called once per frame
        private void Update()
        {
            if (timeUntilWife > 0f)
            {
                timeUntilWife -= Time.deltaTime;
                return;
            }

            UpdateWifePosition();
        }


        private WifeSpawnPointScript PickRandomSpawnPoint()
        {
            if (_wifeSpawnPoints.Count == 0)
            {
                Debug.LogError("No spawn points available for the wife!");
                return null;
            }

            var randomIndex = Random.Range(0, _wifeSpawnPoints.Count);
            var selectedSpawnPoint = _wifeSpawnPoints[randomIndex];
            selectedSpawnPoint.SetActive(true);

            return selectedSpawnPoint.GetComponent<WifeSpawnPointScript>();
        }


        private void MoveWife(Vector2 endpoint, Quaternion targetRotation)
        {
            Vector2 currentPosition = _wifeImage.transform.position;
            var newPosition = Vector2.SmoothDamp(
                currentPosition,
                endpoint,
                ref _wifeVelocity,
                wifeMoveSmoothTime,
                wifeMaxMoveDistance);

            _wifeImage.transform.SetPositionAndRotation(
                new Vector3(newPosition.x, newPosition.y, _wifeImage.transform.position.z),
                targetRotation);
        }

        private void UpdateWifePosition()
        {
            _wifeImage.enabled = true;
            if (!_currentWifeSpawnPoint)
            {
                _currentWifeSpawnPoint = PickRandomSpawnPoint();
                _wifeIsLeaving = false;
                _wifeVelocity = Vector2.zero;
                _wifeImage.transform.position = _currentWifeSpawnPoint.spawnPoint.position;
                _wifeImage.transform.rotation = _currentWifeSpawnPoint.enterRotation;
            }

            Vector2 currentPosition = _wifeImage.transform.position;
            Vector2 destination = _wifeIsLeaving
                ? _currentWifeSpawnPoint.spawnPoint.position
                : _currentWifeSpawnPoint.targetPoint.position;

            var destinationRotation = _currentWifeSpawnPoint.enterRotation;
            if (_wifeIsLeaving)
            {
                destinationRotation = Quaternion.Euler(0, _currentWifeSpawnPoint.enterRotation.eulerAngles.y + 180, 0);
            }

            var shouldMove = Vector2.Distance(currentPosition, destination) > 0.05f;

            if (shouldMove)
            {
                MoveWife(destination, destinationRotation);
                return;
            }

            if (!_wifeIsLeaving && wifeWaitTime > 0)
            {
                wifeWaitTime -= Time.deltaTime;
                Debug.Log("Wife waiting on screen");
                return;
            }

            if (!_wifeIsLeaving)
            {
                _wifeIsLeaving = true;
                _wifeVelocity = Vector2.zero;
                return;
            }

            wifeWaitTime = Random.value * 5f;
            timeUntilWife = Random.value * 5f + 5f;
            _currentWifeSpawnPoint.gameObject.SetActive(false);
            _wifeIsLeaving = false;
            _currentWifeSpawnPoint = null;
            _wifeVelocity = Vector2.zero;
            _wifeImage.enabled = false;
        }

        public void WakeUpWife()
        {
            timeUntilWife = Random.value * 5f + 5f;
            wifeWaitTime = Random.value * 5f;
            _wifeIsLeaving = false;
            _wifeVelocity = Vector2.zero;
            _wifeImage.enabled = true;
            Debug.Log($"Wife is waking up! {timeUntilWife} seconds until she is fully awake.");
        }

        public void SleepWife()
        {
            timeUntilWife = 0;
            _wifeIsLeaving = false;
            _wifeVelocity = Vector2.zero;
            _currentWifeSpawnPoint = null;
            _wifeImage.enabled = false;
            foreach (var spawnPoint in _wifeSpawnPoints) spawnPoint.SetActive(false);
        }
    }
}