using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarGame.Common;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtrl : MonoSingleton<PlayerCtrl>
{
    public float walkSpeed = 4.5f;

    private CharacterController characterCtrl;
    private InputCtrl m_Input;

    protected override void OnAwake()
    {
        this.characterCtrl = this.GetComponent<CharacterController>();
        this.m_Input = InputCtrl.Instance;
    }

    private void OnEnable()
    {
        m_Input.MovementInputEvent += this.MovementUpdate;
    }

    private void OnDisable()
    {
        m_Input.MovementInputEvent -= this.MovementUpdate;
    }

    private void MovementUpdate(Vector2 movement)
    {
        Vector3 dir = this.transform.TransformDirection(movement.x, -1, movement.y);
        characterCtrl.Move(dir * walkSpeed * Time.deltaTime);
    }
}
