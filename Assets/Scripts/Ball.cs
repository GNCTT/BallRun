using DG.Tweening;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : Shape
{
    public bool isFlying
    {
        get; private set;
    }

    private void Broken()
    {
        Reset();
        this.gameObject.SetActive(false);
        StopAllCoroutines();
        Spawn.Instance.ReSpawn(ballData);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            BallData playerBallData = player.GetBallData();
            if (ballData.IsSameColor(playerBallData))
            {
                Debug.Log("Touch Player ");
                player.TouchRightBall(this);
                //Destroy
                Broken();
            } else
            {
                player.TouchWrongBall();
            }

        }

        if (other.gameObject.CompareTag("Plane"))
        {
            isFlying = false;
            rb.isKinematic = true;
            this.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }

    public void PlayAnimation()
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(FlyRoutine());
    }


    private IEnumerator FlyRoutine()
    {
        var startPosY = transform.position.y;
        while (!isFlying)
        {
            var speedFly = 3f;
            float newY = (1 + Mathf.Sin(Time.time * speedFly)) * 0.3f + startPosY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    public void StopAnimation()
    {
        StopAllCoroutines();
        this.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    public override void Reset()
    {
        base.Reset();
        isFlying = true;
        rb.isKinematic = false;
    }
}

[Serializable]
public struct BallData
{
    public BallColor ballColor;
    public Material mat;
    public BallData(BallColor ballColor, Material mat)
    {
        this.ballColor = ballColor;
        this.mat = mat;
    }

    public bool IsSameColor(BallData otherBall)
    {
        return ballColor == otherBall.ballColor;
    }
}

public enum BallColor
{
    RED,
    GREEN,
    BLUE
}


