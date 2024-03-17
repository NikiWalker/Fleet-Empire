using System;
using Rook;
using UnityEngine;

/// <summary>
/// Main game flow manager for Fleet & Empire.
/// Start here!
/// </summary>
public class Game : MonoBehaviour
{
	void Awake()
	{
		RookSingleton<Game>.I = this;
	}

	void OnApplicationPause(bool isPaused)
	{
		if(isPaused) 
			RookPrefs.SaveAnyChanges();
	}	
	
	void OnDestroy()
	{
		RookPrefs.SaveAnyChanges();
	}
}