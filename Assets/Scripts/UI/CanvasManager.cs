using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneStartGame;
    [SerializeField] private GameObject popUpWin;
    [SerializeField] private GameObject popUpLose;
    [SerializeField] private GameObject fadeImage;

    [SerializeField] private Text winTextPoint;
    [SerializeField] private Text loseTextPoint;

    [SerializeField] private Text pointText;

    [SerializeField] private Image imagePlayer;

    [SerializeField] private Color[] listColor;

    [SerializeField] private BallData[] ballDatas;
    private int indexColor;

    public static CanvasManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        indexColor = 0;
        imagePlayer.color = listColor[indexColor];
    }

    public void UpdatePoint(int point)
    {
        pointText.text = "Point: " + point;
    }

    public void OnStartGame()
    {
        sceneStartGame.SetActive(false);
        GameManager.Instance.StartGame(ballDatas[indexColor]);
    }

    public void OnClickNextColorPlayer()
    {
        indexColor++;
        if (indexColor > ballDatas.Length - 1)
        {
            indexColor = 0;
        }
        imagePlayer.color = listColor[indexColor];

    }

    public void OnClickPreColorPlayer()
    {
        indexColor--;
        if (indexColor < 0)
        {
            indexColor = ballDatas.Length - 1;
        }
        imagePlayer.color = listColor[indexColor];
    }

    public void OnGameReset()
    {
        HideAllObject();
        GameManager.Instance.OnGameReset();
    }

    public void OnBackHome()
    {
        HideAllObject();
        GameManager.Instance.OnGameEnd();
        sceneStartGame.gameObject.SetActive(true);
    }

    public void ShowPopupWin(int point)
    {
        popUpWin.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        winTextPoint.text = point.ToString();
    }

    public void ShowPopupLose(int point)
    {
        popUpLose.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        loseTextPoint.text = point.ToString();
    }


    private void HideAllObject()
    {
        sceneStartGame.SetActive(false);
        popUpWin.SetActive(false);
        popUpLose.SetActive(false);
        fadeImage.SetActive(false);
    }

}
