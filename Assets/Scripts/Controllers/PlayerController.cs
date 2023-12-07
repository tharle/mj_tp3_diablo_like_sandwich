using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    private Rigidbody m_Rigidbody;
    private ObjectController m_ObjetTarget;
    private List<TypeItem> items = new List<TypeItem>();

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ActionObject();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
            {
                CheckObjetMouseClick(hitInfo);
                m_Agent.SetDestination(hitInfo.point);
            }
        }
    }

    private void CheckObjetMouseClick(RaycastHit hit)
    {
        ObjectController objetTarget = hit.collider.gameObject.GetComponent<ObjectController>();

        if (objetTarget == null || objetTarget.IsOpened()) return;

        Debug.Log("WE CLICK OBJECT");

        m_ObjetTarget = objetTarget;
    }

    private void ActionObject()
    {
        if (m_ObjetTarget == null || m_ObjetTarget.IsOpened()) return;

        if (GetDistanceFromObjetSelected() <= m_ObjetTarget.GetDistanceInteraction())
        {

            Debug.Log("WE ARE NEXT TO OBJECT");
            TypeItem item = m_ObjetTarget.Open();

            if (item != TypeItem.NONE) items.Add(item);

            // TODO maj HUD
            m_ObjetTarget = null;
        }
    }

    private float GetDistanceFromObjetSelected()
    {
        return Vector3.Distance(m_ObjetTarget.transform.position, transform.position);
    }
}
