using System;
using UnityEngine;

namespace Exerussus._1MiniGames
{
    public class MiniGamesData : ScriptableObject
    {
        [SerializeField] private MiniGamePack[] miniGames;

        public MiniGamePack[] MiniGames => miniGames;

        [Serializable]
        public class MiniGamePack
        {
            [SerializeField] private string name;
            [SerializeField] private GameObject prefab;

            public string Name => name;
            public GameObject Prefab => prefab;
        }
    }
}