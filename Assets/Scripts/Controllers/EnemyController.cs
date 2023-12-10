using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent m_Agent;
    [SerializeField] private Transform m_Target;

    // Start is called before the first frame update
    void Start()
    {

        m_Agent = GetComponent<NavMeshAgent>();
        MoveToTarget();
    }

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }

    private void MoveToTarget()
    {
        m_Agent.SetDestination(m_Target.position);
    }

    public void Die()
    {
        // TODO Add animation morte
        Destroy(gameObject);
    }
}
