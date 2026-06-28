using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance { get; private set; }
    private enum MonsterType
    { 
        Hyena = 0,
        Snake,
        Cat,
    }

    [SerializeField] private Transform[] roadPointsParent;

    [SerializeField] private MobState monsterPrefab;
    [SerializeField] private MonsterData[] monsterData;
    [field:SerializeField] public int maxSpawnCount { get; private set; } = 10;
    [field:SerializeField] public int spawnCount { get; private set; }
    private bool canSpawn = false;

    private ComponentPool<MobState> monsterPool;
    private void Awake()
    {
        Instance = this;
        monsterPool = new ComponentPool<MobState>(monsterPrefab, transform);
    }

    private void Spawn(MonsterData data, Transform[] roadPointsParent, Vector2 spawnPosition)
    {
        if (!GameFlowManager.Instance.IsRunning) return;

        MobState monster = monsterPool.Get();

        monster.InitializeStat(data, roadPointsParent, spawnPosition);

        GameFlowManager.Instance.RegisterMonster(monster);
    }

    public void StartSpawn()
    {
        canSpawn = true;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawn()
    {
        canSpawn = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn && spawnCount < maxSpawnCount)
        {
            int index = Random.Range(0, monsterData.Length);

            Spawn(monsterData[index], roadPointsParent, transform.position);

            spawnCount++;

            yield return new WaitForSeconds(2f);
        }
    }
}