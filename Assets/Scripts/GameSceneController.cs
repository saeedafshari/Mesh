using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    GameObject fabGraph;
    Text txtScore;
    Text txtTime;
    Button btnStop;

    public int Score { get; private set; }
    public int TimeLeft { get; private set; }
    public bool IsAlive { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        fabGraph = Resources.Load<GameObject>("Prefabs/Graph Controller");

        txtScore = GameObject.Find("Score Text").GetComponent<Text>();
        txtTime = GameObject.Find("Time Text").GetComponent<Text>();
        btnStop = GameObject.Find("Stop Button").GetComponent<Button>();

        btnStop.onClick.AddListener(OnStopClicked);

        Next(false);

        IsAlive = true;
        TimeLeft = 10;
        StartCoroutine(Tick());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            ScreenCapture.CaptureScreenshot($"Screenshot_{System.DateTime.Now.Ticks}.jpg", 1);
    }

    IEnumerator Tick()
    {
        while (IsAlive)
        {
            UpdateUI();
            yield return new WaitForSeconds(1);
            TimeLeft--;
            if (TimeLeft < 0)
            {
                EndGame();
            }
        }
    }

    void UpdateUI()
    {
        txtScore.text = $"Score: {Score}";
        txtTime.text = $"Time: {TimeLeft}";
    }

    public void Next(bool addScore)
    {
        var obj = Instantiate(fabGraph);
        if (addScore)
        {
            Score += TimeLeft;
            TimeLeft += 5;
            UpdateUI();
        }
    }

    void OnStopClicked()
    {
        EndGame();
    }

    void EndGame()
    {
        Prefs.Score = Score;
        TimeLeft = 0;
        IsAlive = false;
        SceneManager.LoadScene("GameOverScene");
    }
}
