using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAl : MonoBehaviour
{
    public int CanAl2 = 20;
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
            FpsSaglik.SaglikAl = CanAl2;
            transform.GetComponent<AudioSource>().PlayOneShot(Ses);
            Destroy(gameObject, YokOlmaSuresi);
            Alindi = true;
        }
    }
}