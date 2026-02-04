using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUICameraController : MonoBehaviour
{
    public GameObject targetUICamera;
    private void Start()
    {
        Invoke("Active", 9f);
    }

    public void Active()
    {
        targetUICamera.SetActive(true);
    }
}
