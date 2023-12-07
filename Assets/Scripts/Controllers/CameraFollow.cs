using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_ToFollow;

    [SerializeField] private Transform m_LimitTop;
    [SerializeField] private Transform m_LimitDown;
    [SerializeField] private Transform m_LimitLeft;
    [SerializeField] private Transform m_LimitRight;

    Vector3 m_Offset = Vector3.zero;

    void Start()
    {
        m_Offset = transform.position - m_ToFollow.position;
    }

    void Update()
    {
        transform.position = m_ToFollow.position + m_Offset;
        FixCameraBounds();
    }

    private void FixCameraBounds()
    {
        Vector3 position = transform.position;
        if (m_LimitTop.position.z + m_Offset.z > position.z) position.z = m_LimitTop.position.z + m_Offset.z;
        else if(m_LimitDown.position.z + m_Offset.z < position.z) position.z = m_LimitDown.position.z + m_Offset.z;

        if (m_LimitLeft.position.x + m_Offset.x > position.x) position.x = m_LimitLeft.position.x + m_Offset.x;
        else if (m_LimitRight.position.x + m_Offset.x < position.x) position.x = m_LimitRight.position.x + m_Offset.x;

        transform.position = position;
    }
}
