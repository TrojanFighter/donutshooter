using System.Collections;
using System.Collections.Generic;
using DonutShooter.Base;
using UnityEngine;

public class reloadpointbehave : MonoBehaviour {
    public float lifespan;
    public float movingSpeed;
	public ColorState m_ColorState;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -movingSpeed, Time.deltaTime);
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
