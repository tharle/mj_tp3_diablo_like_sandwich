using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private float m_DistanceInteraction = 1f;
    [SerializeField] private TypeItem item;
    private bool m_IsOpened = false;

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
        GetComponent<MeshRenderer>().material.color = Color.gray;
        return item;
    }
}
