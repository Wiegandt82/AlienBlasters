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
    }

    void Update()
    {
        if (_player)
        {
            _scoreText.SetText(_player.Coins.ToString());

            //Iterate thru array of images (hearts)
            for (int i = 0; i < _hearts.Length; i++)
            {
                Image heart = _hearts[i];
                heart.enabled = i < _player.Health; //heart will be enable as long as index is lower then current health value
            }
        }
            
    }
}
