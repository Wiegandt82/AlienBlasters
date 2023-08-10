﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    string _gameName;

    void Start() => GetComponent<Button>().onClick.AddListener(LoadGame); //Second method Onclick same as manually adding it in Inspector

    public void LoadGame() => GameManager.Instance.LoadGame(_gameName);

    public void SetGameName(string gameName)
    {
        _gameName = gameName;
        GetComponentInChildren<TMP_Text>().SetText(_gameName);
    }
}