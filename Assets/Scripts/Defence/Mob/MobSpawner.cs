using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterSpawner : MonoBehaviour
{
    private enum MonsterType
    { 
        Hyena = 0,
        Snake,
        Cat,
    }

    [SerializeField] private Transform[] roadPointsParent;

    [SerializeField] private MobState monsterPrefab;
    [SerializeField] private MonsterData[] MonsterData;

    private ComponentPool<MobState> monsterPool;
    private void Awake()
    {
        monsterPool = new ComponentPool<MobState>(monsterPrefab, transform);
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Spawn(MonsterData[(int)MonsterType.Hyena], roadPointsParent, transform.position);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Spawn(MonsterData[(int)MonsterType.Snake], roadPointsParent, transform.position);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            Spawn(MonsterData[(int)MonsterType.Cat], roadPointsParent, gameObject.transform.position);
        }
    }

    private void Spawn(MonsterData data, Transform[] roadPointsParent, Vector2 spawnPosition)
    {
        MobState monster = monsterPool.Get();

        monster.InitializeStat(data, roadPointsParent, spawnPosition);
    }
}