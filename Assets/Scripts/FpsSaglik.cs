using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FpsSaglik : MonoBehaviour {

    public int Saglik = 100;
    public int SaglikKapasitesi = 100;
    static public int SaglikAl = 0;
    public UnityEngine.UI.Text SaglikBilgisi;
    public GameObject SaglikAz;

    public Transform SpawnYeri;
    public bool Olu;
    public AudioClip SpawnSesi;
    public AudioClip OlumSesi;
    public GameObject RespawnButonu;
    public GameObject OlumKamerasi;
    public string SahneAdi;

    Collider FPS;
    public UnityEngine.UI.Text SkorBilgisi;
    static public int SkorSonuc = 0;

    Vector3 DusmanSpawnYeri;
    public GameObject DusmanObjesi;

    void Start()
    {
        SaglikAz.SetActive(false);
        OlumKamerasi.SetActive(false);
        gameObject.SetActive(true);
        RespawnButonu.SetActive(false);
        Olu = false;
        SaglikBilgisi.text = Saglik.ToString();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SahneAdi);
        }

        SkorBilgisi.text = SkorSonuc.ToString();
        while (DusmanCan.DusmanSayisi > 0)
        {
            int RastgeleSayi = Random.Range(1, 4);
            if (RastgeleSayi == 2)
            {
                DusmanSpawnYeri.x = Random.Range(85, 86);
                DusmanSpawnYeri.z = Random.Range(79, 80);
            }
            else if (RastgeleSayi == 3)
            {
                DusmanSpawnYeri.x = Random.Range(85, 86);
                DusmanSpawnYeri.z = Random.Range(20, 21);
            }
            else
            {
                DusmanSpawnYeri.x = Random.Range(14, 15);
                DusmanSpawnYeri.z = Random.Range(79, 80);
            }
            DusmanSpawnYeri.y = 0.3f;
            Instantiate(DusmanObjesi, DusmanSpawnYeri, Quaternion.identity);
            --DusmanCan.DusmanSayisi;
        }

        if (SaglikAl > 0)
        {
            int EklenecekSaglik = SaglikAl;
            int GerekenSaglik = SaglikKapasitesi - Saglik;
            SaglikAl = 0;
            if (EklenecekSaglik > GerekenSaglik)
                Saglik = SaglikKapasitesi;
            else
                Saglik += EklenecekSaglik;
            SaglikBilgisi.text = Saglik.ToString();            
        }
    }

    /*public void Respawn()
    {
        AudioSource RespawnSesi = GetComponent<AudioSource>();
        gameObject.SetActive(true);
        OlumKamerasi.SetActive(false);

        if (Olu == true)
        {
            RespawnSesi.PlayOneShot(SpawnSesi);
            Olu = false;
        }
        RespawnButonu.SetActive(false);
        Saglik = 100;
        SaglikBilgisi.text = Saglik.ToString();
    }*/

    void HasarAlma(int Hasar)
    {
        AudioSource OlmeSesi = GetComponent<AudioSource>();
        Saglik -= Hasar;
        SaglikBilgisi.text = Saglik.ToString();

        if (Saglik > 20)
            SaglikAz.SetActive(false);

        if (Saglik > 0 && Saglik <= 20)
            SaglikAz.SetActive(true);       

        if (Saglik <= 0)
        {
            SceneManager.LoadScene(SahneAdi);
            Saglik = 0;
            SaglikBilgisi.text = Saglik.ToString();
            SaglikAz.SetActive(false);
            DusmanCan.DusmanSayisi = 6;
            OlmeSesi.PlayOneShot(OlumSesi);
            RespawnButonu.SetActive(false);
            OlumKamerasi.SetActive(true);
            gameObject.transform.position = SpawnYeri.position;
            Olu = true;
        }
    }
}
