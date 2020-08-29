using StarGame.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputCtrl : MonoSingleton<InputCtrl>
{

    public UnityAction<Vector2> MovementInputEvent;
    public UnityAction<Vector3,Vector2> CameraInputEvent;

    private Vector2 m_Movement = Vector2.zero;
    private Vector3 m_Camera = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));

        this.MovementInputEvent?.Invoke(m_Movement);
        
    }

    private void LateUpdate()
    {
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));
        this.CameraInputEvent?.Invoke(m_Camera,m_Movement);
    }
}
