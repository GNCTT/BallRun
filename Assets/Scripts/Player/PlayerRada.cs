using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRada : MonoBehaviour
{
    [SerializeField] private Player player;

    private List<Ball> listBallInRange;

    private void Awake()
    {
        listBallInRange = new List<Ball>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            listBallInRange.Add(ball);
            var otherBallColor = ball.GetBallColor();
            var myColor = player.GetBallData().ballColor;
            if (otherBallColor == myColor)
            {
                ball.PlayAnimation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            listBallInRange.Remove(ball);
            var otherBallColor = ball.GetBallColor();
            var myColor = player.GetBallData().ballColor;
            ball.StopAnimation();
        }
    }

    public void OnPlayerChangeColor()
    {
        var playerBallColor = player.GetBallData().ballColor;
        foreach (var ball in listBallInRange)
        {
            if (ball.GetBallColor() == playerBallColor)
            {
                ball.PlayAnimation();
            } else
            {
                ball.StopAnimation();
            }
        }
    }

    public void RemoveBall(Ball ball)
    {
        if (listBallInRange.Contains(ball))
        {
            ball.StopAnimation();
            listBallInRange.Remove(ball);
        }
    }

    public void OnReset()
    {
        listBallInRange.Clear();
    }
}
