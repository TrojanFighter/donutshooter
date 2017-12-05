using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<zombie>())
    {// game should be over by now 
        Application.LoadLevel("ending");
    }
      if (other.GetComponent<donut>())
      {// game should be over by now 
          other.GetComponent<donut>().SelfDestroy();
      }
  }
}
