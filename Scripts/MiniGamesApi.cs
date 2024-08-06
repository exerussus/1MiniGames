
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exerussus._1MiniGames
{
    public class MiniGamesApi : MonoBehaviour
    {
        #region Private Feilds and Properties

        private static MiniGamesApi _instance;
        private static MiniGamesData _miniGamesData;
        private static bool _isInitialized;
        private readonly Dictionary<string, GameObject> _miniGames = new();

        private static MiniGamesApi Instance
        {
            get
            {
                if (!_isInitialized) Initialize();
                return _instance;
            }
        }

        #endregion

        #region Public Properties

        public static bool IsRunning { get; private set; }

        #endregion
        
        #region Initializing
        
        private static void Initialize()
        {
            _isInitialized = true;
            _instance = new GameObject { name = "MiniGamesApi"} .AddComponent<MiniGamesApi>();
            _miniGamesData = Resources.Load<MiniGamesData>(Constants.ResourceFilePath);

            foreach (var miniGamePack in _miniGamesData.MiniGames) _instance._miniGames[miniGamePack.Name] = miniGamePack.Prefab;
            
            DontDestroyOnLoad(_instance);
        }

        #endregion

        #region Methods

        public bool TryGetMiniGame<T>(string gameName, out MiniGame<T> miniGame) where T : IGameResultData
        {
            if (_instance._miniGames.TryGetValue(gameName, out var gameObject))
            {
                var foundedMiniGame = gameObject.GetComponent<MiniGame<T>>();
                if (foundedMiniGame != null)
                {
                    miniGame = foundedMiniGame;
                    return true;
                }
            }
            miniGame = default;
            return false;
        }
        
        public static void RunGame<T>(string gameName, T miniGameData, GameAction[] gameActions = null, Action<T> onGameOver = null) 
            where T : IGameResultData
        {
            if (!Instance.TryGetMiniGame(gameName, out MiniGame<T> miniGamePrefab)) return;
    
            var miniGame = Instantiate(miniGamePrefab.gameObject).GetComponent<MiniGame<T>>();
            
            miniGame.Initialize(miniGameData, onGameOver, gameActions);
            miniGame.Run();
        }

        #endregion
        
        public interface IGameResultData
        {
            public bool IsWon { get; set; }
        }

        public class GameActions
        {
            public GameActions(GameAction[] gameActions)
            {
                if (gameActions == null || gameActions.Length == 0)
                {
                    _isEmpty = true;
                    return;
                }
                
                foreach (var gameAction in gameActions)
                {
                    _actions[gameAction.Name] = gameAction.Action;
                }
            }
            
            private Dictionary<string, Action> _actions = new();
            private bool _isEmpty;

            public void TryInvoke(string actionKey)
            {
                if (_isEmpty) return;
                if (_actions.TryGetValue(actionKey, out var action)) action.Invoke();
            }
        }

        public struct GameAction
        {
            public string Name;
            public Action Action;
        }
    }

    public interface IMiniGame
    {
        public abstract void Run();
        public abstract void Pause();
        public abstract void Abort();
        public abstract void InvokeGameOver(bool isWon);
        public abstract void InvokeAction(string actionKey);
    }
}
