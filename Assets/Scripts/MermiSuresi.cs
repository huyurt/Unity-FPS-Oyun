using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiSuresi : MonoBehaviour {
    public double Zaman = 0.0;
    public double MaksimumZaman = 5.0;

    void Start () {
		
	}

    void Update()
    {
        Zaman += Time.deltaTime;
        if (Zaman >= MaksimumZaman)
            Destroy(gameObject);
    }
}
