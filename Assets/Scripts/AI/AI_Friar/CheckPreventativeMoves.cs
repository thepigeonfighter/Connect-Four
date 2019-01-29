namespace ConnectFour.AI.AI_Friar
{
    public class CheckPreventativeMoves
    {
        #region Explanation
        /* Prevent block: You are 'o'. Placing a tile on either '!' below would allow enemy to block you.
           Forcing them to fill that position will guarantee a win.

           Block enemy: Enemy is 'o'. Placing a tile on either '!' below would allow the enemy to win next turn.
         
         - - o !        - - - -
         - o x x        o o - o
         o x x o        x o ! x

         */ 
        #endregion

        private GameState _gameState;
        private BoardPosition[,] currentBoardState;
        private TeamName _myTeam;

        public int[,] GetMoves(GameState gameState, int[,] movesToWinWeight)
        {
            //Init here since can't use constructors
            currentBoardState = gameState.CurrentBoardState;
            _gameState = gameState;

            var movesToAvoid = new int[7, 6];

            for (var v = 0; v < 6; v++)
            {
                for (var h = 0; h < 7; h++)
                {
                    //If position has 1 move to win
                    if (movesToWinWeight[h, v] == 1)
                    {
                        //And if the tile below it is empty
                        if (v - 1 < 0)
                        {
                            break;
                        }

                        if (!gameState.CurrentBoardState[h, v - 1].IsOccupied)
                        {
                            //Nuke that tile
                            movesToAvoid[h, v - 1] = 1;
                        }
                    }
                }
            }
            return movesToAvoid;
        }
    }
}
