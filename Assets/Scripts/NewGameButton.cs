using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    void Start() => GetComponent<Button>().onClick.AddListener(CreatNewGame); //Second method Onclick same as manually adding it in Inspector

    public void CreatNewGame() => GameManager.Instance.NewGame();
}
