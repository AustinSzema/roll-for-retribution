using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetFlightFuelValue : MonoBehaviour
{
    [SerializeField] private Slider _flightFuelSlider;
    
    [SerializeField] private GameObject _flightSliderBackground;

    [SerializeField] private Magnet _magnet;
    
    [SerializeField] private Slider _minimumFuelSlider;

    [SerializeField] private TextMeshProUGUI _fuelTextMeshProUGUI;


    [SerializeField] private Image _fuelFill;

    
    private GameManager _gameManager;
    
    private Color _originalFillColor;

    private void Start()
    {
        _gameManager = GameManager.Instance;;
        _flightFuelSlider.maxValue = _magnet._maxFlightDuration;
        _minimumFuelSlider.maxValue = _magnet._maxFlightDuration;
        _originalFillColor = _fuelFill.color;
    }

    
    
    
    
    private void Update()
    {
        if (!_gameManager.gameIsPaused)
        {
            // Set the slider value
            _flightFuelSlider.value = _gameManager.flightDuration;
            _minimumFuelSlider.value = _magnet._fuelPenaltyThreshold;

            if (_gameManager.outOfFuel == true)
            {
                _fuelFill.color = _originalFillColor / 2;
            }

            if (_flightFuelSlider.value >= _magnet._fuelPenaltyThreshold)
            {
                //Debug.Log("Flight Fuel Slider Value: " + _flightFuelSlider.value);
                _fuelFill.color = _originalFillColor;
            }

            if (_gameManager.outOfFuel == false)
            {
                _fuelTextMeshProUGUI.text = (int)(_flightFuelSlider.value / _flightFuelSlider.maxValue * 100) + "%";
            }
            else
            {
                _fuelTextMeshProUGUI.text = "Charging...";
            }


            
        }

    }

    public void EnableSliderVisual()
    {
        _flightSliderBackground.SetActive(true);
    }
}
