using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static GameParametres;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_RangeAttack = 50f;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private float m_VelocityBullet = 4.0f;
    [SerializeField] private Transform m_CrossBow;
    private float m_ElapseBullet = 0;
    private float m_CooldownBullet = 0.1f;
    public float GetRangeAttack() { return m_RangeAttack; }

    private GameObject m_Target;
    private bool m_IsWithHam = false;
    private bool m_IsWithBread = false;
    private bool m_IsGameRunning = false;

    private NavMeshAgent m_Agent;
    private PlayerAnimation m_PlayerAnimation;
    private TableSandwichController m_TableSandwich;
    private EnemySpawner m_EnemySpawner;
    private float m_TimerToSuiver = GameParametres.Values.TIME_TO_SUIVIVE_IN_SECONDS;



    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerAnimation = GetComponentInChildren<PlayerAnimation>();
        m_TableSandwich = FindAnyObjectByType<TableSandwichController>();
        m_EnemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        m_PlayerAnimation.UpdatePlayerVelocity(m_Agent.velocity);

        if (Input.GetMouseButtonDown((int)MouseButton.Left)) MouseLeftClicDown();

        if (m_Target != null) ActionTarget();

        if (m_IsGameRunning) ActionRunningGame();
    }

    private void MouseLeftClicDown()
    {
        Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
        {
            GameObject gameObjectHit = hitInfo.collider.gameObject;
            switch (hitInfo.collider.tag)
            {
                case GameParametres.TagName.OBJECT:
                    MouseClickObject(gameObjectHit);
                    break;
                case GameParametres.TagName.ENEMY:
                case GameParametres.TagName.TABLE:
                    TargetGameObject(gameObjectHit);
                    TargetGameObject(gameObjectHit);
                    break;
                default:
                    MouseClickNothing();
                    break;
            }
            m_Agent.isStopped = false;
            m_Agent.SetDestination(hitInfo.point);
        }
    }

    private void MouseClickObject(GameObject gameObject)
    {
        ObjectController objetTarget = gameObject.GetComponent<ObjectController>();

        if (objetTarget == null || objetTarget.IsOpened()) return;

        
        TargetGameObject(gameObject);
        m_ElapseBullet = 0; // Restart elapsed bullet timer
    }

    private void TargetGameObject(GameObject gameObject)
    {
        m_Target = gameObject;
    }

    private void MouseClickNothing()
    {
        m_Target = null;
    }

    private void ActionTarget()
    {
        if (m_Target.CompareTag(GameParametres.TagName.OBJECT)) ActionObject();
        else if (m_Target.CompareTag(GameParametres.TagName.ENEMY)) AttackEnemy();
        else if (m_Target.CompareTag(GameParametres.TagName.TABLE)) InteractTable();
    }

    private void ActionObject()
    {
        ObjectController objectTarget = m_Target.GetComponent<ObjectController>();

        

        if (GetDistanceFromObjetSelected() <= objectTarget.GetDistanceInteraction())
        {
            m_Agent.isStopped = true;

            if (objectTarget.IsOpened())
            {
                m_Target = null;
                return;
            }

            m_PlayerAnimation.Interract();
            InteractObject(objectTarget.Open());

            // TODO maj HUD
            m_Target = null;
        }
    }

    private void InteractObject(TypeItem type)
    {
        Debug.Log(" OBJECT INTERRACT!! " + type);
        switch (type)
        {
            case TypeItem.BREAD:
                m_IsWithBread = true;
                break;
            case TypeItem.HAM:
                m_IsWithHam = true;
                break;
            default: 
                // do nothing
                break;
        }
    }

    private void InteractTable()
    {
        if (GetDistanceFromObjetSelected() > m_TableSandwich.GetDistanceInteraction()) return;

        m_Agent.isStopped = true;

        if (!m_IsWithBread || !m_IsWithHam) {
            m_Target = null;
            return;
        }

        m_PlayerAnimation.Interract();
        m_TableSandwich.ServeSandwich();
        m_EnemySpawner.Run();
        m_IsGameRunning = true;
        m_Target = null;
    }

    private void AttackEnemy()
    {
        if (GetDistanceFromObjetSelected() <= m_RangeAttack)
        {
            m_PlayerAnimation.Shoot(true);
            m_Agent.isStopped = true;
            ShotTarget();
        }
    }

    private void ShotTarget()
    {
        m_ElapseBullet -= Time.deltaTime;
        if (m_ElapseBullet > 0) return;
        m_ElapseBullet = m_CooldownBullet;

        transform.rotation = Quaternion.LookRotation(GetDirectionToTarget());
        GameObject bullet = Instantiate(m_Bullet, m_CrossBow.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetTargetAndVelocity(m_Target.transform, m_VelocityBullet);

        // TODO rotate player to face enemy

        m_Target = null;
    }

    private void ActionRunningGame()
    {
        m_TimerToSuiver -= Time.deltaTime;
        if(m_TimerToSuiver <= 0)
        {
            m_TimerToSuiver = 0;
            m_IsGameRunning = false;
            Time.timeScale = 0; // pause game
            // TODO WIN GAME
            Debug.Log("WIN GAME");
        }
    }


    private Vector3 GetDirectionToTarget()
    {
        return (m_Target.transform.position - transform.position).normalized;
    }

    private float GetDistanceFromObjetSelected()
    {
        return Vector3.Distance(m_Target.transform.position, transform.position);
    }
}
