using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSandwichController : MonoBehaviour
{
    [SerializeField] private GameObject m_Sandwich;
    [SerializeField] private float m_LifeSandwich = 100;
    [SerializeField] private float m_DistanceInteraction = 1f;

    private void Start()
    {
        m_Sandwich.SetActive(false);
    }
    public float GetDistanceInteraction()
    {
        return m_DistanceInteraction;
    }

    public void ServeSandwich()
    {
        m_Sandwich.SetActive(true);
    }

    public void EatSandwich(float damage)
    {
        m_LifeSandwich -= damage;

        if (m_LifeSandwich <= 0) GameOver();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        // TODO call game over screen
        Debug.Log("GAME OVER");
    }
}
