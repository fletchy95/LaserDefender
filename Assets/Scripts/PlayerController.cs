using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour 
{
	public float speed = 10f;
	public float xPadding = 1f;
	public float yPadding = 1f;
	public float laserSpeed = 8f;
	public float fireRate = 0.2f;
	public GameObject playerLaser;
	public float health = 250f;
	public AudioClip laserFire;
	public AudioClip shipDestroy;
	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;
	private int laserValue = -1;
	private int destroyedValue = -1000;
	private ScoreKeeper scoreKeeper;
	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0 , 0 , distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1 , 0 , distance));
		Vector3 upMost = Camera.main.ViewportToWorldPoint(new Vector3(0 , 1 , distance));
		Vector3 downMost = Camera.main.ViewportToWorldPoint(new Vector3(0 , 0 , distance));
		xMin = leftMost.x + xPadding;
		xMax = rightMost.x - xPadding;
		yMin= downMost.y + yPadding;
		yMax = upMost.y - yPadding;
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	void Update () 
	{
		Controller ();
		// --- Restricts player to the game space ---
		float newX = Mathf.Clamp(transform.position.x , xMin , xMax);
		float newY = Mathf.Clamp(transform.position.y , yMin , yMax);
		transform.position = new Vector3(newX , transform.position.y , transform.position.z);
		transform.position = new Vector3(transform.position.x , newY , transform.position.z);
		// --- ---
	}
	void Controller()
	{
		if(Input.GetKey (KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire" , 0.000001f , fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
	}
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0 , .65f , 0);
		GameObject laser = Instantiate(playerLaser , startPosition , Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0 , laserSpeed , 0);
		scoreKeeper.Score(laserValue);
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
				Die ();
				scoreKeeper.Score(destroyedValue);
			}
		}
	}
	void Die()
	{
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose");
		Destroy (gameObject);
	}
}