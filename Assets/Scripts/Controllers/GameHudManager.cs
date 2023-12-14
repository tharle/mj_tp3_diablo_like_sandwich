using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHudManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Quest1Title;
    [SerializeField] private TextMeshProUGUI m_Quest1Bread;
    [SerializeField] private TextMeshProUGUI m_Quest1Ham;
    [SerializeField] private TextMeshProUGUI m_Quest2Title;

    [SerializeField] private TextMeshProUGUI m_Timer;
    [SerializeField] private GameObject m_SandwichHP;
    private Slider m_SandwichHPSlider;

    private void Start()
    {
        m_SandwichHP.SetActive(false);
    }

    public void NotifyQuest1GotHam() {
        m_Quest1Ham.text = $"<s>{m_Quest1Ham.text}<s>";
        m_SandwichHPSlider = m_SandwichHP.GetComponentInChildren<Slider>();
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
        m_SandwichHP.SetActive(true);
    }

    public void NotifyTimer(float timer)
    {
        m_Timer.text = timer.ToString("00");
    }

    public void NotifySandwichHP(float percHPCurrent)
    {
        m_SandwichHPSlider.value = percHPCurrent;
    }
}
