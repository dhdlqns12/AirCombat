using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap2Camera : MonoBehaviour
{
    public Transform target; //카메라가 따라갈 대상
    public float offsetRatio;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 targetForwardVector = target.forward;
        targetForwardVector.y = 0;
        targetForwardVector.Normalize();

        Vector3 position = new Vector3(target.transform.position.x, 1, target.transform.position.z) + targetForwardVector * offsetRatio * cam.orthographicSize;
        transform.position = position;
        transform.eulerAngles = new Vector3(90, 0, -target.eulerAngles.y);
    }
}
