using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
namespace ConnectFour
{
    public class HumanPlayer_Base : Player_Base, IPlayer
    {
        protected TeamName _myTeam;
        public string _myName = "Random AI";
        public bool PickRandomName = true;
        protected GUID _mySecurityHandle;
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

        public override void MakeMove(ColumnIndex index)
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

        public async void OnTurnRequested(GameState gameState)
        {
            ColumnIndex index = await brain.GetDesiredMoveAsync(gameState);
            MakeMove(index);
        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }

        public override void SignUp(GUID securityKey)
        {
            _mySecurityHandle = securityKey;
        }
    }
}