using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHudManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Quest1Title;
    [SerializeField] private TextMeshProUGUI m_Quest1Bread;
    [SerializeField] private TextMeshProUGUI m_Quest1Ham;
    [SerializeField] private TextMeshProUGUI m_Quest2Title;

    [SerializeField] private TextMeshProUGUI m_Timer;


    public void NotifyQuest1GotHam() {
        m_Quest1Ham.text = $"<s>{m_Quest1Ham.text}<s>";
    }

    public void NotifyQuest1GotBread()
    {
        m_Quest1Bread.text = $"<s>{m_Quest1Bread.text}<s>";
    }

    public void NotifyQuest1Finish()
    {
        m_Quest1Title.text = $"<s>{m_Quest1Title.text}<s>";
    }

    public void NotifyQuest2Finish()
    {
        m_Quest2Title.text = $"<s>{m_Quest2Title.text}<s>";
    }

    public void NotifyTimer(float timer)
    {
        m_Timer.text = timer.ToString("00");
    }
}
