using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemiesPrefab;
    public List<SpawnEnemyWays> SpawnEnemyWaysList;
    private float _timeToWait;
    [HideInInspector]public int currentWave = -1;
    private bool _allEnemySpawned;
    private int _numbersOfEnemyInCurrentWave;
    private bool _isGameStarted;
    [SerializeField] private GameObject NextWavePanel;
    [SerializeField] private GameObject StartPanel;
    
    private void Update()
    {
        if(_isGameStarted)
            CheckIfAllDied();
    }

    private void CheckIfAllDied()
    {
        if (transform.childCount == 0)
        {
            NextWavePanel.SetActive(true);
        }
    }
    
    public void StartNewWave()
    {
        currentWave++;
        CheckIfHasMoreWaves();
        
        //setting the value for the wave
        StartPanel.SetActive(false);
        NextWavePanel.SetActive(false);
        _timeToWait = SpawnEnemyWaysList[currentWave].spawnTime;
        _allEnemySpawned = false;
        _isGameStarted = true;
        
        StartCoroutine(SpawnEnemyTimer());
    }
    private void CheckIfHasMoreWaves()
    {
        if (currentWave > SpawnEnemyWaysList.Count-1)
        {
            currentWave = SpawnEnemyWaysList.Count - 1;
        }
    }
  

    private IEnumerator SpawnEnemyTimer()
    {
        int randomValue;
        while (!_allEnemySpawned)
        {
            //the enemy will wake up from the left or from the right
            randomValue = Random.Range(0, 2) * 2 - 1;
            _numbersOfEnemyInCurrentWave++;
            _allEnemySpawned = CheckIfAllEnemyWasSpawned();
            
            SpawnEnemies(100 * randomValue);
            yield return new WaitForSeconds(_timeToWait);
        }
        //new Wave value settings
        _numbersOfEnemyInCurrentWave = 0;
    }

    private bool CheckIfAllEnemyWasSpawned()
    {
        return SpawnEnemyWaysList[currentWave]._enemyCount == _numbersOfEnemyInCurrentWave;
    }

    private void SpawnEnemies(float directionX)
    {
        int randomIndex = Random.Range(0, _enemiesPrefab.Length);
        
        GameObject newGameObject = Instantiate(_enemiesPrefab[randomIndex],
            new Vector3(directionX, -30f, 0f), Quaternion.identity);
        newGameObject.transform.parent = transform;
    }
    [System.Serializable]
    public class SpawnEnemyWays
    {
        public float spawnTime = 2;
        public int _enemyCount = 3;
    }
}