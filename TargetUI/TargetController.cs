using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject targetUIObject;
    List<TargetUI> targetUIs;
    TargetUI currentTargettedUI;

    public TargetObject lockedTarget;

    [SerializeField]
    TargetLock targetLock;

    void Start()
    {
        if (lockedTarget != null)
        {
            GameManager.UIController.SetTargetText(lockedTarget.Info);
        }
    }

    public bool IsLocked
    {
        get { return targetLock.IsLocked; }
    }


    public void CreateTargetUI(TargetObject targetObject)
    {
        GameObject obj = Instantiate(targetUIObject);
        TargetUI targetUI = obj.GetComponent<TargetUI>();
        targetUI.Target = targetObject;

        obj.transform.SetParent(transform, false);
    }

    public void RemoveTargetUI(TargetObject targetObject)
    {
        TargetUI targetUI = FindTargetUI(targetObject);
        if (targetUI.Target != null)
        {
            Destroy(targetUIObject);
            targetUIs.Remove(targetUI);
            Destroy(targetUI.gameObject);
        }
    }

    public void ChangeTarget(TargetObject lockedTarget)
    {
        GameManager.UIController.SetTargetText(lockedTarget.Info);

        TargetUI targetUI = FindTargetUI(lockedTarget);
        if (targetUI.Target != null)
        {
            currentTargettedUI.SetTargetted(false);
            currentTargettedUI = targetUI;
            targetUI.SetTargetted(true);
            targetLock.SetTarget(null);

            targetLock.SetTarget(lockedTarget.transform);
        }
    }

    public TargetUI FindTargetUI(TargetObject targetObject)
    {
        foreach (TargetUI targetUI in targetUIs)
        {
            if (targetUI.Target == lockedTarget)
            {
                return targetUI;
            }
        }

        return null;
    }

    public void SetTargetUILock(bool isLocked)
    {
        if (currentTargettedUI)
        {
            currentTargettedUI.SetLock(isLocked);
        }
    }
}

