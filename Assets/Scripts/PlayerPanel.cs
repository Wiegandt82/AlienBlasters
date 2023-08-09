using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] Image[] _hearts;
    
    Player _player;

    public void Bind(Player player)
    {
        _player = player;
        _player.CoinsChanged += UpdateCoins;                  //adding UpdateCoins() to event CoinsChanged
        UpdateCoins();
    }


    void UpdateCoins()
    {
        _scoreText.SetText(_player.Coins.ToString());
    }

    void Update()
    {
        if (_player)
        {
            //Iterate thru array of images (hearts)
            for (int i = 0; i < _hearts.Length; i++)
            {
                Image heart = _hearts[i];
                heart.enabled = i < _player.Health; //heart will be enable as long as index is lower then current health value
            }
        }
            
    }
}
