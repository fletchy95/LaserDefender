using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FinalScore : MonoBehaviour 
{
	void Start () 
	{
		Text myText = GetComponent<Text>();
		myText.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset();
	}
}