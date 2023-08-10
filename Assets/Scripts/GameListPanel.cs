using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListPanel : MonoBehaviour
{
    [SerializeField] LoadGameButton _buttonPrefab;

    void Start()
    {
        foreach (var gameName in GameManager.Instance.AllGamesNames)
        {
            var button = Instantiate(_buttonPrefab, transform);
            button.SetGameName(gameName);
        }
    }
}
