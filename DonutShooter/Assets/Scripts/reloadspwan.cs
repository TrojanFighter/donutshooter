using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadspwan : MonoBehaviour {
    public int spawnRate;
    private float spawnTimer;
    private int chancer;
    private int chancer2;
    private int red;
    private int green;
    private int blue;
    public GameObject reload1;
    public GameObject reload2;
    public GameObject reload3;


	// Use this for initialization
	void Start () {     
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += 1;
        if (Time.frameCount % spawnRate == 0)
        {
            chancer = Random.Range(0, 3);
            if (chancer < 1 )
            {
                Instantiate(reload1, transform.position, Quaternion.identity);
                red += 1;
                green = 0;
                blue = 0;
                

            }
            if (chancer < 2 && chancer >= 1)
            {
                Instantiate(reload2, transform.position, Quaternion.identity);
                green += 1;
                red = 0;
                blue = 0;

            }
            if (chancer<3 && chancer >= 2)
            {
                Instantiate(reload3, transform.position, Quaternion.identity);
                blue += 1;
                red = 0;
                green = 0;

            }
          
        }

        
	}
}
