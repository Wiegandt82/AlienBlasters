using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] List<PlayerData> _playerDatas = new List<PlayerData>();

    PlayerInputManager _playerInputManager;

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

        _playerInputManager = GetComponent<PlayerInputManager>();

        _playerInputManager.onPlayerJoined += HandlePlayerJoined;

        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    //Below method takes name of scene to determine which join method to use
    void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Menu")
            _playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        else
            _playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
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

    public void NewGame()
    {
        Debug.Log("NewGame Called");
    }
}
