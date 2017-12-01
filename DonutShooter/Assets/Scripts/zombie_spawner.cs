using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_spawner : MonoBehaviour {
    public float spawnmaxy;
    public float spawnminy;
    public float spawnDis;
    public int spawnrate;
    private int spawntimer;

	public Transform[] spawnpoints;
	public GameObject[] monsters;
		
	// Use this for initialization
	/*void Start () {
        //initial enemies 
        Vector3 newpos = new Vector3(spawnDis, Random.Range(spawnminy, spawnmaxy), 0);
        Instantiate(monster, newpos, Quaternion.identity);
    }*/
	
	// Update is called once per frame
	void Update () {
        spawntimer += 1;
        if (spawntimer % spawnrate == 0)
        {
	        int type = Random.Range(0, monsters.Length);
            Vector3 newpos = new Vector3(spawnDis, Random.Range(spawnminy, spawnmaxy), 0);
            Instantiate(monsters[type], newpos, Quaternion.identity);
        }
		
	}

}
