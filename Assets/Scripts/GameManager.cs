using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Player player;

    [SerializeField] private int maxPoint = 10;
    [SerializeField] private int maxLives = 1;
    private int point;
    private int lives;

    private bool isOver;

    private void Awake()
    {
        Instance = this;
    }
    //private void Start()
    //{
    //    Setup();
    //}
    public void StartGame(BallData ballData)
    {
        Setup(ballData);
    }

    private void Setup(BallData ballData)
    {
        point = 0;
        lives = maxLives;
        isOver = false;
        Spawn.Instance.Setup();
        player.Init(ballData);
    }

    private void Reset()
    {
        
    }
    public void AddPoint()
    {
        point++;
        Debug.Log("Add Point: " + point);
        CanvasManager.Instance.UpdatePoint(point);
        CheckWin();
    }

    private void CheckWin()
    {
        if (point >= maxPoint && !isOver)
        {
            OnGameWin();
        }
    }

    public void MakeWrongMove()
    {
        lives--;
        Debug.Log("Lives: " + lives);
        CheckLose();
    }

    private void CheckLose()
    {
        if (lives <= 0 && !isOver)
        {
            OnGameLose();
        }
    }

    private void OnGameWin()
    {
        Debug.Log("WIn");
        OnGameEnd();
        CanvasManager.Instance.ShowPopupWin(point);
    }

    public void OnGameLose()
    {
        Debug.Log("Lose");
        OnGameEnd();
        CanvasManager.Instance.ShowPopupLose(point);
    }

    public void OnGameEnd()
    {
        isOver = true;
        player.Reset();
        Spawn.Instance.DeactiveAllBall();
    }

    public void OnGameReset()
    {
        point = 0;
        lives = maxLives;
        isOver = false;
        Spawn.Instance.Reset();
        player.gameObject.SetActive(true);
    }
}
