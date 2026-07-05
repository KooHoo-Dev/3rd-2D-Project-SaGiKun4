using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [SerializeField] private float gameTime = 60f;
    [SerializeField] private TMP_Text timerTxt;

    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text scoreText;
    [field:SerializeField] public int killCount { get; private set; }

    [field:SerializeField] public int defencePoint { get; set; }
    private float timer;
    [field: SerializeField] public bool IsRunning { get; private set; } = false;

    private readonly List<MobState> aliveMonsters = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!IsRunning) return;

        timer -= Time.deltaTime;

        UpdateTimerUI();

        if (timer <= 0f ||
            MonsterSpawner.Instance.spawnCount >= MonsterSpawner.Instance.maxSpawnCount && 
            aliveMonsters.Count == 0)
        {
            timer = 0f;

            StopSpawning();

            FinishGame(aliveMonsters.Count == 0);
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        if (minutes < 0)
            minutes = 0;

        if (seconds < 0)
            seconds = 0;

        timerTxt.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartGame()
    {
        timer = gameTime;
        IsRunning = true;
        killCount = 0;
        defencePoint = 10;
        aliveMonsters.Clear();
        resultPanel.SetActive(false);

        UpdateTimerUI();
        MonsterSpawner.Instance.StartSpawn();

        ContentController.Instance.Refresh();
    }

    public void StopSpawning()
    {
        IsRunning = false;
        MonsterSpawner.Instance.StopSpawn();
    }

    public void RegisterMonster(MobState monster)
    {
        if (!aliveMonsters.Contains(monster))
            aliveMonsters.Add(monster);
    }

    public void UnregisterMonster(MobState monster)
    {
        if (aliveMonsters.Contains(monster))
        {
            aliveMonsters.Remove(monster);
            killCount++;
        }
    }

    private void FinishGame(bool isWin)
    {
        IsRunning = false;

        StartCoroutine(ResultRoutine(isWin));
    }

    private IEnumerator ResultRoutine(bool isWin)
    {
        yield return new WaitForSeconds(2f);

        ShowResult(isWin);
    }

    public void ShowResult(bool isWin)
    {
        resultPanel.SetActive(true);

        if (isWin)
        {
            PlayerDataManager.Instance.DefenceLevel++;
        }

        if (!isWin)
            killCount /= 2;

        titleText.text = isWin ? "승리" : "패배";
        scoreText.text = $"처치 : {killCount}";
    }
}