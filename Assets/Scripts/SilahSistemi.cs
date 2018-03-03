using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilahSistemi : MonoBehaviour
{
    public GameObject KanEfekti;
    public GameObject MermiObjesi;
    public int MermiMenzili = 100;

    public int Cephane = 30;
    public int CephaneKapasitesi = 30;
    public int ToplamCephane = 90;
    public int ToplamCephaneKapasitesi = 90;
    public UnityEngine.UI.Text CephaneBilgisi;
    public UnityEngine.UI.Text ToplamCephaneBilgisi;
    
    public float Hasar = 20;

    public Transform Animasyon;
    public float AtesEtmeAnimasyonu = 3;

    public AudioClip SilahSesi;
    public AudioClip ReloadSesi;
    public GameObject Efekt;

    static public int CephaneAl = 0;

    public Camera Kamera;
    public float PosXNormal = 0;
    public float PosYNormal = 0;
    public float PosZNormal = 0;
    public float PosXGorus = 0;
    public float PosYGorus = 0;
    public float PosZGorus = 0;
    float AsagiKonumX = 0;
    float AsagiKonumY = 0;
    float AsagiKonumZ = 0;
    float AsagiAlan = 0;
    float HizX = 0.4f;
    float HizY = 0.4f;
    float HizZ = 0.4f;
    float HizAlani = 0.4f;

    void Start()
    {
        CephaneBilgisi.text = Cephane.ToString();
        ToplamCephaneBilgisi.text = ToplamCephane.ToString();
        Efekt.SetActive(false);
    }

    void Update()
    {
        CephaneBilgisi.text = Cephane.ToString();
        ToplamCephaneBilgisi.text = ToplamCephane.ToString();

        if ((Input.GetButton("Fire1") && CephaneKapasitesi == 30 && Cephane > 0 && !Animasyon.GetComponent<Animation>().IsPlaying("REcargar") && !Animasyon.GetComponent<Animation>().IsPlaying("Disparar")) || (Input.GetMouseButtonDown(0) && CephaneKapasitesi == 13 && Cephane > 0 && !Animasyon.GetComponent<Animation>().IsPlaying("REcargar") && !Animasyon.GetComponent<Animation>().IsPlaying("Disparar")))
        {
            Cephane -= 1;
            transform.GetComponent<AudioSource>().PlayOneShot(SilahSesi);
            Efekt.SetActive(true);

            RaycastHit AtesEtme;
            Vector3 Konum = transform.parent.position;
            Vector3 Yon = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(Konum, Yon, out AtesEtme, MermiMenzili))
            {
                Quaternion Donme = Quaternion.FromToRotation(Vector3.up, AtesEtme.normal);
                if (AtesEtme.collider.tag == "Yeryuzu")
                    Instantiate(MermiObjesi, AtesEtme.point, Donme);

                if (AtesEtme.collider.tag == "Dusman")
                {
                    AtesEtme.collider.SendMessage("HasarVerme", Hasar, SendMessageOptions.DontRequireReceiver);
                    Instantiate(KanEfekti, AtesEtme.point, Donme);
                }
            }
            Animasyon.GetComponent<Animation>()["Disparar"].speed = AtesEtmeAnimasyonu;
            Animasyon.GetComponent<Animation>().Play("Disparar", PlayMode.StopAll);
            Invoke("FX", 0.1f);
        }

        Invoke("Reload", 1);
        if (Animasyon.GetComponent<Animation>().IsPlaying("REcargar") || Animasyon.GetComponent<Animation>().IsPlaying("Disparar") || Animasyon.GetComponent<Animation>().IsPlaying("Aparecer"))
        {
            CephaneBilgisi.text = Cephane.ToString();
            ToplamCephaneBilgisi.text = ToplamCephane.ToString();
        }

        if(CephaneAl >= 1)
        {
            int EklenecekCephane = CephaneKapasitesi * CephaneAl;
            int GerekenCephane = ToplamCephaneKapasitesi - ToplamCephane;
            CephaneAl = 0;
            if (EklenecekCephane > GerekenCephane)
                ToplamCephane = ToplamCephaneKapasitesi;
            else
                ToplamCephane += EklenecekCephane;
            CephaneBilgisi.text = Cephane.ToString();
            ToplamCephaneBilgisi.text = ToplamCephane.ToString();
        }

        float YeniKonumX = Mathf.SmoothDamp(Animasyon.transform.localPosition.x, AsagiKonumX, ref HizX, .04f);
        float YeniKonumY = Mathf.SmoothDamp(Animasyon.transform.localPosition.y, AsagiKonumY, ref HizY, .04f);
        float YeniKonumZ = Mathf.SmoothDamp(Animasyon.transform.localPosition.z, AsagiKonumZ, ref HizZ, .04f);
        float YeniAlan = Mathf.SmoothDamp(Kamera.fieldOfView, AsagiAlan, ref HizAlani, .04f);
        Animasyon.transform.localPosition = new Vector3(YeniKonumX, YeniKonumY, YeniKonumZ);
        Kamera.fieldOfView = YeniAlan;
                
        if (Input.GetButton("Fire2"))
        {
            AsagiKonumX = PosXGorus;
            AsagiKonumY = PosYGorus;
            AsagiKonumZ = PosZGorus;
            AsagiAlan = 40;
        }
        else
        {
            AsagiKonumX = PosXNormal;
            AsagiKonumY = PosYNormal;
            AsagiKonumZ = PosZNormal;
            AsagiAlan = 60;
        }
    }

    void Reload()
    {
        int YuklenecekCephane = CephaneKapasitesi - Cephane;            
        if (Cephane == 0 && Input.GetButtonUp("Fire1") && YuklenecekCephane > 0 && ToplamCephane > 0)
        {
            Animasyon.GetComponent<Animation>().Play("REcargar", PlayMode.StopAll);
            transform.GetComponent<AudioSource>().PlayOneShot(ReloadSesi);
            if (YuklenecekCephane <= ToplamCephane)
            {
                Cephane += YuklenecekCephane;
                ToplamCephane -= YuklenecekCephane;
                CephaneBilgisi.text = Cephane.ToString();
                ToplamCephaneBilgisi.text = ToplamCephane.ToString();
            }
            else
            {
                Cephane += ToplamCephane;
                ToplamCephane = 0;
                CephaneBilgisi.text = Cephane.ToString();
                ToplamCephaneBilgisi.text = ToplamCephane.ToString();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && YuklenecekCephane > 0 && ToplamCephane > 0 && YuklenecekCephane != CephaneKapasitesi)
        {
            Animasyon.GetComponent<Animation>().Play("REcargar", PlayMode.StopAll);
            transform.GetComponent<AudioSource>().PlayOneShot(ReloadSesi);
            if (YuklenecekCephane <= ToplamCephane)
            {
                Cephane += YuklenecekCephane;
                ToplamCephane -= YuklenecekCephane;
                CephaneBilgisi.text = Cephane.ToString();
                ToplamCephaneBilgisi.text = ToplamCephane.ToString();
            }
            else
            {
                Cephane += ToplamCephane;
                ToplamCephane = 0;
                CephaneBilgisi.text = Cephane.ToString();
                ToplamCephaneBilgisi.text = ToplamCephane.ToString();
            }
        }
    }

    void FX()
    {
        Efekt.SetActive(false);
    }    
}
