using System;
using System.IO;

namespace Exerussus._1MiniGames
{
#if UNITY_EDITOR
    
    using UnityEditor;
    using UnityEngine;

    [InitializeOnLoad]
    public class ScriptImporter
    {
        static ScriptImporter()
        {
            if (Resources.Load<MiniGamesData>(MiniGamesApi.ResourceFilePath) == null)
            {
                CreateMiniGamesData();
            }
        }
        
        public static void CreateMiniGamesData()
        {
            MiniGamesData miniGame = ScriptableObject.CreateInstance<MiniGamesData>();

            var folderPath = "Assets/Resources"; 
        
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "MiniGames");
            }

            string assetPathAndName = Path.Combine(folderPath, "MiniGamesData.asset");

            try
            {
                AssetDatabase.CreateAsset(miniGame, assetPathAndName);
                AssetDatabase.SaveAssets(); 
            }
            catch (Exception e)
            {
                throw;
            }

            Debug.Log("MiniGame asset created at: " + assetPathAndName);
        }
    }
     
#endif
}