using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using ConnectFour;
using ConnectFour.AI.AI_Torgo;
using System.Collections.Generic;

public class OptionBuilderTest
{
    /*

    [Test]
    public void OptionBuilderTestSimplePasses()
    {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator OptionBuilderTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
    [Test]
    public void BottomLeftBoardPosTest()
    {
        OptionBuilder builder = new OptionBuilder();
       //Option option = builder.BuildOption(BottomLeftBoardPosition());
        List<Vector2Int> expectedTargets = new List<Vector2Int>()
        {
            new Vector2Int(0,3),
            new Vector2Int(3,3),
            new Vector2Int(3,0),
        };
       // Assert.AreEqual(expectedTargets, option.TargetIndices);

    }
    /*
    Available targets should not just be vanilla they need to specifically calculate to the board positions 
    index and adjust accordingly
     
    [Test]
    public void BottomRightBoardPosTest()
    {
        OptionBuilder builder = new OptionBuilder();
        //Option option = builder.BuildOption(BottomRightBoardPosition());
        List<Vector2Int> expectedTargets = new List<Vector2Int>()
        {
            new Vector2Int(0,3),
            new Vector2Int(-3,3),
            new Vector2Int(-3,0),
        };
       // Assert.AreEqual(expectedTargets, option.TargetIndices);

    }
    [Test]
    public void TopRightBoardPosTest()
    {
        OptionBuilder builder = new OptionBuilder();
      //  Option option = builder.BuildOption(BottomRightBoardPosition());
        List<Vector2Int> expectedTargets = new List<Vector2Int>()
        {
            new Vector2Int(0,3),
            new Vector2Int(-3,3),
            new Vector2Int(-3,0),
        };
       // Assert.AreEqual(expectedTargets, option.TargetIndices);

    }
    private BoardPosition BottomLeftBoardPosition()
    {
        BoardPosition bp = new BoardPosition()
        {
            XIndex = 2,
            YIndex = 2,
            IsOccupied = false,

        };
        return bp;
    }
    private BoardPosition BottomRightBoardPosition()
    {
        BoardPosition bp = new BoardPosition()
        {
            XIndex = 5,
            YIndex = 1,
            IsOccupied = false,

        };
        return bp;
    }
    */
}
