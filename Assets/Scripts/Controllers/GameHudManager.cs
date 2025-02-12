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

    [SerializeField] private GameObject m_GameOverScreen;
    [SerializeField] private GameObject m_WinScreen;
    [SerializeField] private GameObject m_PauseMenuScreen;

    [SerializeField] private AudioSource m_AudioGame;
    [SerializeField] private AudioSource m_AudioGameOver;
    [SerializeField] private AudioSource m_AudioWin;

    private Slider m_SandwichHPSlider;

    private void Start()
    {
        m_SandwichHP.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(m_PauseMenuScreen.activeSelf) OnResumeGame();
            else ShowPauseScreen();
        }
    }

    public void NotifyQuest1GotHam() 
    {
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

    public void ShowGameOverScreen()
    {
        m_AudioGame.Stop();
        m_AudioGameOver.Play();
        m_GameOverScreen.SetActive(true);
    }

    public void ShowWinScreen()
    {
        m_AudioGame.Stop();
        m_AudioWin.Play();
        m_WinScreen.SetActive(true);
    }

    private void ShowPauseScreen()
    {
        Time.timeScale = 0; // Stop Game
        m_PauseMenuScreen.SetActive(true);
    }

    public void OnResumeGame()
    {
        m_PauseMenuScreen.SetActive(false);
         Time.timeScale = 1; // Resume Game
    }
}
