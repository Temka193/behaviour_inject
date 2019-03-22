using BehaviourInject.Internal;
using UnityEditor;
using UnityEngine;

namespace BInject.Editor
{
	[InitializeOnLoad]
	static class SettingsInit
	{
		private const string RESOURCES_PATH = "Assets/Resources/";
		
		static SettingsInit()
		{
			Settings asset = Resources.Load<Settings>(Settings.SETTINGS_PATH);
			if (asset != null)
			{
				return;
			}

			if (!AssetDatabase.IsValidFolder(RESOURCES_PATH))
			{
				string[] resourcesPath = RESOURCES_PATH.Split('/');
				AssetDatabase.CreateFolder(resourcesPath[0], resourcesPath[1]);
			}
			
			asset = ScriptableObject.CreateInstance<Settings>();
			string path = RESOURCES_PATH + Settings.SETTINGS_PATH + ".asset";
			AssetDatabase.CreateAsset(asset, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}