using System.Diagnostics;
using System.Linq;
using UnityEngine;
namespace ConnectFour.AI.AI_FoulWind
{
    public class AI_FoulWind : MonoBehaviour, IBrain
    {
        public int searchDepth = 5;
        private BoardPosition[,] _currentBoard;
        private BoardPosition _lastMove;
        private TeamName _myTeam;
        private BoardScorer boardScorer = new BoardScorer();
        public ColumnNumber ChooseColumnIndex(GameState gameState)
        {
            _currentBoard = gameState.CurrentBoardState;


            if (_lastMove == null)
            {
                _lastMove = gameState.AvailableMoves.First(x => x.XIndex == 3);
                return 3;
            }
             Stopwatch sw = new Stopwatch();
             sw.Start();
            Position rootNode = BuildRootNode();
            _lastMove = GetBestMove(rootNode);
             sw.Stop();
             UnityEngine.Debug.Log($"Took {sw.ElapsedMilliseconds} ms to complete tree.");


            //Log file to tree.tst
              TreeLogger treeLogger = new TreeLogger();
              treeLogger.LogTree(rootNode);
            return _lastMove.XIndex;
        }

        public void OnRoundCompletion()
        {
            _lastMove = null;
        }
        private BoardPosition GetBestMove(Position rootNode)
        {
            Position defaultAlpha = new Position(_currentBoard) { Score = -101 };
            Position defaultBeta = new Position(_currentBoard) { Score = 101 };
            Position bestMove = MiniMax(rootNode, defaultAlpha, defaultBeta, searchDepth,true);
            while (bestMove.Parent.Parent != null)
            {
                bestMove = bestMove.Parent;
            }
            return bestMove.StaticPosition;
        }
        private Position BuildRootNode()
        {
            Position rootNode = new Position(_currentBoard)
            {
                StaticPosition = _lastMove
            };
            TreeBuilder treeBuilder = new TreeBuilder(_myTeam, searchDepth);
          
            treeBuilder.BuildChildren(rootNode, _currentBoard, _myTeam, searchDepth);
            return rootNode;

        }
        private Position MiniMax(Position currentPos, Position alpha, Position beta, int depth, bool maximizing)
        {
            
            int score = currentPos.Score = boardScorer.ScoreBoard(currentPos.BoardAtThisState, _myTeam);
            currentPos.Score = score;
            if (depth == 0 || Mathf.Abs(score)== 100)
            {                
                return currentPos;
            }
            if (maximizing)
            {
                int maxScore = -101;
                Position bestPos = currentPos;
                foreach (var child in currentPos.Children)
                {
                    Position pos = MiniMax(child, alpha, beta, depth -1 ,false);
                    pos.Score = boardScorer.ScoreBoard(pos.BoardAtThisState, _myTeam);
                    if (pos.Score > maxScore)
                    {
                        maxScore = pos.Score;
                        bestPos = pos;
                    }
                    alpha = ReturnMax(alpha, pos);
                    if (beta.Score <= alpha.Score) { break; }
                }
                return bestPos;
            }
            else
            {
                int minScore = 101;
                Position worstPos = currentPos;
                foreach (var child in currentPos.Children)
                {
                    Position pos = MiniMax(child, alpha, beta, depth -1, true);
                    pos.Score = boardScorer.ScoreBoard(pos.BoardAtThisState, _myTeam);
                    if (pos.Score < minScore)
                    {
                        minScore = pos.Score;
                        worstPos = pos;
                    }
                    beta = ReturnMin(beta, pos);
                    if (beta.Score <= alpha.Score) { break; }
                }
                return worstPos;
            }

        }
        public Position ReturnMax(Position a, Position b)
        {
            if (a.Score > b.Score) { return a; }
            return b;
        }
        public Position ReturnMin(Position a, Position b)
        {
            if (a.Score < b.Score) { return a; }
            return b;
        }
        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }

    }
}