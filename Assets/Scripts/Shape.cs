using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] protected BallData ballData;

    protected Rigidbody rb;
    protected Material myMat;
    protected Renderer myRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myMat = GetComponent<MeshRenderer>().material;
        myRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        myRenderer.enabled = true;
    }

    public void Setup(BallData ballData)
    {
        this.ballData = ballData;
        myMat = ballData.mat;
        myRenderer.sharedMaterial = ballData.mat;
        this.gameObject.SetActive(true);
    }

    public BallColor GetBallColor()
    {
        return ballData.ballColor;
    }

    public BallData GetBallData()
    {
        return ballData;
    }

    public virtual void Reset()
    {
    }
}

