using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Center UI")]
    [SerializeField]
    TextMeshProUGUI speedText; //speed °ª UI¿¡ Ç¥½Ã
    [SerializeField]
    TextMeshProUGUI altitudeText; //alt(°íµµ °ª UI¿¡ Ç¥½Ã)

    [Header("HUD Controll")]
    [SerializeField]
    GameObject _3rdHud;
    [SerializeField]
    GameObject Subtitle;

    [Header("Upper Left UI")]
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI targetText;
    float remainTime;

    float elapsedTime = 0; //¼º¹Î¼·
    bool isRedTimerActive = false;
    bool isTimeLow = false;

    public void Start()
    {
        _3rdHud.SetActive(false);
        Subtitle.SetActive(true);  //¼º¹Î¼·
        Invoke("_3rdHud_Active", 9f);
        
    }

    public void SetSpeed(int speed)
    {
        string text = string.Format("<mspace=18>{0}</mspace>", speed);//mspce=18{0}Àº ¿©¹é ¼³Á¤ÇÏ°í {0}¿¡ °ªÀ» ³ÖÀ½
        speedText.text = text;       
    }

    public void SetAltitude(int altitude)
    {
        string text = string.Format("<mspace=18>{0}</mspace>", altitude);//mspce=18{0}Àº ¿©¹é ¼³Á¤ÇÏ°í {0}¿¡ °ªÀ» ³ÖÀ½
        altitudeText.text = text;
    }

    //¼º¹Î¼·
    public void Subtitle_Active()
    {
        Subtitle.SetActive(true);
    }

    public void _3rdHud_Active()
    {
        _3rdHud.SetActive(true);
    }

    public void SetTargetText(ObjectInfo objectInfo)
    {
        if (objectInfo == null || objectInfo.ObjectName == "")
        {
            targetText.text = "";
        }
        else
        {
            string objectName = objectInfo.ObjectName + " " + objectInfo.ObjectNickname;
            string text = string.Format("TARGET {0} +<mspace=18>{1}</mspace>", objectName, objectInfo.Score);
            targetText.text = text;
        }
    }
    //¼º¹Î¼·
    public void SetRemainTime(int remainTime)
    {
        this.remainTime = (float)remainTime;
    }

    void SetTime()
    {
        //¼º¹Î¼·
        remainTime -= Time.deltaTime;
        elapsedTime += Time.deltaTime;

        if (isRedTimerActive == false && remainTime < 10 && isTimeLow == false)
        {
            InvokeRepeating("PlayTimeLowAudioClip", 0, 1);
            isTimeLow = true;
        }

        if (remainTime <= 0)
        {
            remainTime = 0;
            return;
        }

        remainTime -= Time.deltaTime;
        int seconds = (int)remainTime;

        int min = seconds / 60;
        int sec = seconds % 60;
        int millisec = (int)((remainTime - seconds) * 100);
        string text = string.Format("TIME <mspace=18>{0:00}</mspace>:<mspace=18>{1:00}</mspace>:<mspace=18>{2:00}</mspace>", min, sec, millisec);
        timeText.text = text;
    }
}
