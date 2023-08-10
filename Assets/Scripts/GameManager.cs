using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<string> AllGamesNames = new List<string>();

    [SerializeField] GameData _gameData;

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

        string commaSeparatedList = PlayerPrefs.GetString("AllGamesNames");
        Debug.Log(commaSeparatedList);
        AllGamesNames = commaSeparatedList.Split(",").ToList();
    }

    //Below method takes name of scene to determine which join method to use
    void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Menu")
            _playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        else
        {
            _playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
            SaveGame();
        }
    }

    void SaveGame()
    {
        string text = JsonUtility.ToJson(_gameData);            //Save data into text           
        Debug.Log(text);

        if (AllGamesNames.Contains(_gameData.GameName) == false)
            AllGamesNames.Add(_gameData.GameName);

        string commaSeparatedGameNames = string.Join(",", AllGamesNames);

        PlayerPrefs.SetString("AllGamesNames", commaSeparatedGameNames);

        PlayerPrefs.SetString(_gameData.GameName, text);        //Saves data into Game1 and you will be able to retrieve 
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        string text = PlayerPrefs.GetString("Game1");
        _gameData = JsonUtility.FromJson<GameData>(text);
        SceneManager.LoadScene("Level 1");
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
        if (_gameData.PlayerDatas.Count <= playerIndex)      //if we do not have playerDatas
        {
            var playerData = new PlayerData();               //we create new playerData
            _gameData.PlayerDatas.Add(playerData);           //and we add it to playerData list
        }
        return _gameData.PlayerDatas[playerIndex];           //return data at index playerIndex
    }

    public void NewGame()
    {
        Debug.Log("NewGame Called");
        _gameData = new GameData();
        _gameData.GameName = DateTime.Now.ToString("G");
        SceneManager.LoadScene("Level 1");
    }
}
