using BehaviourInject;
using BehaviourInject.Internal;
using UnityEditor;
using UnityEngine;

namespace BInject.Editor
{
	[InitializeOnLoad]
	static class SettingsInit
	{
		private const string RESOURCES_PATH = "Assets/Resources";
		private const string INJECTOR_PATH = "Packages/com.sergeysychov.behaviour_inject/Scripts/BehaviourInject/Injector.cs";
		
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
			string path = RESOURCES_PATH + "/" + Settings.SETTINGS_PATH + ".asset";
			AssetDatabase.CreateAsset(asset, path);

			//Setup injector script execution order
			MonoScript injector = AssetDatabase.LoadAssetAtPath<MonoScript>(INJECTOR_PATH);
			int currentExecutionOrder = MonoImporter.GetExecutionOrder(injector);
			if (currentExecutionOrder != -100)
			{
				MonoImporter.SetExecutionOrder(injector, -100);
			}
			
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}