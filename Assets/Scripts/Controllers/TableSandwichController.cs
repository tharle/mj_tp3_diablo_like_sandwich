using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSandwichController : MonoBehaviour
{
    [SerializeField] private GameObject m_Sandwich;
    [SerializeField] private float m_DistanceInteraction = 1f;
    [SerializeField] private float m_LifeSandwich = 100;
    private float m_LifeSandwichCurrent;
    private bool m_IsServedSandwich = false;

    [SerializeField] private GameHudManager m_HudManager;

    private void Start()
    {
        m_HudManager = FindAnyObjectByType<GameHudManager>();
        m_Sandwich.SetActive(false);
        m_LifeSandwichCurrent = m_LifeSandwich;
    }
    
    public float GetDistanceInteraction()
    {
        return m_DistanceInteraction;
    }

    public void ServeSandwich()
    {
        m_Sandwich.SetActive(true);
        m_IsServedSandwich = true;
    }

    public bool IsServedSandwich() 
    {
        return m_IsServedSandwich;
    }

    public void EatSandwich(float damage)
    {
        m_LifeSandwichCurrent -= damage;
        m_HudManager.NotifySandwichHP(m_LifeSandwichCurrent / m_LifeSandwich);
        if (m_LifeSandwichCurrent <= 0) GameOver();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        m_HudManager.ShowGameOverScreen();
    }
}
