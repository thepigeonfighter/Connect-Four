using ConnectFour.AI.AI_FoulWind;
using ConnectFour.BuisnessLogic;
using NUnit.Framework;

namespace Tests
{
    public class BoardScorerTest
    {
        [Test]
        public void WhenOwnWinScoreEQ100()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = GetSimpleWinBlack();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.BlackTeam);
            Assert.AreEqual(100, score);
        }
        [Test]
        public void WhenLoseScoreEQ_NEG100()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = GetSimpleWinBlack();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.RedTeam);
            Assert.AreEqual(-100, score);
        }
        [Test]
        public void GivenUnbeatablePairEQ95()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = GetValidHorizontalSet();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.BlackTeam);
            Assert.AreEqual(95, score);
        }
        [Test]
        public void GivenUnwinnableSetEQ_NEG95()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = GetValidHorizontalSet();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.RedTeam);
            Assert.AreEqual(-95, score);
        }
        [Test]
        public void GivenUnbeatableSetElevatedNEQ100()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = GetInvalidElevatedSet();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.BlackTeam);
            Assert.AreNotEqual(100, score);
        }
        [Test]
        public void GivenValidElevatedSetEQ95()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = GetValidElevatedHorizontalSet();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.BlackTeam);
            Assert.AreEqual(95, score);
        }
        [Test]
        public void GivenBlockableBoardEQ0()
        {
            BoardScorer boardScorer = new BoardScorer();
            BoardPosition[,] simpleBlackTeamWin = BuildBlockableBoard();
            int score = boardScorer.ScoreBoard(simpleBlackTeamWin, TeamName.BlackTeam);
            Assert.AreEqual(0, score);
        }

        //------------------------DATA------------------------------ 
        private BoardPosition[,] BuildBlankBoard()
        {
            IBoardBuilder boardBuilder = new BoardBuilder();
            return boardBuilder.BuildBoard();
        }
        private BoardPosition[,] BuildBlockableBoard()
        {
            BoardPosition[,] board = BuildBlankBoard();
            for (int i = 0; i < 3; i++)
            {
                board[i, 0] = new BoardPosition
                {
                    Owner = TeamName.BlackTeam,
                    IsOccupied = true,
                    XIndex = i,
                    YIndex = 0
                };
            }
            return board;
        }
        private BoardPosition[,] GetInvalidElevatedSet()
        {
            BoardPosition[,] board = BuildBlankBoard();
            for (int j = 1; j < 4; j++)
            {

                board[j, 1] = new BoardPosition
                {
                    Owner = TeamName.BlackTeam,
                    IsOccupied = true,
                    XIndex = j,
                    YIndex = 1
                };
            }
            return board;
        }
        private BoardPosition[,] GetValidElevatedHorizontalSet()
        {
            BoardPosition[,] board = BuildBlankBoard();
            board[0, 0].IsOccupied = true;
            board[0, 0].Owner = TeamName.RedTeam;
            board[4, 0].IsOccupied = true;
            board[4, 0].Owner = TeamName.RedTeam;
            for (int j = 1; j < 4; j++)
            {

                board[j, 1] = new BoardPosition
                {
                    Owner = TeamName.BlackTeam,
                    IsOccupied = true,
                    XIndex = j,
                    YIndex = 1
                };
            }
            return board;
        }
        private BoardPosition[,] GetValidHorizontalSet()
        {
            BoardPosition[,] board = BuildBlankBoard();
            board[1, 0] = new BoardPosition { Owner = TeamName.BlackTeam, IsOccupied = true, XIndex = 1, YIndex = 0 };
            board[2, 0] = new BoardPosition { Owner = TeamName.BlackTeam, IsOccupied = true, XIndex = 2, YIndex = 0 };
            board[3, 0] = new BoardPosition { Owner = TeamName.BlackTeam, IsOccupied = true, XIndex = 3, YIndex = 0 };
            return board;
        }
        private BoardPosition[,] GetSimpleWinBlack()
        {
            BoardPosition[,] board = BuildBlankBoard();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    board[i, j] = new BoardPosition
                    {
                        Owner = TeamName.BlackTeam,
                        IsOccupied = true,
                        XIndex = i,
                        YIndex = j
                    };
                }
            }
            return board;
        }
    }
}
