using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetLock : FollowTansform
{
    RawImage rawImage;

    public GameObject lockOnImage;

    [SerializeField]
    Texture mslLockTexture;
    [SerializeField]
    Texture spwLockTexture;
    [SerializeField]
    RectTransform crosshair;

    // From Missile Data
    public float targetSearchSpeed;
    public float boresightAngle;
    public float lockDistance;

    // Status
    public float lockProgress;
    public bool isLocked;

    public bool IsLocked
    {
        get { return isLocked; }
    }

  
    Vector2 targetScreenPosition;

    void ResetLock()
    {
        isLocked = false;
        lockProgress = 0;
        rawImage.color = GameManager.instance.normalColor;
        rawImage.enabled = false;

        GameManager.TargetController.SetTargetUILock(false);
    }

    public void SetTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        ResetLock();
    }

    public void SwitchWeapon(Missile missile)
    {

        boresightAngle = missile.boresightAngle;
        targetSearchSpeed = missile.targetSearchSpd;
        lockDistance = missile.lockOnDistance;

        ResetLock();

        rawImage.texture = (missile.isSpecialWeapon == true) ? spwLockTexture : mslLockTexture;
    }


    void CheckTargetLock()
    {

        if (targetTransform == null)
        {
            ResetLock();
            return;
        }

        float distance = Vector3.Distance(targetTransform.position, GameManager.PlayerCtrl.transform.position);


        if (distance > lockDistance)
        {
            ResetLock();
            return;
        }

        cam = GameManager.instance.cameraCtrl.GetActiveCamera();
        Vector3 screenPosition = cam.WorldToScreenPoint(targetTransform.position);

        if (screenPosition.z > 0)
        {
            // UI Position
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, targetTransform.position);
            targetScreenPosition = screenPoint - canvasRect.sizeDelta * 0.5f;
        }

        float targetAngle = GameManager.GetAngle(targetTransform);

        if (targetAngle > boresightAngle)
        {
            ResetLock();
        }

        else
        {
            rawImage.enabled = true;

            if (isLocked == false)
            {
                lockProgress += targetSearchSpeed * Time.deltaTime;
            }

            if (lockProgress >= targetAngle)
            {
                isLocked = true;
                lockProgress = boresightAngle;
                rawImage.color = GameManager.instance.warningColor;

                GameManager.TargetController.SetTargetUILock(true);
            }

            else
            {
                isLocked = false;
                rawImage.color = GameManager.instance.normalColor;
            }

            rectTransform.anchoredPosition = Vector2.Lerp(crosshair.anchoredPosition, targetScreenPosition, lockProgress / targetAngle);
        }
    }

    public void InitImage()
    {
        lockOnImage.SetActive(true);
        lockOnImage.transform.SetParent(transform, false);       
    }

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    protected override void Start()
    {
        base.Start();
        ResetLock();
        GameObject obj = Instantiate(lockOnImage);
    }

    protected override void Update()
    {
        CheckTargetLock();
        if (isLocked == true)
        {
            InitImage();
        }
        if(isLocked==false)
        {
            lockOnImage.SetActive(false);
        }
    }
}
