using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPosition{

	public Vector2 Position { get; set; }
	public int XIndex { get; set; }
	public int YIndex { get; set; }
	public bool IsOccupied { get; set; }
	public TeamName Owner{ get; set; }
	public DateTime TimeSet{ get; set; }
	public IPlayer Player{ get; set; }

	public override bool Equals(object obj)
	{
		if(obj == null || !this.GetType().Equals(obj.GetType()))
		{
            return false;
        }
        BoardPosition position = (BoardPosition)obj;
        return (position.XIndex == this.XIndex) && (position.YIndex == this.YIndex);
    }
	public override int GetHashCode()
	{
        return (XIndex << 2) ^ YIndex;
    }

}
