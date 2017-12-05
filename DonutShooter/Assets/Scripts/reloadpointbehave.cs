using System.Collections;
using System.Collections.Generic;
using DonutShooter.Base;
using UnityEngine;

public class reloadpointbehave : MonoBehaviour {
    public float lifespan;
    public float movingSpeed;
	public ColorState m_ColorState;


	// Use this for initialization
	void Start ()
	{
		StartCoroutine(SelfDestroy());
	}

	IEnumerator SelfDestroy()
	{
		yield return new WaitForSeconds(lifespan);
		Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(movingSpeed!=0f)
        transform.Translate(0, -movingSpeed, Time.deltaTime);
	}
}
