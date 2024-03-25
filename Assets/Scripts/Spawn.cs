using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private BallData redBallData;
    [SerializeField] private BallData greenBallData;
    [SerializeField] private BallData blueBallData;

    private List<BallData> listBallData;


    private int minRed = 2;
    private int minBlue = 2;
    private int minGreen = 2;

    [SerializeField] private Ball ballPrefabs;

    [SerializeField] private int numberBall = 10;
    [SerializeField] private float ReSpawnDelay = 1f;

    private List<Ball> balls;

    public static Spawn Instance;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        balls = new List<Ball>();
        for (int i = 0; i < numberBall; i++)
        {
            var ball = Instantiate(ballPrefabs, transform.position, Quaternion.identity);
            ball.gameObject.SetActive(false);
            balls.Add(ball);
            ball.transform.parent = this.transform;
        }
        listBallData = new List<BallData>();
        listBallData.Add(redBallData);
        listBallData.Add(greenBallData);
        listBallData.Add(blueBallData);
    }

    public void Setup()
    {
        for (int i = 0; i < numberBall; i++)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    public void Reset()
    {
        for (int i = 0; i < numberBall; i++)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnBall();
    }

    private void SpawnBall()
    {
        var pos = CalculatorRandomPos();
        var ballData = GetBallDataSpawn();
        var ball = GetBallDeActive();
        if (ball == null) return;
        ball.Setup(ballData);
        ball.transform.position = pos;
    }

    private void SpawnBall(BallData ballData)
    {
        var pos = CalculatorRandomPos();
        var ball = GetBallDeActive();
        if (ball == null) return;
        ball.Setup(ballData);
        ball.transform.position = pos;
    }

    public void ReSpawn(BallData ballData)
    {
        StartCoroutine(ReSpawnRoutine(ballData));
    }

    private IEnumerator ReSpawnRoutine(BallData ballData)
    {
        yield return new WaitForSeconds(ReSpawnDelay);
        SpawnBall(ballData);
    }

    private Vector3 CalculatorRandomPos()
    {
        var randomPos =  new Vector3(Random.Range(0, 10), 2f, Random.Range(-10, 10));

        if (IsValidPos(randomPos))
        {
            return randomPos;
        } else
        {
            return CalculatorRandomPos();
        }
    }

    private bool IsValidPos(Vector3 pos)
    {
        var validDistance = 3f;
        foreach (var ball in balls)
        {
            var distance = Vector3.Distance(pos, ball.transform.position);
            if (distance < validDistance)
            {
                return false;
            }
        }
        return true;
    }

    private BallData GetBallDataSpawn()
    {
        if (minBlue != 0)
        {
            minBlue--;
            return blueBallData;
        }
        if (minGreen != 0)
        {
            minGreen--;
            return greenBallData;
        }
        if (minRed != 0)
        {
            minRed--;
            return redBallData;
        }
        var randomNum = Random.Range(0, 3);
        switch (randomNum)
        {
            case 0:
                return redBallData;
            case 1:
                return greenBallData;
            case 2:
                return blueBallData;
        }
        return redBallData;
    }

    public void DeactiveAllBall()
    {
        foreach (var ball in balls)
        {
            ball.Reset();
            ball.gameObject.SetActive(false);
        }
    }

    private Ball GetBallDeActive()
    {
        foreach (var ball in balls)
        {
            if (!ball.gameObject.activeSelf)
            {
                return ball;
            }
        }
        return null;
    }

    public List<BallData> GetListBallData()
    {
        return listBallData;
    }
}
