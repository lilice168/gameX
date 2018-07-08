using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class BuildBundle : MonoBehaviour {

	private const string AssetBundlePath = "AssetBundle";

	[MenuItem("打包資源/Windows/Prefabs")]
	static void BuildWindow()
	{
		BulidPrefab();
	}

	[MenuItem("打包資源/Android/Prefabs")]
	static void BuildAndroid()
	{
		BulidPrefab(BuildTarget.Android);
	}

	[MenuItem("打包資源/iOS/Prefabs")]
	static void BuildIOS()
	{
		BulidPrefab(BuildTarget.iOS);
	}



	static void BulidPrefab(BuildTarget _tartget = BuildTarget.StandaloneOSXIntel64)
	{
		if (!Directory.Exists(AssetBundlePath))
		{
			Directory.CreateDirectory(AssetBundlePath);
		}

		Debug.LogFormat("Build Bundle in " + _tartget);

		BulidBundle(_tartget, "characterbundle", "Assets/Prefabs/Character");
		BulidBundle(_tartget, "gamebundle", "Assets/Prefabs/Game");
		BulidBundle(_tartget, "mapbundle", "Assets/Prefabs/Map");
		BulidBundle(_tartget, "uibundle", "Assets/Prefabs/UI");
	}

	static void BulidBundle(BuildTarget _tartget, string bundlename, string path)
	{
		//Caching.CleanCache ();
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

		buildMap[0].assetBundleName = bundlename;

		DirectoryInfo dir = new DirectoryInfo(path);
		FileInfo[] info = dir.GetFiles("*.*", SearchOption.AllDirectories);

		int assetNumber = info.Length;
		if(assetNumber <= 0){
			return;
		}

		int count = 0;
		string[] characterAssets = new string[assetNumber];
		foreach (FileInfo file in info) {

			if(file.Name.Contains(".meta") == true){
				continue;
			}

			int assetPathIndex = file.FullName.IndexOf("Assets");
			string sourcePath = file.FullName.Substring(assetPathIndex);
			Object obj = AssetDatabase.LoadAssetAtPath(sourcePath, typeof(GameObject));
			if(obj == null){
				continue;
			}

			characterAssets[count] = sourcePath;
			count++;
		}

		buildMap[0].assetNames = characterAssets;
		BuildPipeline.BuildAssetBundles(AssetBundlePath, buildMap, BuildAssetBundleOptions.None, _tartget);
	}
}
