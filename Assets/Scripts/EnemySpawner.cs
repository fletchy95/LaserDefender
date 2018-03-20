using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour 
{
	public GameObject[] enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float xPadding = -0.55f;
	public float spawnDelay = 0.5f;
	private int enemyTFighter = 0;
	private bool direction = true; // True = Right , False = Left
	private float xMin;
	private float xMax;
	private ScoreKeeper scoreKeeper;
	private int spawnChoice = 0;
	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0 , 0 , distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1 , 0 , distance));
		xMin = leftMost.x + xPadding;
		xMax = rightMost.x - xPadding;
		SpawnUntilFull();
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	void Update () 
	{
		if(direction)
		{
			transform.position += Vector3.right * Time.deltaTime * speed;
		}
		else
		{
			transform.position += Vector3.left * Time.deltaTime * speed;
		}
		float rightEdgeForm = transform.position.x + (.5f * width);
		float leftEdgeForm = transform.position.x - (.5f * width);
		if(rightEdgeForm >= xMax)
		{
			direction = false;
		}
		else if(leftEdgeForm <= xMin)
		{
			direction = true;
		}
		if(AllMembersDead())
		{
			SpawnUntilFull();
		}
		ChooseTFighter (ScoreKeeper.score);
	}
	void ChooseTFighter(int spawnChoice)
	{
		if(spawnChoice > 1000 && spawnChoice < 3000)
		{
			enemyTFighter = 1;
		}
		else if(spawnChoice >= 3000)
		{
			enemyTFighter = 2;
		}
		else
		{
			enemyTFighter = 0;
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position , new Vector3(width , height));
	}
	bool AllMembersDead()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if(childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
	Transform NextFreePosition()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if(childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}
	void SpawnUntilFull()
	{
		Transform nxtPosition = NextFreePosition();
		if(nxtPosition != null)
		{
			GameObject enemy = Instantiate(enemyPrefab[enemyTFighter] , nxtPosition.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = nxtPosition;
		}
		if(nxtPosition)
		{
			Invoke ("SpawnUntilFull" , spawnDelay);
		}
	}
}