using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using ConnectFour;
using System.Collections.Generic;

public class CheckWinTests
{
    private enum WinType { Horizontal, Vertical, Diagonal }
    private bool CheckForWin(List<BoardPosition> moves, WinType winType)
    {
        CheckForWin winChecker = new CheckForWin();
        switch (winType)
        {
            case WinType.Diagonal:
                return winChecker.CheckForDiagonalWin(moves);
            case WinType.Vertical:
                return winChecker.CheckForVerticalWin(moves);
            case WinType.Horizontal:
                return winChecker.CheckForHorizontalWin(moves);
            default:
                return false;

        }


    }
    #region Horizontal Test Cases
    //Naming Convention is 
    // Type of case we are looking at
    // Expected result
    // Binary expression of the row where one represents a piece 
    // and zero represents either empty or that spot belonging to someone else
    [Test]
    public void TestForHorizontalWin_1111000()
    {
        List<BoardPosition> horizontalWin = W_GetHoriz1111000();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.True(actual);

    }
    [Test]
    public void TestForHorizontalWin_0001111()
    {
        List<BoardPosition> horizontalWin = W_GetHoriz0001111();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.True(actual);

    }
    [Test]
    public void TestForHorizontalWin_0111101()
    {
        List<BoardPosition> horizontalWin = W_GetHoriz0111101();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.True(actual);

    }
    [Test]
    public void TestForHorizontalLoss_1101100()
    {
        List<BoardPosition> horizontalLoss = L_GetHoriz1101100();
        bool actual = CheckForWin(horizontalLoss, WinType.Horizontal);
        Assert.False(actual);

    }
    [Test]
    public void TestForHorizontalWin_1011110()
    {
        List<BoardPosition> horizontalWin = W_GetHoriz1011110();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.True(actual);

    }
    [Test]
    public void TestForHorizontalWin_1001111()
    {
        List<BoardPosition> horizontalWin = W_GetHoriz1001111();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.True(actual);

    }
    [Test]
    public void TestForHorizontalWin_1101111()
    {
        List<BoardPosition> horizontalWin = W_GetHoriz1101111();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.True(actual);

    }
    [Test]
    public void TestForHorizontalLoss_1010110()
    {
        List<BoardPosition> horizontalWin = L_GetHoriz1010110();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.False(actual);

    }
    [Test]
    public void TestForHorizontalLoss_0000001()
    {
        List<BoardPosition> horizontalWin = L_GetHoriz0000001();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.False(actual);

    }
    [Test]
    public void TestForHorizontalLoss_1000000()
    {
        List<BoardPosition> horizontalWin = L_GetHoriz1000000();
        bool actual = CheckForWin(horizontalWin, WinType.Horizontal);
        Assert.False(actual);

    }
    #endregion
    #region  Vertical Test Cases
    [Test]
    public void TestForVertWin_111100()
    {
        List<BoardPosition> vertWin = W_GetVert_111100();
        bool actual = CheckForWin(vertWin, WinType.Vertical);
        Assert.True(actual);
    }
    [Test]
    public void TestForVertWin_011110()
    {
        List<BoardPosition> vertWin = W_GetVert_011110();
        bool actual = CheckForWin(vertWin, WinType.Vertical);
        Assert.True(actual);
    }
    [Test]
    public void TestForVertWin_101111()
    {
        List<BoardPosition> vertWin = W_GetVert_101111();
        bool actual = CheckForWin(vertWin, WinType.Vertical);
        Assert.True(actual);
    }
    [Test]
    public void TestForVertLoss_010100()
    {
        List<BoardPosition> vertLoss = L_GetVert_010100();
        bool actual = CheckForWin(vertLoss, WinType.Vertical);
        Assert.False(actual);
    }
    [Test]
    public void TestForVertLoss_100000()
    {
        List<BoardPosition> vertLoss = L_GetVert_100000();
        bool actual = CheckForWin(vertLoss, WinType.Vertical);
        Assert.False(actual);
    }
    [Test]
    public void TestForVertLoss_101110()
    {
        List<BoardPosition> vertLoss = L_GetVert_101110();
        bool actual = CheckForWin(vertLoss, WinType.Vertical);
        Assert.False(actual);
    }

