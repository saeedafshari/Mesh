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
            "Swipe your finger to draw an edge between nodes. Disconnect nodes by swiping on them. Win the game by making all nodes 0. May contain nuts, use with caution. A Neat Games production. (c) 2019 NeatCapital OU.",
            "Swipe your finger to draw an edge between nodes. Disconnect nodes by swiping on them. Win the game by making all nodes 0. If the game lasts for more than four hours, call your doctor. A Neat Games production. (c) 2019 NeatCapital OU.",
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
