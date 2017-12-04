using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_spawner : MonoBehaviour {
	public float spawnGap;
    private int spawntimer;

	public Transform[] spawnpoints;
	public GameObject[] monsters;
		
	// Use this for initialization
	void Start () {
        //initial enemies 
        //Vector3 newpos = new Vector3(spawnDis, Random.Range(spawnminy, spawnmaxy), 0);
        //Instantiate(monster, newpos, Quaternion.identity);
		SpawnEnemy();
	}
	
	IEnumerator SpawnWave()
	{
		while (true)
		{
			int num = Random.Range(1, spawnpoints.Length);
			List<int> spawnedslot=new List<int>();
			for (int i = 0; i < num; i++)
			{
				int type = Random.Range(0, monsters.Length);
				int position = -1;
				while (position==-1||spawnedslot.Contains(position))
				{
					position=Random.Range(0, spawnpoints.Length);
				}
				Vector3 newpos = spawnpoints[position].position;
				spawnedslot.Add(position);
				//Debug.Log("type:"+type+" position:"+position+" num i: "+ i);
				Instantiate(monsters[type], newpos, Quaternion.identity);
			}
			spawnedslot.Clear();
			yield return new WaitForSeconds(spawnGap);
		}
	}

	public void SpawnEnemy()
	{
		StartCoroutine(SpawnWave());
	}

	/*
	// Update is called once per frame
	void Update () {
        spawntimer += 1;
        if (spawntimer % spawnrate == 0)
        {
	        int type = Random.Range(0, monsters.Length);
            Vector3 newpos = new Vector3(spawnDis, Random.Range(spawnminy, spawnmaxy), 0);
            Instantiate(monsters[type], newpos, Quaternion.identity);
        }
		
	}*/

}
