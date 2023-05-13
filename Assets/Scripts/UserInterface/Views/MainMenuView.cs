using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private MainMenu _mainMenu;
        
        private void Awake()
        {
            _playButton.onClick.AddListener(PlayClicked);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(PlayClicked);
        }

        private void PlayClicked()
        {
            _mainMenu.OpenLevelSelection();
        }
    }
}