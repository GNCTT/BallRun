using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerRada playerRada;

    private Shape shape;
    private BallData ballData;
    private int indexBallData;
    private List<BallData> listBallData;

    private void Awake()
    {
        shape = GetComponent<Shape>();
    }

    public void Init(BallData ballData)
    {
        listBallData = Spawn.Instance.GetListBallData();
        this.ballData = ballData;
        shape.Setup(ballData);
        indexBallData = FindIndexBallData(ballData);
    }

    public void ChangePlayerColor(BallData ballData)
    {
        this.ballData = ballData;
        shape.Setup(ballData);
        playerRada.OnPlayerChangeColor();
    }

    public BallData GetBallData()
    {
        return ballData;
    }

    public void TouchRightBall(Ball ball)
    {
        GameManager.Instance.AddPoint();
        var currentColor = ballData.ballColor;
        ChangePlayerColor(FindNextBallData());
        playerRada.RemoveBall(ball);
    }

    private int FindIndexBallData(BallData ballData)
    {
        var currentColor = ballData.ballColor;
        for(int i = 0; i < listBallData.Count; i++)
        {
            var ballDat = listBallData[i];
            if (ballDat.ballColor == currentColor)
            {
                return i;
            }
        }
        return 0;
    }

    private BallData FindNextBallData()
    {
        int randomNumber = Random.Range(1, listBallData.Count - 1);
        int index = (indexBallData + randomNumber) % (listBallData.Count);
        indexBallData = index;
        return listBallData[index];
    }

    public void TouchWrongBall()
    {
        GameManager.Instance.MakeWrongMove();
    }

    public void Reset()
    {
        playerRada.OnReset();
        this.gameObject.SetActive(false);
    }
}
