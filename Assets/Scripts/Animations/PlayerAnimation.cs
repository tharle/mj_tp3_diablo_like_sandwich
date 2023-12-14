using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject m_Crossbow;
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Shoot() 
    {
        m_Animator.SetTrigger(GameParametres.Animation.PLAYER_TRIGGER_SHOOT);
    }

    public void Interract() 
    {
        m_Animator.SetTrigger(GameParametres.Animation.PLAYER_TRIGGER_INTERACT);
    }

    public void UpdateVelocity(Vector3 velocity)
    {
        m_Animator.SetFloat(GameParametres.Animation.PLAYER_FLOAT_VELOCITY, velocity.magnitude);
    }
}