    #endregion
    #region Diagonal Left to Right  Test Cases
    //Naming convention for this collection of test is:
    //  direction of the diagonal
    // expected result
    // origin coordinates
    [Test]
    public void TestLeftToRightDiagonalWin00()
    {
        List<BoardPosition> diagonalWin = W_LeftToRightDiagonal_00();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestLeftToRightDiagonalWin11()
    {
        List<BoardPosition> diagonalWin = W_LeftToRightDiagonal_11();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestLeftToRightDiagonalWin21()
    {
        List<BoardPosition> diagonalWin = W_LeftToRightDiagonal_21();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestLeftToRightDiagonalWin02()
    {
        List<BoardPosition> diagonalWin = W_LeftToRightDiagonal_02();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestLeftToRightDiagonalLoss02()
    {
        List<BoardPosition> diagonalWin = L_LeftToRightDiagonal_02();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.False(actual);
    }
    [Test]
    public void TestLeftToRightDiagonalLoss33()
    {
        List<BoardPosition> diagonalWin = L_LeftToRightDiagonal_33();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.False(actual);
    }
    [Test]
    public void TestLeftToRightDiagonalWin30()
    {
        List<BoardPosition> diagonalWin = W_LeftToRightDiagonal30();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    #endregion
    #region Diagonal Right to Left  Test Cases
    //Naming convention for this collection of test is:
    //  direction of the diagonal
    // expected result
    // origin coordinates
    [Test]
    public void TestDiagonalWinRightToLeft_05()
    {
        List<BoardPosition> diagonalWin = W_DiagonalRightToLeft_05();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestDiagonalWinRightToLeft_13()
    {
        List<BoardPosition> diagonalWin = W_DiagonalRightToLeft_13();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestDiagonalWinRightToLeft_34()
    {
        List<BoardPosition> diagonalWin = W_DiagonalRightToLeft_34();
        bool actual = CheckForWin(diagonalWin, WinType.Diagonal);
        Assert.True(actual);
    }
    [Test]
    public void TestDiagonalLossRightToLeft_34()
    {
        List<BoardPosition> diagonal = L_DiagonalRightToLeft_34();
        bool actual = CheckForWin(diagonal, WinType.Diagonal);
        Assert.False(actual);
    }
    [Test]
    public void TestDiagonalLossRightToLeft_33()
    {
        List<BoardPosition> diagonal = L_DiagonalRightToLeft_33();
        bool actual = CheckForWin(diagonal, WinType.Diagonal);
        Assert.False(actual);
    }
    #endregion
    #region  Diagonal Left to Right Cases Test Boards
    private List<BoardPosition> W_LeftToRightDiagonal30()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 1, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 2, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 6, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    private List<BoardPosition> W_LeftToRightDiagonal_00()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 1, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 2, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    private List<BoardPosition> W_LeftToRightDiagonal_11()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 1, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 2, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 4, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    private List<BoardPosition> W_LeftToRightDiagonal_21()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 1, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 2, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 4, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    private List<BoardPosition> W_LeftToRightDiagonal_02()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 2, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 4, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 5, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    private List<BoardPosition> L_LeftToRightDiagonal_02()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 2, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 4, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 5, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    private List<BoardPosition> L_LeftToRightDiagonal_33()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 3, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 4, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 5, Owner = TeamName.BlackTeam, IsOccupied = true });
        diagonalWin.Add(new BoardPosition() { XIndex = 6, YIndex = 5, Owner = TeamName.BlackTeam, IsOccupied = true });
        return diagonalWin;
    }
    #endregion
    #region Diagonal Right to Left Cases Test Boards
    private List<BoardPosition> W_DiagonalRightToLeft_05()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 5, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 4, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });

        return diagonalWin;
    }
    private List<BoardPosition> W_DiagonalRightToLeft_13()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 1, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 0, IsOccupied = true, Owner = TeamName.BlackTeam });

        return diagonalWin;
    }
    private List<BoardPosition> W_DiagonalRightToLeft_34()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 4, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 6, YIndex = 1, IsOccupied = true, Owner = TeamName.BlackTeam });
        return diagonalWin;
    }
    private List<BoardPosition> L_DiagonalRightToLeft_34()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 4, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        return diagonalWin;
    }
    private List<BoardPosition> L_DiagonalRightToLeft_33()
    {
        List<BoardPosition> diagonalWin = new List<BoardPosition>();
        diagonalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 1, IsOccupied = true, Owner = TeamName.BlackTeam });
        diagonalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 0, IsOccupied = true, Owner = TeamName.BlackTeam });
        return diagonalWin;
    }
    #endregion
    #region Horizontal Cases Test Boards
    private List<BoardPosition> W_GetHoriz1111000()
    {
        List<BoardPosition> horizontalWin = new List<BoardPosition>();
        horizontalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });

        return horizontalWin;

    }
    private List<BoardPosition> W_GetHoriz0001111()
    {
        List<BoardPosition> horizontalWin = new List<BoardPosition>();
        horizontalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 6, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });

        return horizontalWin;

    }
    private List<BoardPosition> W_GetHoriz1011110()
    {
        List<BoardPosition> horizontalWin = new List<BoardPosition>();
        horizontalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });


        return horizontalWin;

    }
    private List<BoardPosition> W_GetHoriz0111101()
    {
        List<BoardPosition> horizontalWin = new List<BoardPosition>();
        horizontalWin.Add(new BoardPosition() { XIndex = 1, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 2, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 6, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });


        return horizontalWin;

    }
    private List<BoardPosition> W_GetHoriz1001111()
    {
        List<BoardPosition> horizontalWin = new List<BoardPosition>();
        horizontalWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 5, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalWin.Add(new BoardPosition() { XIndex = 6, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });


        return horizontalWin;

    }
    private List<BoardPosition> W_GetHoriz1101111()
    {
        List<BoardPosition> horizontalLoss = new List<BoardPosition>();
        horizontalLoss.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 1, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 5, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 6, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });


        return horizontalLoss;

    }
    private List<BoardPosition> L_GetHoriz1010110()
    {
        List<BoardPosition> horizontalLoss = new List<BoardPosition>();
        horizontalLoss.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 2, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 5, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });


        return horizontalLoss;

    }
    private List<BoardPosition> L_GetHoriz0000001()
    {
        List<BoardPosition> horizontalLoss = new List<BoardPosition>();
        horizontalLoss.Add(new BoardPosition() { XIndex = 6, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        return horizontalLoss;

    }
    private List<BoardPosition> L_GetHoriz1000000()
    {
        List<BoardPosition> horizontalLoss = new List<BoardPosition>();
        horizontalLoss.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        return horizontalLoss;

    }
    private List<BoardPosition> L_GetHoriz1101100()
    {
        List<BoardPosition> horizontalLoss = new List<BoardPosition>();
        horizontalLoss.Add(new BoardPosition() { XIndex = 0, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 1, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 3, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });
        horizontalLoss.Add(new BoardPosition() { XIndex = 4, YIndex = 0, Owner = TeamName.BlackTeam, IsOccupied = true });


        return horizontalLoss;

    }

    #endregion
    #region Vertical Cases Test Boards
    List<BoardPosition> W_GetVert_111100()
    {
        List<BoardPosition> vertWin = new List<BoardPosition>();
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 1, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        return vertWin;
    }
    List<BoardPosition> W_GetVert_011110()
    {
        List<BoardPosition> vertWin = new List<BoardPosition>();
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 1, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 4, IsOccupied = true, Owner = TeamName.BlackTeam });
        return vertWin;
    }
    List<BoardPosition> W_GetVert_101111()
    {
        List<BoardPosition> vertWin = new List<BoardPosition>();
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 4, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 5, IsOccupied = true, Owner = TeamName.BlackTeam });
        return vertWin;
    }
    List<BoardPosition> L_GetVert_010100()
    {
        List<BoardPosition> vertWin = new List<BoardPosition>();
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 1, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        return vertWin;
    }
    List<BoardPosition> L_GetVert_100000()
    {
        List<BoardPosition> vertWin = new List<BoardPosition>();
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, IsOccupied = true, Owner = TeamName.BlackTeam });
        return vertWin;
    }
    List<BoardPosition> L_GetVert_101110()
    {
        List<BoardPosition> vertWin = new List<BoardPosition>();
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 0, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 2, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 3, IsOccupied = true, Owner = TeamName.BlackTeam });
        vertWin.Add(new BoardPosition() { XIndex = 0, YIndex = 4, IsOccupied = true, Owner = TeamName.BlackTeam });
        return vertWin;
    }
    #endregion
}

