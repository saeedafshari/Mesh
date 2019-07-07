using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialSceneController : MonoBehaviour
{
    Button btnStart;
    Text txtObjective;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        var scripts = new[]
        {
            "Swipe your finger to draw an edge between nodes.\nDisconnect nodes by swiping on them.\nWin the game by making all nodes 0.\n\nA Neat Games production.",
        };

        var script = $"Instructions:\n" + scripts[Random.Range(0, scripts.Length)];

        txtObjective = GameObject.Find("Objective").GetComponent<Text>();
        btnStart = GameObject.Find("Start Button").GetComponent<Button>();

        txtObjective.text = script;
        btnStart.onClick.AddListener(OnStartClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
