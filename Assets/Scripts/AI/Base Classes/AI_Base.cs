using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConnectFour
{
    //[RequireComponent(typeof(IBrain))]
    public class AI_Base : Player_Base, IPlayer
    {
        protected TeamName _myTeam;
        public string _myName = "Random AI";
        public bool PickRandomName = true;
        protected GUID _mySecurityHandle;
        private IBrain brain;
        public EventHandler<MoveEvent> OnMoveComplete { get; set; }
        private void Start()
        {
            brain = GetComponent<IBrain>();     
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
            MakeMove(brain.ChooseColumnIndex(gameState));
        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
            brain.SetTeam(teamName);
        }

        public override void SignUp(GUID securityKey)
        {
            _mySecurityHandle = securityKey;
        }

        public void OnRoundCompleted()
        {
            brain.OnRoundCompletion();
        }
    }
}