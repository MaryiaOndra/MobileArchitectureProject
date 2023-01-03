﻿using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }
        public static IInputService InputService { get; set; }

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    }
}