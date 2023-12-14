using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private GameObject m_CursorInteract;
    [SerializeField] private GameObject m_CursorAttack;
    [SerializeField] private GameObject m_CursorNormal;
    private PlayerController m_Player;

    private void Start()
    {
        m_Player = FindAnyObjectByType<PlayerController>();
        Cursor.visible = false;
        ShowCursorNormal();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
        CheckMouseOver();
    }

    private void CheckMouseOver()
    {
        Ray rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayMouse, out RaycastHit infoHit))
        {
            GameObject gameObjectHit = infoHit.collider.gameObject;
            switch (gameObjectHit.tag)
            {
                case GameParametres.TagName.OBJECT:
                    MouseOverObject(gameObjectHit.GetComponent<ObjectController>());
                    break;
                case GameParametres.TagName.TABLE:
                    MouseOverTable(gameObjectHit.GetComponent<TableSandwichController>());
                    break;
                case GameParametres.TagName.ENEMY:
                    MouseOverEnemy(gameObjectHit.GetComponent<EnemyController>());
                    break;
                default:
                    ShowCursorNormal();
                    break;
            }
        }
    }

    private void MouseOverObject(ObjectController objectController)
    {
        if (objectController.IsOpened()) return;

        ShowCursorIntract();
    }

    private void MouseOverTable(TableSandwichController tableSandwich)
    {
        if (!m_Player.IsWithAllIngrients() || tableSandwich.IsServedSandwich()) return;

        ShowCursorIntract();
    }

    private void MouseOverEnemy(EnemyController enemy)
    {
        if (enemy.IsDead()) return;
        ShowCursorAttack();
    }

    private void ShowCursorNormal()
    {
        m_CursorInteract.SetActive(false);
        m_CursorAttack.SetActive(false);
        m_CursorNormal.SetActive(true);
        
    }

    private void ShowCursorAttack()
    {
        m_CursorInteract.SetActive(false);
        m_CursorAttack.SetActive(true);
        m_CursorNormal.SetActive(false);
    }

    private void ShowCursorIntract()
    {
        m_CursorInteract.SetActive(true);
        m_CursorAttack.SetActive(false);
        m_CursorNormal.SetActive(false);
    }
}
