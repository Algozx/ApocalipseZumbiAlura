using System;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public EventHandler OnBossSpawn;
    
    [SerializeField] private float timeToSpawn ;
    [SerializeField] private float timeBetweenSpawns = 60;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform[] spawnPoints;
    
    private Transform _player;
    private void Start()
    {
        timeToSpawn = timeBetweenSpawns;
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        var bossInScene = GameObject.FindWithTag("Boss");
        bool canSpawn = Time.timeSinceLevelLoad > timeToSpawn && bossInScene == null;
        
        SpawnBoss(canSpawn);
    }

    /// <summary>
    /// Call this function to spawn a Boss in spawn point.
    /// </summary>
    private void SpawnBoss(bool canSpawn)
    {
        if (canSpawn)
        {
            Vector3 spawnPoint = FurthestFromPlayer();
            Instantiate<GameObject>(bossPrefab, spawnPoint, Quaternion.identity);
            OnBossSpawn?.Invoke(this, EventArgs.Empty);
            timeToSpawn = Time.timeSinceLevelLoad + timeBetweenSpawns;
        }
    }
    
    /// <summary>
    /// Call this function to check the higher distance from player, using spawn points.
    /// </summary>
    Vector3 FurthestFromPlayer()
    {
        Vector3 posFurthest = Vector3.zero;
        float higherDistance = 0;
        foreach (Transform pos in spawnPoints)
        {
            float disBetweenPlayer = Vector3.Distance(pos.position, _player.position);
            if (disBetweenPlayer > higherDistance)
            {
                higherDistance = disBetweenPlayer;
                posFurthest = pos.position;
            }
        }
        return posFurthest;
    }
}
