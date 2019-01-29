using ConnectFour;
using System.Collections.Generic;
using UnityEngine;

public class AITester : MonoBehaviour
{
    public AI_Base teamBlack;
    public AI_Base teamRed;
    public int testSpeed;
    public int iterationsPerTest;

    private bool testingActive;
    private int testCount;
    private GameManager gameManager;
    private MoveManager moveManager;
    private StatsView statsView;
    private GameResultsView gameResultsView;
    private GameSpeedController gameSpeedController;
    private TeamStats teamAStats;
    private TeamStats teamBStats;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameSpeedController = FindObjectOfType<GameSpeedController>();

        switch (teamBlack.GetTeam())
        {
            case TeamName.BlackTeam:
                teamAStats = ScoreKeeper.GetBlackTeamStats();
                teamBStats = ScoreKeeper.GetRedTeamStats();
                break;
            case TeamName.RedTeam:
                teamAStats = ScoreKeeper.GetRedTeamStats();
                teamBStats = ScoreKeeper.GetBlackTeamStats();
                break;
        }

        teamAStats = ScoreKeeper.GetBlackTeamStats();
        teamBStats = ScoreKeeper.GetRedTeamStats();

        gameSpeedController.speed = testSpeed;

        StartNewTest();
    }

    // Update is called once per frame
    void Update()
    {
        if (testingActive)
        {
            if (gameManager.RoundCount > testCount)
            {
                gameManager.AutoPlayToggle.isOn = false;
                testingActive = false;
                Invoke("GetResults", 20f);
            }
        }
    }

    void GetResults()
    {
        var teamAResult = teamAStats.WinCount;
        var teamBResult = teamBStats.WinCount;
        var results = new List<int> { teamAResult, teamBResult };
        results.Sort();
        var resultString = teamAResult > teamBResult ? teamBlack._myName + " (" + teamBlack.GetTeam() + ")" : teamRed._myName + " (" + teamRed.GetTeam() + ")";
        var percentBetter = (Mathf.Round((float)(results[1] - results[0]) / results[0] * 100));

        Debug.Log(testCount + 1 + " rounds completed");
        Debug.Log(resultString + " won the test " + results[1] + "/" + results[0] + ", performing " + percentBetter + "% better.");
    }

    void StartNewTest()
    {
        gameManager.ResetGame();
        testCount = iterationsPerTest - 1;
        gameManager.AutoPlayToggle.isOn = true;
        gameManager.RoundCount = 0;
        testingActive = true;
    }
}
