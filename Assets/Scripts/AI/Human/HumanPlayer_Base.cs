using System;
using UnityEngine;
namespace ConnectFour
{
    public class HumanPlayer_Base : Player_Base, IPlayer
    {
        protected TeamName _myTeam;
        public string _myName = "Human";
        public bool PickRandomName = true;
        protected Guid _mySecurityHandle;
        private IHuman brain;
        public EventHandler<MoveEvent> OnMoveComplete { get; set; }
        private void Start()
        {
            brain = GetComponent<IHuman>();
            if(PickRandomName)
            {
                _myName = AdjectiveHolder.GetRandomName();
            }
            GameObject.FindObjectOfType<MoveManager>().RegisterPlayer(this);
        }
        public override string GetName()
        {
            return _myName;
        }

        public override void MakeMove(ColumnNumber index)
        {
           
            Move move = new Move();
            move.Column = index;
            move.MyTeam = _myTeam;
			move.Player = this;
            MoveEvent thisMove = new MoveEvent()
            {
                MyMove = move,
                MySecurityHandle = _mySecurityHandle
            };
            if (OnMoveComplete != null)
            {
                OnMoveComplete.Invoke(this, thisMove);
            }
        }

        public void OnTurnRequested(GameState gameState)
        {          
            MessageBoard.Instance.ShowMessage( _myName + ", it is your turn. Click one of the green buttons to drop a piece in that column");
            brain.GetMove(gameState, OnTurnChosen);
        }
        public void OnTurnChosen(ColumnNumber index)
        {
            MakeMove(index);
        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
            if(_myName == "Human")
            {
                _myName = teamName.ToString();
            }
        }

        public override void SignUp(Guid securityKey)
        {
            _mySecurityHandle = securityKey;
        }

        public void OnRoundCompleted()
        {

        }
    }
}