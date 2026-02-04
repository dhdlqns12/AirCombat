using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class LoadingSceneManager : MonoBehaviour
{
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    public string sceneName;
    public LoopType loopType;

    void Start()
    {
        loadingText.DOFade(0.0f, 1).SetLoops(-1, loopType);//글자 깜빡임 설정
        StartCoroutine(LoadAsyncSceneCoroutine());
    }

    IEnumerator LoadAsyncSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            loadingBar.value = operation.progress;
            loadingText.text = "Loading" + (operation.progress * 100) + "%";

            if (operation.progress>=0.9f)//씬 로딩이 완료 됬을때 아무버튼이나 눌러 로딩된 씬으로 넘어가게 하는 조건문
            {
                loadingText.text = "Press Any Button To Start";
                loadingBar.value = 1;

                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}

