using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHudManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Quest1;
    [SerializeField] private GameObject m_Quest1Bread;
    [SerializeField] private GameObject m_Quest1Ham;
    [SerializeField] private GameObject m_Quest2;
    [SerializeField] private GameObject m_Quest3;


    public void NotifyGotHam() {
        // TODO change color font Ham to gray
    }

    public void NotifyGotBread()
    {
        // TODO change color font Ham to gray
    }

    public void NotifyFinishQuest1()
    {
        // TODO change color font Q1 to gray
        m_Quest2.SetActive(true);
    }

    public void NotifyFinishQuest2()
    {
        // TODO change color font Q2 to gray
        m_Quest3.SetActive(true);
    }
}
