using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        m_Animator.SetTrigger(GameParametres.Animation.ENEMY_TRIGGER_ATTACK);
    }

    public void Die()
    {
        m_Animator.SetTrigger(GameParametres.Animation.ENEMY_TRIGGER_DIE);
    }

    public void UpdateVelocity(Vector3 velocity)
    {
        m_Animator.SetFloat(GameParametres.Animation.ENEMY_FLOAT_VELOCITY, velocity.magnitude);
    }
}
