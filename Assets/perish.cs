using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perish : MonoBehaviour {

    public float death_timer = 5f;
    private float start_time;

	// Use this for initialization
	void Start () {
        start_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Time.time > start_time + death_timer ) Destroy (this.gameObject);
	}
}
