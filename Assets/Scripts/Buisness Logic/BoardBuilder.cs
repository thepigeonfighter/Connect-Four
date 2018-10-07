using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoardBuilder:IBoardBuilder {
	//These values are hard coded in. Very unstable by works for now
    //private float gizmoRadius =.5f;
    private float yOffset = -2.18f;
    private float xOffset = -3.63f;
    private float xPadding = 1.2f;
    private float yPadding = 1.3f;
    private BoardPosition[,] boardPositions = new BoardPosition[7, 6];

    BoardPosition[,] IBoardBuilder.BuildBoard()
    {
        for (int i = 0; i < 7 ; i++)
		{
			for (int j = 0; j < 6; j++)
			{
                BoardPosition boardPosition = new BoardPosition();
				boardPosition.Position =  new Vector2(i * xPadding, j * yPadding) + new Vector2(xOffset,yOffset);
                boardPosition.XIndex = i;
                boardPosition.YIndex = j;
                boardPositions[i, j] = boardPosition;
            }
		}
        return boardPositions;
    }

}
