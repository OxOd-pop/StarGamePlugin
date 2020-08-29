using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPGCameraCtrl : MonoBehaviour
{
    [Tooltip("摄像机跟随的目标")] public Transform target;
    [Tooltip("高度")]public float targetHeight = 1.8f;
    [Tooltip("偏移")]public float targetSide = -0.1f;
    [Tooltip("距离")] public float distance = 4;
    [Tooltip("最大距离")] public float maxDistance = 8;
    [Tooltip("最小距离")] public float minDistance = 2.2f;
    [Tooltip("水平方向速度")] public float hSpeed = 250;
    [Tooltip("垂直方向速度")] public float vSpeed = 125;
    [Tooltip("水平方向最大角度")] public float vMaxLimit = 72;
    [Tooltip("水平方向最小角度")] public float vMinLimit = -10;
    [Tooltip("缩放倍数")] public float zoomRate = 80;

    public float h = 20;
    public float v = 0;

    private InputCtrl m_Input;

    private void Awake()
    {
        this.m_Input = InputCtrl.Instance;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        this.m_Input.CameraInputEvent += this.CameraUpdate;
    }

    private void OnDisable()
    {
        this.m_Input.CameraInputEvent -= this.CameraUpdate;
    }

    private void CameraUpdate(Vector3 camera ,Vector2 movement)
    {
        this.h += camera.x * hSpeed * Time.deltaTime;
        this.v -= camera.y * vSpeed * Time.deltaTime;

        distance -= (camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        v = this.ClampAngle(v, vMinLimit, vMaxLimit);

        Quaternion rotation = Quaternion.Euler(v, h, 0);
        this.transform.rotation = rotation;

        if (movement.x != 0 || movement.y != 0)
        {
            target.transform.rotation = Quaternion.Euler(0, h, 0);
        }

        this.transform.position = target.position - (rotation * new Vector3(targetSide, 0, 1) * distance - new Vector3(0, targetHeight, 0));

    }


    private float ClampAngle(float angle,float min,float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
