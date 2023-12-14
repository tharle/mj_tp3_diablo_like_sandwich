using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent m_Agent;
    private EnemyAnimation m_EnemyAnimation;
    private TableSandwichController m_TableSandwich;
    private float m_Hunger;
    private float m_ElapseAtack = 0;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_EnemyAnimation = GetComponentInChildren<EnemyAnimation>();
        m_TableSandwich = FindAnyObjectByType<TableSandwichController>();
        SetRandomHunger();
        MoveToTarget();
    }
    private void Update()
    {
        if (GetDistanceFromSandwich() <= m_TableSandwich.GetDistanceInteraction()) EatSandwich();
        else m_EnemyAnimation.UpdateVelocity(m_Agent.velocity);
    }


    private void MoveToTarget()
    {
        m_Agent.SetDestination(m_TableSandwich.transform.position);
    }

    private void SetRandomHunger()
    {
        m_Hunger = Random.Range(GameParametres.Values.ENEMY_HUNGER_MIN, GameParametres.Values.ENEMY_HUNGER_MAX);
    }


    private void EatSandwich()
    {
        m_ElapseAtack += Time.deltaTime;
        if (m_ElapseAtack <= GameParametres.Values.ENEMY_COOLDOWN_BITE) return ;
        m_ElapseAtack = 0;
        m_Agent.isStopped = true;
        m_EnemyAnimation.Attack();

        m_TableSandwich.EatSandwich(m_Hunger);
    }

    private float GetDistanceFromSandwich()
    {
        return Vector3.Distance(m_TableSandwich.transform.position, transform.position);
    }


    public void Die()
    {
        m_Agent.isStopped = true;
        StartCoroutine(DoDie());
    }


    private IEnumerator DoDie()
    {
        m_EnemyAnimation.Die();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
