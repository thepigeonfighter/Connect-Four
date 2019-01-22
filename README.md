# Connect-Four
A small unity project built for making connect four AI's.
If you would like to build an AI it is really simple just follow the steps below

1 Create a gameobject in the scene view and name it whatever you want your AI to be named.

2 Add an AI_Base Script Component to the GameObject  and change the name to match the gameobject's name.

3 Add another Script component onto the gameobject titlted AI_*Your AI Name Here*

4 Move that script into a folder titled by the same name. That folder should hold all scripts that your AI will need

5 Your AI script must implement the "IBrain" Interface in order for the game to work. The interface is really simple only has 
three methods
          SetTeam(TeamName teamName)
          Use this method to set a private variable to keep track of which color you are playing as.
          A manager class will call this method and pass you your team color. 
          
          OnRoundCompletion()
          As the name suggest this method will be called at the end of each game. Assuming your AI will 
          play multiple rounds this method gives you a chance to reset any local data you might have from the 
          previous round.
          
          ColumnIndex ChooseColumnIndex(Gamestate gameState)
          This is the main method of the IBrain interface. This method gets called everytime it is your turn to 
          make a move. An outside manager class will pass in a snapshot of what the gamestate currently looks like
          and it will expect a column index in return. The gamestate object is pretty simple.
          
          public class GameState
          {
              public BoardPosition[,] CurrentBoardState{ get; set; }
              public List<ColumnIndex> AvailableColumns{ get; set; }
	      public List<BoardPosition> AvailableMoves{get;set;}
          }
          Use the current board state to decide where your next move should be placed. 
          Each boardPosition has the following information:
          
          This is position in WorldSpace
          public Vector2 Position { get; set; }
          XIndex on the gameboard
	        public int XIndex { get; set; }
          YIndex on the gameboard
	        public int YIndex { get; set; }
	        public bool IsOccupied { get; set; }
          Will be red , black or empty
	        public TeamName Owner{ get; set; }      
	        public DateTime TimeSet{ get; set; }
          public IPlayer Player{ get; set; }

6 Make your move! You can refer to the "Random AI class for the simplest implementation of this class. For a slightly
complex take on it you can refer to AI_Torgo which was my first pass at making a competitive AI.
