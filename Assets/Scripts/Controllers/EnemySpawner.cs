using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> m_Spots;
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Delay;
    private float m_ElapseTime;
    [SerializeField] private bool m_Running = false;


    void Update()
    {
        if (!m_Running) return;

        Spawn();
    }

    private void Spawn() {
        m_ElapseTime += Time.deltaTime;
        if (m_ElapseTime < m_Delay) return;
        m_ElapseTime = 0;

       Instantiate(m_EnemyPrefab, GetRandomSpot(), transform.rotation);
    }

    private Vector3 GetRandomSpot()
    {
        int randomPos = Random.Range(0, m_Spots.Count);
        return m_Spots[randomPos].position;
    }

    public void Run()
    {
        m_Running = true;
    }
}
