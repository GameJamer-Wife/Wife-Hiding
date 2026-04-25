using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Wife
{
    public class WifeManagerScript2 : MonoBehaviour
    {
        [SerializeField] private RawImage wifeImage;
        [SerializeField] private List<GameObject> wifeSpawnPoints;

        // What state the wife is in
        [SerializeField] private WifeStates currentWifeState;
        [SerializeField] private float wifeMaxMoveDistance = 130f;
        [SerializeField] private float wifeMoveSmoothTime = 0.2f;

        // How long wife stays on screen
        [SerializeField] private float wifeWaitTime = 3f;

        // How long wife waits before re-entering the screen
        [SerializeField] private float timeUntilWife;

        // If wife is active
        [SerializeField] private bool wifeIsAwake;

        // How long the wife should wait before wandering again
        [SerializeField] private float wanderCooldown = 2;

        // Max wander distance (percentage of screen size from 0)
        [SerializeField] private float maxWanderDistance;


        private WifeSpawnPointScript _currentSpawnPoint;
        private Vector2 _currentWanderTargetPosition;
        private Vector2 _wifeVelocity;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            wifeImage.enabled = false;
            foreach (var spawnPoint in wifeSpawnPoints.ToList())
            {
                spawnPoint.SetActive(false);
                if (spawnPoint.GetComponent<WifeSpawnPointScript>()) continue;

                Debug.LogError($"Spawn point {spawnPoint.name} missing WifeSpawnPointScript");
                wifeSpawnPoints.Remove(spawnPoint);
            }

            WakeUpWife();
            
        }
    
        
        private void WifeWander()
        {
            Debug.Log("Wife is wandering...");
            if (!(wifeWaitTime > 0))
            {
                currentWifeState = WifeStates.WalkingToSpawn;
                return;
            }
            wifeWaitTime  -= Time.deltaTime;
            if (wanderCooldown > 0)
            {
                wanderCooldown -= Time.deltaTime;
                return;
            }
            
            if(Vector2.Distance(wifeImage.transform.position, _currentWanderTargetPosition) > 0.1f)
            {
                Debug.Log("Wife is moving towards wander target...");
                MoveWife(new Vector2(_currentWanderTargetPosition.x, wifeImage.transform.position.y), 
                    wifeImage.transform.rotation);
                return;
            }

            var screenSize = new Vector2(Screen.width, Screen.height);
            var randomDirection = Random.insideUnitCircle.normalized;
            var randomDistance = Random.Range(0, maxWanderDistance) * screenSize.magnitude;
            _currentWanderTargetPosition = (Vector2)wifeImage.transform.position + randomDirection * randomDistance;

            // Clamp to screen bounds
            _currentWanderTargetPosition.x = Mathf.Clamp(_currentWanderTargetPosition.x, 0, screenSize.x /5);
            _currentWanderTargetPosition.y = wifeImage.transform.position.y;
            
            wanderCooldown = Random.Range(1f, 3f);
        }
        
        private void MoveWife(Vector2 endpoint, Quaternion targetRotation)
        {
            Vector2 currentPosition = wifeImage.transform.position;
            var newPosition = Vector2.SmoothDamp(
                currentPosition,
                endpoint,
                ref _wifeVelocity,
                wifeMoveSmoothTime,
                wifeMaxMoveDistance);

            wifeImage.transform.SetPositionAndRotation(
                new Vector3(newPosition.x, newPosition.y, wifeImage.transform.position.z),
                targetRotation);
        }

        private void UpdateWifePosition()
        {
            
            Vector2 currentPosition = wifeImage.transform.position;
            Vector2 destination;
            switch (currentWifeState)
            {
                case WifeStates.WalkingToTarget:
                    destination =  _currentSpawnPoint.targetPoint.position;
                    break;
                case WifeStates.WalkingToSpawn:
                    destination = _currentSpawnPoint.spawnPoint.position;
                    break;
                default:
                    destination = Vector2.zero;
                    break;
            }
            
            var destinationRotation = _currentSpawnPoint.enterRotation;
            if (currentWifeState == WifeStates.WalkingToSpawn)
                destinationRotation = Quaternion.Euler(0, _currentSpawnPoint.enterRotation.eulerAngles.y + 180, 0);

            var shouldMove = Vector2.Distance(currentPosition, destination) > 0.05f;

            if (shouldMove)
            {
                MoveWife(destination, destinationRotation);
                return;
            }
         
            switch (currentWifeState)
            {
                case WifeStates.WalkingToTarget:
                    currentWifeState = WifeStates.Wandering;
                    break;
                case WifeStates.WalkingToSpawn:
                    currentWifeState = WifeStates.Offscreen;
                    timeUntilWife = Random.Range(5f, 10f);
                    wifeWaitTime = Random.Range(3f, 6f);
                    wifeImage.enabled = false;
                    _currentSpawnPoint.gameObject.SetActive(false);
                    _currentSpawnPoint = null;
                    break;
                default:
                    Debug.LogError("Invalid wife state!");
                    currentWifeState = WifeStates.WalkingToSpawn;
                    break;
            }
        
            
        }


        private WifeSpawnPointScript PickRandomSpawnPoint()
        {
            var spawnPoint = wifeSpawnPoints[Random.Range(0, wifeSpawnPoints.Count)];
            spawnPoint.SetActive(true);
            Debug.Log(spawnPoint.name);
            return spawnPoint.GetComponent<WifeSpawnPointScript>();
        }
        
        private void Update()
        {
            if (!wifeIsAwake) return;
            if (timeUntilWife > 0f)
            {
                timeUntilWife -= Time.deltaTime;
                wifeImage.enabled = false;
                return;
            }
            wifeImage.enabled = true;

            if (currentWifeState == WifeStates.Offscreen)
            {
                currentWifeState = WifeStates.WalkingToTarget;
                _currentSpawnPoint = null;
            }

            switch (currentWifeState)
            {
                case WifeStates.WalkingToTarget:
                    if (!_currentSpawnPoint)
                    {
                        _currentSpawnPoint = PickRandomSpawnPoint();
                    } 
                    UpdateWifePosition();
                    break;
                case WifeStates.WalkingToSpawn:
                    UpdateWifePosition();
                    break;
                case WifeStates.Wandering:
                    WifeWander();
                    break;
            }
        }


        public void WakeUpWife()
        {
            wifeIsAwake = true;
        }

        public void SleepWife()
        {
            wifeIsAwake = false;
        }
    }
}