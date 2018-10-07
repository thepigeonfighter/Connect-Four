using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConnectFour
{
    [RequireComponent(typeof(IBrain))]
    public class AI_Base : Player_Base, IPlayer
    {
        protected TeamName _myTeam;
        protected string _myName = "AI_Random";
        protected GUID _mySecurityHandle;
        private IBrain brain;
        public EventHandler<MoveEvent> OnMoveComplete { get; set; }
        private void Start()
        {
            brain = GetComponent<IBrain>();
            GameObject.FindObjectOfType<MoveManager>().RegisterPlayer(this);
        }
        public override string GetName()
        {
            return _myName;
        }

        public override void MakeMove(Move move)
        {
            move.MyTeam = _myTeam;
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
            MakeMove(brain.GetDesiredMove(gameState));
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