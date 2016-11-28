using UnityEngine;

public class SpawnScript : MonoBehaviour
{
	public GameObject enemy;			
	public Transform target;			
	public float spawnInterval = 3f;	

	float spawnVariance;			

	void Start () 
	{
	spawnVariance = spawnInterval * .5f;
		Invoke ("Spawn", spawnInterval + Random.Range(-spawnVariance, spawnVariance));
	}

	void Update()
	{
		if (spawnInterval > 1f)
		{
			float timeReduction = Time.deltaTime / 50;

			spawnInterval = Mathf.Max(1f, spawnInterval - timeReduction);
			spawnVariance = spawnInterval * .5f;
		}
	}

	void Spawn()
	{
        GameObject enemyObj = Instantiate (enemy, transform.position, transform.rotation) as GameObject;
        enemyObj.transform.parent = transform;

		enemyObj.GetComponent<EnemyNavigation> ().target = target;
		Invoke("Spawn", spawnInterval + Random.Range(-spawnVariance, spawnVariance));
	}
}
