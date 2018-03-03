using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CephaneAl : MonoBehaviour {
    public int CephanePaketi = 1;
    public AudioClip Ses;
    public bool Alindi;
    public float YokOlmaSuresi = 1;

    void Start()
    {
        Alindi = false;
    }

    void OnTriggerEnter(Collider FPS)
    {
        if (FPS.gameObject.tag == "Fps" && Alindi == false)
        {
            SilahSistemi.CephaneAl = CephanePaketi;
            transform.GetComponent<AudioSource>().PlayOneShot(Ses);
            Destroy(gameObject, YokOlmaSuresi);
            Alindi = true;
        }
    }
}
