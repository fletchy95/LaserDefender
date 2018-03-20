using UnityEngine;
using System.Collections;
public class EnemyT3 : MonoBehaviour 
{
	public float laserSpeed;
	public GameObject enemyLaser;
	public float fireRate = 10f;
	public float shotsPerSec = 0.5f;
	public int scoreValue = 250;
	public float health = 300f;
	public float damage = 50f;
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
		GameObject laser1 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser2 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser3 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser4 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser5 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser6 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser7 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser8 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser9 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser10 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser11 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		GameObject laser12 = Instantiate(enemyLaser , startPosition , Quaternion.identity) as GameObject;
		laser1.GetComponent<Rigidbody2D>().velocity = new Vector3(laserSpeed , -laserSpeed , 0);
		laser2.GetComponent<Rigidbody2D>().velocity = new Vector3(-laserSpeed , -laserSpeed , 0);
		laser3.GetComponent<Rigidbody2D>().velocity = new Vector3(0 , -laserSpeed , 0);
		laser4.GetComponent<Rigidbody2D>().velocity = new Vector3(laserSpeed/2 , -laserSpeed , 0);
		laser5.GetComponent<Rigidbody2D>().velocity = new Vector3(-laserSpeed/2 , -laserSpeed , 0);
		laser6.GetComponent<Rigidbody2D>().velocity = new Vector3(-laserSpeed , 0 , 0);
		laser7.GetComponent<Rigidbody2D>().velocity = new Vector3(laserSpeed , 0 , 0);
		laser8.GetComponent<Rigidbody2D>().velocity = new Vector3(-laserSpeed , laserSpeed , 0);
		laser9.GetComponent<Rigidbody2D>().velocity = new Vector3(laserSpeed , laserSpeed , 0);
		laser10.GetComponent<Rigidbody2D>().velocity = new Vector3(0 , laserSpeed , 0);
		laser11.GetComponent<Rigidbody2D>().velocity = new Vector3(-laserSpeed/2 , laserSpeed , 0);
		laser12.GetComponent<Rigidbody2D>().velocity = new Vector3(laserSpeed/2 , laserSpeed , 0);
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