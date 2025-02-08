using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject m_PauseMenuScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_PauseMenuScreen.SetActive(false);
    }

    
}
