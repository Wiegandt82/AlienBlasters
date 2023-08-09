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
    [SerializeField] Image _flashingImage;

    Player _player;

    public void Bind(Player player)
    {
        _player = player;
        _player.CoinsChanged += UpdateCoins;                  //adding UpdateCoins() to event CoinsChanged
        UpdateCoins();
        _player.HealthChanged += UpdateHealth;
        UpdateHealth();
    }

    void UpdateHealth()
    {
            //Iterate thru array of images (hearts)
            for (int i = 0; i < _hearts.Length; i++)
            {
                Image heart = _hearts[i];
                heart.enabled = i < _player.Health; //heart will be enable as long as index is lower then current health value
            }
        StartCoroutine(Flash());
    }

    //below method will flash red background by 0.5 second when health is updated
    IEnumerator Flash()
    {
        _flashingImage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _flashingImage.enabled = false;
    }

    void UpdateCoins()
    {
        _scoreText.SetText(_player.Coins.ToString());
    }
}
