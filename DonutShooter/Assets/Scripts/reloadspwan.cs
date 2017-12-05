using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadspwan : MonoBehaviour {
    public int spawnRate;
    private float spawnTimer;
    private int chancer;
    private int chancer2;

    public GameObject[] reloadprefabs;




    // Update is called once per frame
	void Update () {
        spawnTimer += 1;
        if (Time.frameCount % spawnRate == 0)
        {
            chancer = Random.Range(0, 4);
            if (chancer==1)
            {
                Instantiate(reloadprefabs[1], transform.position, Quaternion.identity);

            }
            else if (chancer == 2)
            {
                Instantiate(reloadprefabs[2], transform.position, Quaternion.identity);

            }
            else if (chancer== 3)
            {
                Instantiate(reloadprefabs[3], transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(reloadprefabs[0], transform.position, Quaternion.identity);
            }
          
        }

        
	}
}
