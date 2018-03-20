using UnityEngine;
using System.Collections;
public class EnemyT2 : MonoBehaviour 
{
	public float laserSpeed;
	public GameObject enemyLaser;
	public float fireRate = 10f;
	public float shotsPerSec = 0.5f;
	public int scoreValue = 150;
	public float health = 200f;
	public float damage = 100f;
	public AudioClip laserFire;
	public AudioClip shipDestroy;
	private ScoreKeeper scoreKeeper;
	void Start () 
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	void Update () 
	{
		float probability = shotsPerSec * Time.deltaTime;
		if(Random.value < probability)
		{
			Fire ();
		}
	}
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0 , -0.65f , 0);
		GameObject laser = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0 , -laserSpeed , 0);
		AudioSource.PlayClipAtPoint(laserFire , transform.position);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			health -= missile.GetDamage();
			missile.LaserDestroy();
			if(health <= 0)
			{
				AudioSource.PlayClipAtPoint(shipDestroy , transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
}