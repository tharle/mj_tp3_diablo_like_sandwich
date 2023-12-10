using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_RangeAttack = 50f;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] float m_VelocityBullet = 4.0f;
    private float m_ElapseBullet = 0;
    private float m_CooldownBullet = 0.5f;
    public float GetRangeAttack() { return m_RangeAttack; }

    private NavMeshAgent m_Agent;
    private Rigidbody m_Rigidbody;
    private GameObject m_Target;
    private bool m_IsWithHam = false;
    private bool m_IsWithBread = false;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left)) MouseLeftClicDown();

        if (m_Target != null) ActionTarget();
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

            Debug.Log("WE ARE NEXT TO OBJECT");
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
            Debug.Log("WE ARE NEXT TO ENEMY");
            m_Agent.isStopped = true;
            ShotTarget();
        } else
        {
            m_Agent.isStopped = false;
            m_Agent.SetDestination(m_Target.transform.position);
        }
    }

    private void ShotTarget()
    {
        m_ElapseBullet -= Time.deltaTime;
        if (m_ElapseBullet > 0) return;
        m_ElapseBullet = m_CooldownBullet;

        Vector3 directionToEnemy = GetDirectionToTarget();
        GameObject bullet = Instantiate(m_Bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetTargetAndVelocity(m_Target.transform, m_VelocityBullet);
        //bullet.GetComponent<Rigidbody>().AddForce(directionToEnemy * m_ForceBullet, ForceMode.Impulse);
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
