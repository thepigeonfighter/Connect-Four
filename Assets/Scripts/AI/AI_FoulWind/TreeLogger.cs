using System.Collections.Generic;
using System.IO;
using System.Text;
namespace ConnectFour.AI.AI_FoulWind
{
    public class TreeLogger
    {
        private Queue<Position> neighborQueue = new Queue<Position>();
        private Dictionary<Position, StringBuilder> parentStrings = new Dictionary<Position, StringBuilder>();
        private StringBuilder defaultBuilder;
        private StringBuilder boardBuilder = new StringBuilder();
        private StringBuilder bodyBuilder = new StringBuilder();
        public void LogTree(Position rootNode)
        {
            InitializeStringBuilder(rootNode);
            neighborQueue.Enqueue(rootNode);
            LoopThroughNeighbors();
            defaultBuilder.Append(BuildMessageBody());
            defaultBuilder.Append("---------------------------------END TREE----------------------------------\n");
            string message = defaultBuilder.ToString();
            File.WriteAllText(@"tree.tst", message);

        }
        private void InitializeStringBuilder(Position rootNode)
        {
            defaultBuilder = new StringBuilder();
            defaultBuilder.Append($"--------------------ROOT NODE @ Position ({rootNode.StaticPosition.XIndex}, {rootNode.StaticPosition.YIndex}) ------------\n");
            parentStrings.Add(rootNode, defaultBuilder);
            defaultBuilder.Append("--------------------START TREE ------------------------\n");
        }
        private void LoopThroughNeighbors()
        {
            while (neighborQueue.Count > 0)
            {
                int spaces = 0;
                Position neighbor = neighborQueue.Dequeue();
                var sb = GetStringBuilder(neighbor.Parent);
                var tempNeighbor = neighbor;
                while (tempNeighbor.Parent != null)
                {
                    tempNeighbor = tempNeighbor.Parent;
                    spaces++;
                }
                foreach (var child in neighbor.Children)
                {
                    neighborQueue.Enqueue(child);
                }
                sb.Append(NodeDescription(neighbor, spaces));
            }
        }
        private string NodeDescription(Position node, int spaces)
        {
            string whiteSpace = GetBuffer(spaces);
            boardBuilder.Clear();
            boardBuilder.Append(whiteSpace);
            boardBuilder.Append("Node number ");
            boardBuilder.Append(node.ChildNumber);
            boardBuilder.Append(" Position ");
            boardBuilder.Append(node.StaticPosition.XIndex);
            boardBuilder.Append(",");
            boardBuilder.Append(node.StaticPosition.YIndex);
            boardBuilder.Append(" scored ");
            boardBuilder.Append(node.Score);
            boardBuilder.Append(".\n");
            boardBuilder.Append(whiteSpace);
            boardBuilder.Append("BoardState[\n");
            TranslateBoard(boardBuilder, node, whiteSpace);
            boardBuilder.Append("\n");
            return boardBuilder.ToString();
        }
        private string BuildMessageBody()
        {
            
            foreach (var parent in parentStrings)
            {
                bodyBuilder.Append(parent.Value.ToString());
                bodyBuilder.Append("---------------------- END SUBTREE of Node #");
                bodyBuilder.Append(parent.Key.ChildNumber);                
                bodyBuilder.Append("-------------\n");

            }
            return bodyBuilder.ToString();
        }
        private StringBuilder GetStringBuilder(Position parent)
        {
            if (parent == null)
            {
                return defaultBuilder;
            }
            if (parentStrings.ContainsKey(parent))
            {
                return parentStrings[parent];
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"----------------------NEW SUBTREE of Node #{parent.ChildNumber}------------\n");
                parentStrings.Add(parent, sb);
                return sb;
            }
        }
        private string GetBuffer(int spaces)
        {
            string whiteSpace = "";
            for (int i = 0; i < spaces * 4; i++)
            {
                whiteSpace += " ";
            }
            return whiteSpace;
        }
        //Optimized to make strings faster
        private void TranslateBoard(StringBuilder sb, Position position, string whiteSpace)
        {
            sb.Append(whiteSpace);
            sb.Append("---------------------\n");
            sb.Append(whiteSpace);
            for (int i = 5; i >= 0; i--)
            {
                for (int j = 6; j >= 0; j--)
                {
                    sb.Append('|');
                    sb.Append(GetTeamLetter(position.BoardAtThisState[j, i].Owner));
                    sb.Append('|');
                }
                sb.Append("\n");
                sb.Append(whiteSpace);
                sb.Append("---------------------\n");
                sb.Append(whiteSpace);
            }

        }
        private char GetTeamLetter(TeamName teamName)
        {
            switch (teamName)
            {
                case TeamName.Empty:
                    return 'O';
                case TeamName.RedTeam:
                    return 'R';
                case TeamName.BlackTeam:
                    return 'B';
                default:
                    return '0';
            }
        }
    }
}