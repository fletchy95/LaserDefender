using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour 
{
	public void LoadLevel(string name)
	{
		Application.LoadLevel(name);
	}
	public void QuitRequest()
	{
		Application.Quit();
	}
	public void ReturnToStart()
	{
		Application.LoadLevel("Start");
	}
	public void LoadNextLevel()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	public void LevelSelector()
	{
		Application.LoadLevel("LevelSelector");
	}
	public void LevelManagerSelector(string level)
	{
		Application.LoadLevel (level);
	}
}