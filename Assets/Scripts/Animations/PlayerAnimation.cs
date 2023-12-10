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

    private void Update()
    {
        // if(m_Animator.GetCurrentAnimatorStateInfo)
    }

    public void Shoot(bool isShooting) {
        //m_Crossbow.SetActive(isShooting);
        m_Animator.SetTrigger(GameParametres.Animation.PLAYER_TRIGGER_SHOOT);
    }

    public void Interract() {
        m_Animator.SetTrigger(GameParametres.Animation.PLAYER_TRIGGER_INTERACT);
    }

    public void UpdatePlayerVelocity(Vector3 velocity)
    {
        m_Animator.SetFloat(GameParametres.Animation.PLAYER_FLOAT_VELOCITY, velocity.magnitude);
    }
}
