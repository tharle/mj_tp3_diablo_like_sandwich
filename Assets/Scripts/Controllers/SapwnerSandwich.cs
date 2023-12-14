using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapwnerSandwich : MonoBehaviour
{
    private float m_ElapseTime = 0f;
    private float m_Cooldown = 0.2f;
    [SerializeField] GameObject m_Sandwich;

    private void Update()
    {
        m_ElapseTime += Time.deltaTime;
        if (m_ElapseTime < m_Cooldown) return;
        m_ElapseTime = 0;

        Instantiate(m_Sandwich, GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = transform.position;
        position.x = Random.Range(-12f, 12f);
        return position;
    }

}
