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
    private GameHudManager m_GameHudManager;
    private float m_TimerToSuiver = GameParametres.Values.TIME_TO_SUIVIVE_IN_SECONDS;



    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerAnimation = GetComponentInChildren<PlayerAnimation>();
        m_TableSandwich = FindAnyObjectByType<TableSandwichController>();
        m_EnemySpawner = FindAnyObjectByType<EnemySpawner>();
        m_GameHudManager = FindAnyObjectByType<GameHudManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_PlayerAnimation.UpdateVelocity(m_Agent.velocity);

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
                    MouseClickEnemy(gameObjectHit);
                    break;
                case GameParametres.TagName.TABLE:
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

    private void MouseClickObject(GameObject gameObjectHit)
    {
        ObjectController objetTarget = gameObjectHit.GetComponent<ObjectController>();

        if (objetTarget == null || objetTarget.IsOpened()) return;

        
        TargetGameObject(gameObjectHit);
        m_ElapseBullet = 0; // Restart elapsed bullet timer
    }

    private void MouseClickEnemy(GameObject gameObjectHit)
    {
        if(gameObjectHit.GetComponent<EnemyController>().IsDead()) return;
        TargetGameObject(gameObjectHit);
    }

    private void TargetGameObject(GameObject gameObjectHit)
    {
        m_Target = gameObjectHit;
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
                m_GameHudManager.NotifyQuest1GotBread();
                break;
            case TypeItem.HAM:
                m_IsWithHam = true;
                m_GameHudManager.NotifyQuest1GotHam();
                break;
            default: 
                // do nothing
                break;
        }

       if(m_IsWithBread && m_IsWithHam) m_GameHudManager.NotifyQuest1Finish();
    }

    private void InteractTable()
    {
        if (GetDistanceFromObjetSelected() > m_TableSandwich.GetDistanceInteraction()) return;

        m_Agent.isStopped = true;

        if (!IsWithAllIngrients()) {
            m_Target = null;
            return;
        }

        m_PlayerAnimation.Interract();
        m_TableSandwich.ServeSandwich();
        m_GameHudManager.NotifyQuest2Finish();
        m_EnemySpawner.Run();
        m_IsGameRunning = true;
        m_Target = null;
    }

    public bool IsWithAllIngrients() 
    {
        return m_IsWithBread && m_IsWithHam;
    }

    private void AttackEnemy()
    {
        if (GetDistanceFromObjetSelected() <= m_RangeAttack)
        {
            m_PlayerAnimation.Shoot();
            m_Agent.isStopped = true;
            ShootTarget();
        }
    }

    private void ShootTarget()
    {
        m_ElapseBullet -= Time.deltaTime;
        if (m_ElapseBullet > 0) return;
        m_ElapseBullet = m_CooldownBullet;

        transform.rotation = Quaternion.LookRotation(GetDirectionToTarget());
        GameObject bullet = Instantiate(m_Bullet, m_CrossBow.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetTargetAndVelocity(m_Target.transform, m_VelocityBullet);

        m_Target = null;
    }

    private void ActionRunningGame()
    {
        m_TimerToSuiver -= Time.deltaTime;
        if(m_TimerToSuiver <= 0)
        {
            m_TimerToSuiver = 0;
            m_IsGameRunning = false;
            Time.timeScale = 0;
            m_GameHudManager.ShowWinScreen();
        }
        m_GameHudManager.NotifyTimer(m_TimerToSuiver);
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
