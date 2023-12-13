using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private float m_DistanceInteraction = 2f;
    [SerializeField] private TypeItem item;
    private bool m_IsOpened = false;
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public float GetDistanceInteraction()
    {
        return m_DistanceInteraction;
    }

    public bool IsOpened()
    {
        return m_IsOpened;
    }

    public TypeItem Open()
    {
        if (m_IsOpened) return TypeItem.NONE;

        m_IsOpened = true;

        Debug.Log("DO ACTION: " + name);
        if(m_Animator) m_Animator.SetTrigger(GameParametres.Animation.OBJECT_TRIGGER_OPEN);

        return item;
    }
}
