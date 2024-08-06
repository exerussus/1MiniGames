using System;
using UnityEngine;

namespace Exerussus._1MiniGames
{
    public abstract class MiniGame<T> : MonoBehaviour, IMiniGame where T : MiniGamesApi.IGameResultData
    {
        [SerializeField] private Level[] levels;
        private int _currentLevelIndex;
        private Level _currentLevel;
        private T _miniGameData;
        private Action<T> _onGameOverAction;
        private MiniGamesApi.GameActions _gameActions;
        
        public Level[] Levels => levels;
        public T Data => _miniGameData;

        public void Initialize(T miniGameResultData, Action<T> onGameOver, MiniGamesApi.GameAction[] gameActions)
        {
            _miniGameData = miniGameResultData;
            _onGameOverAction = onGameOver;
            _gameActions = new MiniGamesApi.GameActions(gameActions);
        }

        public void LoadLevel(int levelIndex)
        {
            if (_currentLevel != null) UnloadLevel(_currentLevel);
            _currentLevel = Instantiate(levels[_currentLevelIndex].gameObject).GetComponent<Level>();
            _currentLevelIndex = levelIndex;
            _currentLevel.Initialize();
        }

        public void UnloadLevel(Level level)
        {
            Destroy(level.gameObject);
        }

        public abstract void Run();
        public abstract void Pause();
        public abstract void Abort();
        public abstract void InvokeGameOver(bool isWon);
        public abstract void InvokeAction(string actionKey);
    }
}