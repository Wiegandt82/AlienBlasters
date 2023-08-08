using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    

    void Awake()
    {
        //will check if object exists and if it does will destroy any new object
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;               // set instance to this gameObject
        DontDestroyOnLoad(gameObject); //will not destroy object when loading new scene 

        GetComponent<PlayerInputManager>().onPlayerJoined += HandlePlayerJoined;
    }

    void HandlePlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("HandlePlayerJoined " + playerInput);
        PlayerData playerData = GetPlayerData(playerInput.playerIndex);
        Player player = playerInput.GetComponent<Player>();
        player.Bind(playerData);
    }

    //Below method will give player data for a specific player based on their index
    PlayerData GetPlayerData(int playerIndex)
    {
        if (_playerDatas.Count <= playerIndex)      //if we do not have playerDatas
        {
            var playerData = new PlayerData();      //we create new playerData
            _playerDatas.Add(playerData);           //and we add it to playerData list
        }
        return _playerDatas[playerIndex];           //return data at index playerIndex
    }

    List<PlayerData> _playerDatas = new List<PlayerData>();
}
