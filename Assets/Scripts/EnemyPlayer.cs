using UnityEngine;
using System.Collections;
public class EnemyPlayer : MonoBehaviour 
{
	public float health = 100f;
	public int scoreValue = 50;
	private ScoreKeeper scoreKeeper;
	void OnTriggerEnter2D(Collider2D col)
	{
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			health -= missile.GetDamage();
			missile.LaserDestroy();
			if(health <= 0)
			{
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
	void Start () 
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
}