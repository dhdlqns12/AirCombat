using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [Header("CrossHair")]
    [SerializeField]
    protected Vector2 offset; //크로스 헤어의 위치를 조정하기 위한 offset 변수(x,y)
    [SerializeField]
    protected float lerpAmount;
    protected float zDistance; //크로스 헤어의 z값(화면에 표시될 위치값)

    protected virtual void Start()
    {
        zDistance = transform.localPosition.z; //zDistance변수를 초기값으로 설정
    }

    protected virtual void Update() //크로스 헤어의 위치를 부드럽게 변경하기 위한 함수
    {
        Vector2 aircraftRotation = GameManager.PlayerCtrl.RotateValue; //현재 비행기의 회전값을 aircraftRotation변수에 저장
        Vector3 convertedPosition = new Vector3(-aircraftRotation.y * offset.x, aircraftRotation.x * offset.y, zDistance);//aircraftRotation값을 기반으로 크로스헤어의 위치 계산
        transform.localPosition = Vector3.Lerp(transform.localPosition, convertedPosition, lerpAmount);//크로스 헤어의 위치를 부드럽게 localPosition에서 convertPosition으로 이동
    }
}
