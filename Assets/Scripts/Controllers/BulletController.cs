using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private PlayerController playerController;

    private Vector3 m_StartPosition;
    private float m_ElapseTime = 0f;
    private float m_TimerCheckDistance = 0.1f;

    private Transform m_Target;
    private float m_Velocity;

    public void SetTargetAndVelocity(Transform target, float velocity) 
    {  
        m_Target = target;
        m_Velocity = velocity;
    }

    void Start()
    {
        m_StartPosition = transform.position;
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void FixedUpdate()
    {
        CheckDestroyByDistance();
        MoveBullet();
    }

    private void CheckDestroyByDistance()
    {
        m_ElapseTime += Time.deltaTime;
        if (m_ElapseTime >= m_TimerCheckDistance)
        {
            m_ElapseTime -= m_TimerCheckDistance;

            if (GetTravelledDistance() >= playerController.GetRangeAttack()) Destroy(gameObject);
        }
    }

    private void MoveBullet()
    {
        transform.Translate(GetDirectionShot() * m_Velocity * Time.deltaTime);
    }

    private Vector3 GetDirectionShot()
    {
        Vector3 vectorToTarget = transform.position - m_StartPosition;
        
        if (m_Target != null && !m_Target.IsDestroyed()) vectorToTarget = m_Target.position - transform.position;

        return vectorToTarget.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy =  other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Die();
            Destroy(gameObject);
        }
    }

    private float GetTravelledDistance()
    {
        return Vector3.Distance(m_StartPosition, transform.position);    
    }
}
