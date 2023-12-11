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

    private NavMeshAgent m_Agent;
    private PlayerAnimation m_PlayerAnimation;
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        m_PlayerAnimation.UpdatePlayerVelocity(m_Agent.velocity);

        if (Input.GetMouseButtonDown((int)MouseButton.Left)) MouseLeftClicDown();

        if (m_Target != null) ActionTarget();
        //else m_Animation.Shoot(false);
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
                    Debug.Log("WE CLICK OBJECT");
                    MouseClickObject(gameObjectHit);
                    break;
                case GameParametres.TagName.ENEMY:
                    Debug.Log("WE CLICK ENEMY");
                    TargetGameObject(gameObjectHit);
                    break;
                default:
                    Debug.Log("WE CLICK NOTHING");
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
    }

    private void ActionObject()
    {
        ObjectController objectTarget = m_Target.GetComponent<ObjectController>();

        if (objectTarget.IsOpened()) {
            m_Target = null;
            return;
        }

        if (GetDistanceFromObjetSelected() <= objectTarget.GetDistanceInteraction())
        {
            m_PlayerAnimation.Interract();
            GotItem(objectTarget.Open());

            // TODO maj HUD
            m_Target = null;
        }
    }

    private void GotItem(TypeItem typeItem)
    {
        switch (typeItem)
        {
            case TypeItem.BREAD:
                m_IsWithBread = true;
                break;
            case TypeItem.HAM:
                m_IsWithBread = true;
                break;
            default: 
                // do nothing
                break;
        }
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

    private Vector3 GetDirectionToTarget()
    {
        return (m_Target.transform.position - transform.position).normalized;
    }

    private float GetDistanceFromObjetSelected()
    {
        return Vector3.Distance(m_Target.transform.position, transform.position);
    }
}
