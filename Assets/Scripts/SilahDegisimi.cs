using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilahDegisimi : MonoBehaviour {
    public GameObject[] Silahlar;
    public int Silah1 = 1;
    public int Silah2 = 0;
    public int HangiSilah = 1;
    public Animator Animasyon;

	void Start () {
        HangiSilah = 1;
        Silahlar[Silah1].gameObject.SetActive(true);
        Silahlar[Silah2].gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("SilahDegistir") && !Input.GetButton("Fire1") && !Input.GetButton("Fire2") && HangiSilah == 1)
        {
            Animasyon.SetBool("Degisim", true);
            Invoke("Silah2Degistir", 0.3f);
        }

        if (Input.GetButtonDown("SilahDegistir") && !Input.GetButton("Fire1") && !Input.GetButton("Fire2") && HangiSilah == 2)
        {
            Animasyon.SetBool("Degisim", true);
            Invoke("Silah1Degistir", 0.3f);
        }

    }
    void Silah1Degistir()
    {
        HangiSilah = 1;
        Silahlar[Silah1].gameObject.SetActive(true);
        Silahlar[Silah2].gameObject.SetActive(false);
        Animasyon.SetBool("Degisim", false);
    }

    void Silah2Degistir()
    {
        HangiSilah = 2;
        Silahlar[Silah1].gameObject.SetActive(false);
        Silahlar[Silah2].gameObject.SetActive(true);
        Animasyon.SetBool("Degisim", false);
    }
}
