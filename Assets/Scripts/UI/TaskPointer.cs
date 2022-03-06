using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPointer : MonoBehaviour
{

    private Vector3 targetPosition;
    private Vector3 targetPositionScreenPoint;
    [SerializeField] float border = 5f;
    [SerializeField] Sprite staticSprite;
    [SerializeField] Sprite arrowSprite;
    private RectTransform pointerRectTransform;
    private Image image;
    Camera mainCamera;

    bool positionSet = false;

    private void Awake()
    {
        image = GetComponent<Image>();
        pointerRectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    public void SetTarget(Vector3 pos)
    {
        targetPosition = pos;
        positionSet = true;
    }

    private void Update()
    {
        if (!positionSet) { return; }

        SetPointerAngle();

        targetPositionScreenPoint = mainCamera.WorldToScreenPoint(targetPosition);

        if (IsOffScreen())
        {
            KeepPositionWithinScreenBounds();
            image.sprite = arrowSprite;
        }
        else
        {
            SetPointerToTargetPosition();
            image.sprite = staticSprite;
        }
    }

    private void SetPointerToTargetPosition()
    {
        Vector3 pointerWorldPosition = mainCamera.ScreenToWorldPoint(targetPositionScreenPoint);
        pointerRectTransform.position = pointerWorldPosition;
    }

    private void KeepPositionWithinScreenBounds()
    {
        Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
        if (cappedTargetScreenPosition.x <= border) { cappedTargetScreenPosition.x = border; }
        if (cappedTargetScreenPosition.x >= Screen.width - border) { cappedTargetScreenPosition.x = Screen.width - border; }
        if (cappedTargetScreenPosition.y <= border) { cappedTargetScreenPosition.y = border; }
        if (cappedTargetScreenPosition.y >= Screen.height - border) { cappedTargetScreenPosition.y = Screen.height - border; }

        Vector3 pointerWorldPosition = mainCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
        pointerRectTransform.position = pointerWorldPosition;
    }

    private bool IsOffScreen() => targetPositionScreenPoint.x <= 0 || 
                                  targetPositionScreenPoint.x >= Screen.width || 
                                  targetPositionScreenPoint.y <= 0 || 
                                  targetPositionScreenPoint.y >= Screen.height;

    private void SetPointerAngle()
    {
        if (!IsOffScreen()) 
        { 
            pointerRectTransform.localEulerAngles = Vector3.zero;
            return;
        }
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = mainCamera.transform.position;
        fromPosition.z = 0;
        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
