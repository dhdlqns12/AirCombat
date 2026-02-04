using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Video;


public class Intro : MonoBehaviour
{
    public LoopType loopType;//DOTween 에셋에 있는 내장 함수
    public TextMeshProUGUI text;//TMP형태의 텍스트를 받아올 변수

    public bool iskeyDown;
    public bool isPress;
    public GameObject gmStart_Text;
    public GameObject gmOption_Text;
    public GameObject gmQuit_Text;
    public GameObject gmPressAnyButton_Text;
    public VideoPlayer video;

    private void Start()
    {
        text.DOFade(0.0f, 1).SetLoops(-1, loopType);//글자 깜빡임 설정
        //Invoke("SetPresAnyButton", 11f);
        iskeyDown = false;
        isPress = false;
        Invoke("isPresstrue", 11.455f);
    }

    private void Update()
    {
        if(Input.anyKeyDown&& iskeyDown == false)
        {

            iskeyDown = true;
            gmPressAnyButton_Text.SetActive(false);
            WaitVideo();
            gmStart_Text.SetActive(true);
            gmOption_Text.SetActive(true);
            gmQuit_Text.SetActive(true);
        }

        if(isPress == true&&iskeyDown==false)
        {
            WaitVideo();
            isPress = false;
        }
    }

    public void Start_Btn()
    {
        PressButtonVideo();
        gmStart_Text.SetActive(false);
        gmOption_Text.SetActive(false);
        gmQuit_Text.SetActive(false);

        Invoke("LoadMissionSelectScene", 4.1f);
    }

    //public void SetPresAnyButton()
    //{
    //    gmPressAnyButton_Text.SetActive(true);
    //}

    public void WaitVideo()
    {
        video.source = VideoSource.Url;
        video.url = "file://C:/Users/shoseo/Desktop/AirLogic_1023_KJW/Assets/KimJongWeon/BackGroundIMG/MainMenuVideo/버튼대기2.mp4";
        video.isLooping = true;
    }

    public void PressButtonVideo()
    {
        video.source = VideoSource.Url;
        video.url = "file://C:/Users/shoseo/Desktop/AirLogic_1023_KJW/Assets/KimJongWeon/BackGroundIMG/MainMenuVideo/버튼누름2.mp4";
        video.isLooping = false;
    }

    public void LoadMissionSelectScene()
    {
        SceneManager.LoadScene("MissionSelectScene");
    }

    public void isPresstrue()
    {
        isPress = true;
    }
}
