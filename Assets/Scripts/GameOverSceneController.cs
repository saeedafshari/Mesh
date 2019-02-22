using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour
{
    Button btnStart;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.Save();
        btnStart = GameObject.Find("Start Button").GetComponent<Button>();
        btnStart.onClick.AddListener(OnStartClicked);

        GameObject.Find("Score Text").GetComponent<Text>().text = $"Score: {Prefs.Score}";
        GameObject.Find("High Score Text").GetComponent<Text>().text = $"High Score: {Prefs.HighScore}";
    }

    void OnStartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
