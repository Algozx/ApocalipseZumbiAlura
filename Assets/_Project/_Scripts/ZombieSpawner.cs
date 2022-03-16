using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float spawnRange = 3;
    [SerializeField] private int maxZombieInSpawn = 3;
    [SerializeField] private float timeToChangeDifficulty = 60f;
    [SerializeField] private float minDisToSpawn = 20;
    [SerializeField] private LayerMask layerZombie;
    
    
    private int _zombiesAlive;
    private float _timerDifficulty;
    private GameObject _player;
    private float _timer;

    private void Start()
    {
        _timerDifficulty = timeToChangeDifficulty;
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        bool canSpawn = Vector3.Distance(transform.position,
            _player.transform.position) > minDisToSpawn;

        if (canSpawn && _zombiesAlive < maxZombieInSpawn)
        {
            _timer += Time.deltaTime;
            if (_timer >= timeToSpawn)
            {
                StartCoroutine(SpawnZombie());
                _timer = 0;
            }
        }
        
        ChangeDifficulty();
    }


    /// <summary>
    /// Call this function to Instantiate a new Zombie inside a spawn area.
    /// </summary>
    IEnumerator SpawnZombie()
    {
        Vector3 pos = RandomPos();
        Collider[] colliders = Physics.OverlapSphere(pos, 1, layerZombie);
        while (colliders.Length > 0)
        {
            pos = RandomPos();
            colliders = Physics.OverlapSphere(pos, 1, layerZombie);
            yield return null;
        }
        Instantiate(zombie, pos, transform.rotation);
        _zombiesAlive++;
    }

    /// <summary>
    /// Call this function to generate a random position inside a spawn area.
    /// </summary>
    Vector3 RandomPos()
    {
        Vector3 pos = Random.insideUnitSphere * spawnRange;
        pos += transform.position;
        pos.y = 0;

        return pos;
    }
    
    /// <summary>
    /// Call this function change the game difficulty, by adding more zombies over time.
    /// </summary>
    private void ChangeDifficulty()
    {
        var increaseMaxZombies = 1;
        
        if (Time.timeSinceLevelLoad > _timerDifficulty)
        {
            maxZombieInSpawn += increaseMaxZombies;
            _timerDifficulty = Time.timeSinceLevelLoad + timeToChangeDifficulty;
        }
    }
    
    /// <summary>
    /// Call this function to update and decrease the amount of zombies alive in map.
    /// </summary>
    public void DecreaseZombiesAlive()
    {
        _zombiesAlive--;
    }
}
