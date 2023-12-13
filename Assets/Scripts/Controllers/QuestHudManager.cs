using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestHudManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Quest1Title;
    [SerializeField] private TextMeshProUGUI m_Quest1Bread;
    [SerializeField] private TextMeshProUGUI m_Quest1Ham;
    [SerializeField] private TextMeshProUGUI m_Quest2Title;


    public void NotifyGotHam() {
        m_Quest1Ham.text = $"<s>{m_Quest1Ham.text}<s>";
    }

    public void NotifyGotBread()
    {
        m_Quest1Bread.text = $"<s>{m_Quest1Bread.text}<s>";
    }

    public void NotifyFinishQuest1()
    {
        m_Quest1Title.text = $"<s>{m_Quest1Title.text}<s>";
    }

    public void NotifyFinishQuest2()
    {
        m_Quest2Title.text = $"<s>{m_Quest2Title.text}<s>";
    }
}
