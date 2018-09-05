using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour 
{
	// 에너미를 서버상에서 찍어낸다.
	public GameObject enemyPrefab;
	public int nemberOfEnemies;
	
	public override void OnStartServer()
	{
		for(int i = 0; i < nemberOfEnemies; i++)
		{
			float x = Random.Range(-8f, 8f);
			float z = Random.Range(-8f, 8f);

			Vector3 randomPos = new Vector3(x, 0, z);
			
			float angleY = Random.Range(0, 180f);

			Quaternion randomRoration = Quaternion.Euler(0, angleY, 0);

			GameObject enemyObject = Instantiate(enemyPrefab, randomPos, randomRoration);

			NetworkServer.Spawn(enemyObject);
 		}
	}
}
