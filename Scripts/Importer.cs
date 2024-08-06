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
            if (Resources.Load<MiniGamesData>(Constants.ResourceFilePath) == null)
            {
                Debug.Log("ASDASDASDASASD");
                CreateAssetFile<MiniGamesData>();
            }
        }
        
        public static void CreateAssetFile<T>(string path = "Resources/MiniGames", Color color = default) where T : ScriptableObject
        {
            MiniGamesData miniGame = ScriptableObject.CreateInstance<MiniGamesData>();

            var folderPath = "Assets/Resources/MiniGames"; 
        
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "MiniGames");
            }

            string assetPathAndName = $"{folderPath}/MiniGamesData.asset";

            AssetDatabase.CreateAsset(miniGame, assetPathAndName);
            AssetDatabase.SaveAssets(); 

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = miniGame;

            Debug.Log("MiniGame asset created at: " + assetPathAndName);
        }
    }
    
#endif
}