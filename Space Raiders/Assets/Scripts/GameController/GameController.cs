using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent<int> OnScoreChange;
    public UnityEngine.Events.UnityEvent<int> OnLivesChange;
    public UnityEngine.Events.UnityEvent<GameController> OnInsertCoin;
    public UnityEngine.Events.UnityEvent<GameController> OnGameOver;
    public UnityEngine.Events.UnityEvent<PlayerController> OnPlayerSpawn;

    [field: SerializeField]
    private int _livesRemaining = 3;
    public int LivesRemaining
    {
        get => _livesRemaining;
        private set
        {
            _livesRemaining = value;
            OnLivesChange.Invoke(_livesRemaining);
        }
    }

    [field: SerializeField]
    private EnemyWaveController WaveController { get; set; }

    [field: SerializeField]
    public PlayerController PlayerTemplate { get; private set; }
    [field: SerializeField]
    public Transform PlayerSpawnPoint { get; private set; }
    [field: SerializeField]
    public float SpawnDelay { get; private set; } = 3;
    [field: SerializeField]
    public float SpawnAt { get; private set; } = -1;
    [field: SerializeField]
    private int _score = 0;
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChange.Invoke(_score);
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        if (SpawnAt > 0 && Time.time > SpawnAt)
        {
            SpawnPlayer();
        }
    }

    public void IncrementScore(int amount)
    {
        Score += amount;
    }

    public void InsertCoin()
    {
        DestroyAll();
        WaveController.Reset();
        LivesRemaining = 3;
        Score = 0;
        SpawnPlayer();
        OnInsertCoin.Invoke(this);
    }

    private void DestroyAll()
    {
        foreach (DestructableController toDestroy in  FindObjectsOfType<DestructableController>())
        {
            Destroy(toDestroy.gameObject);
        }
        foreach (OnScreenController toDestroy in  FindObjectsOfType<OnScreenController>())
        {
            Destroy(toDestroy.gameObject);
        }
    }

    private void SpawnPlayer()
    {
        if (LivesRemaining > 0)
        {
            PlayerController pc = PlayerController.Spawn(PlayerTemplate, this);
            pc.transform.position = PlayerSpawnPoint.position;
            SpawnAt = -1;
            OnPlayerSpawn.Invoke(pc);
        }
    }

    public void DestroyPlayer(PlayerController toDestroy)
    {
        Destroy(toDestroy.gameObject);
        SpawnAt = Time.time + SpawnDelay;
        LivesRemaining--;
        if (LivesRemaining <= 0)
        {
            SceneManager.LoadScene(3);
            //OnGameOver.Invoke(this);
        }
    }
}
