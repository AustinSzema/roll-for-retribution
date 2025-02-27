using System;
using UnityEngine;

    public class ToggleShop : MonoBehaviour
    {
        [SerializeField] private GameObject _shopCanvas;

        private GameManager _gameManager;

        private AudioManager _audioManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _audioManager = AudioManager.Instance;
        }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameManager.shopActive = !_gameManager.shopActive;
                if (_gameManager.shopActive)
                {
                    _shopCanvas.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    _gameManager.gameIsPaused = true;
                }
                else
                { 
                    _shopCanvas.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    _gameManager.gameIsPaused = false;
                }
                _audioManager.PlayClickSound();

            }
        }
    }