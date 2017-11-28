using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingscore : MonoBehaviour {
    GameObject finalscore;
	// Use this for initialization
	void Start () {
        finalscore = GameObject.Find("scorer");
		if (finalscore != null)
		{
			finalscore.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
			finalscore.transform.position = new Vector3(26, -28, -4);
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.LoadLevel("Main Game");
            if(finalscore!=null)
	        DestroyObject(finalscore);
        }
		
	}

}
