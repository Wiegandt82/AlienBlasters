using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    void Start() => GetComponent<Button>().onClick.AddListener(LoadGame); //Second method Onclick same as manually adding it in Inspector

    public void LoadGame() => GameManager.Instance.LoadGame();
}