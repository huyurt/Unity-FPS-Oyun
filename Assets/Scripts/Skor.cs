using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skor : MonoBehaviour
{
    Collider FPS;
    public Text RekorBilgisi;
    public Text SkorBilgisi;

    void Start()
    {
        SkoruKaydet(FpsSaglik.SkorSonuc);
        RekorBilgisi.text = PlayerPrefs.GetInt("Maksimum Skor").ToString();
        SkorBilgisi.text = PlayerPrefs.GetInt("Son Skor").ToString();
    }

    void Update()
    {

    }

    void SkoruKaydet(int GelenSkor)
    {
        PlayerPrefs.SetInt("Son Skor", FpsSaglik.SkorSonuc);
        int Rekor = PlayerPrefs.GetInt("Maksimum Skor");
        if (GelenSkor > Rekor)
            PlayerPrefs.SetInt("Maksimum Skor", GelenSkor);
    }
}
