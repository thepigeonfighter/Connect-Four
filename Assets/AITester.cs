using ConnectFour;
using ConnectFour.AI.AI_Friar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITester : MonoBehaviour
{
    public AI_Base teamA;
    public AI_Base teamB;
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

        switch (teamA.GetTeam())
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
                Debug.Log(testCount + 1 + " rounds completed");
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
        var resultString = teamAResult > teamBResult ? teamA._myName + " (" + teamA.GetTeam() + ")" : teamB._myName + " (" + teamB.GetTeam() + ")";
        var results = new List<int> { teamAResult, teamBResult };
        results.Sort();
        Debug.Log(resultString + " won the test " + results[1] + "/" + results[0]);
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
