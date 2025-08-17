using System;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button endButton;
        [SerializeField] private Button clearButton;

        [SerializeField] private Text matchesText;
        [SerializeField] private Text turnesText;

        [SerializeField] private Transform homePanel;
        [SerializeField] private Transform gamePanel;

        public event Action OnStartButton;
        public event Action OnEndButton;

        public event Action OnClearButton;

        public void Setup()
        {
            startButton.onClick.AddListener(() =>
            {
                OnStartButton?.Invoke();
            });

            endButton.onClick.AddListener(() =>
            {
                OnEndButton?.Invoke();
            });

            clearButton.onClick.AddListener(() =>
            {
                OnClearButton?.Invoke();
            });
        }

        public void SetResult(GameResult result)
        {
            matchesText.text = "Matches" + "\n" + result.matches;
            turnesText.text = "Turnes" + "\n" + result.turnes;
        }

        public void HomeMode()
        {
            homePanel.gameObject.SetActive(true);
            gamePanel.gameObject.SetActive(false);
        }

        
        public void GameMode()
        {
            homePanel.gameObject.SetActive(false);
            gamePanel.gameObject.SetActive(true);
        }
    }
}
