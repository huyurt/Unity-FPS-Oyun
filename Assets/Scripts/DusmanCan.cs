using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DusmanCan : MonoBehaviour
{
    public float Can = 100;
    public GameObject Animasyon;
    public float SaldiriMesafesi = 1;
    public float MermiMenzili = 100;
    public Transform AtisNoktasi;
    public GameObject Efekt;
    public float AtisHizi = 1.2f;
    public int Hasar = 10;
    public float DonmeHizi = 20;
    public AudioClip SilahSesi;
    public GameObject Ragdoll;

    float Zaman;
    public int YeniHedef;
    Vector3 Hedef;
    NavMeshAgent Navigasyon;    

    public GameObject Cephaneler;
    public GameObject Medkitler;

    static public int DusmanSayisi = 6;

    void Start()
    {
        Navigasyon = gameObject.GetComponent<NavMeshAgent>();
        Efekt.SetActive(false);
        Animasyon.GetComponent<Animation>()["Disparar"].speed = AtisHizi;
    }

    void Update()
    {
        float Mesafe = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Fps").transform.position);
        Quaternion ObjeAcisi = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Fps").transform.position - transform.position, Vector3.up);

        if (Mesafe >= SaldiriMesafesi)
        {
            Zaman += Time.deltaTime;
            if (Zaman >= YeniHedef)
            {
                float DusmanX = gameObject.transform.position.x;
                float DusmanY = gameObject.transform.position.y;
                float DusmanZ = gameObject.transform.position.z;
                float xPoz = DusmanX + Random.Range(DusmanX - 200, DusmanX + 200);
                float yPoz = DusmanY + Random.Range(DusmanY - 200, DusmanY + 200);
                float zPoz = DusmanZ + Random.Range(DusmanZ - 200, DusmanZ + 200);
                Hedef = new Vector3(xPoz, yPoz, zPoz);
                Navigasyon.SetDestination(Hedef);
                Zaman = 0;
                GetComponent<NavMeshAgent>().speed = 3;
                Animasyon.GetComponent<Animation>().CrossFade("Caminar");
            }
        }
        else if (Mesafe <= SaldiriMesafesi)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, ObjeAcisi, Time.deltaTime * DonmeHizi);
            GetComponent<NavMeshAgent>().speed = 0;
            Atis();
            Animasyon.GetComponent<Animation>().CrossFade("Disparar");
        }
    }

    void HasarVerme(int Hasar)
    {
        Can -= Hasar;
        if (Can <= 0)
        {
            Destroy(gameObject);
            FpsSaglik.SkorSonuc += 10;
            int KitOlusturma = Random.Range(1, 8);
            Vector3 KitKonumu = AtisNoktasi.transform.position;
            KitKonumu.y = 0;
            if (KitOlusturma == 4)
                Instantiate(Cephaneler, KitKonumu, AtisNoktasi.rotation);
            else if (KitOlusturma == 5)
                Instantiate(Medkitler, KitKonumu, AtisNoktasi.rotation);
            ++DusmanSayisi;
        }
    }

    void Atis()
    {
        RaycastHit AtesEtme;
        Vector3 Konum = AtisNoktasi.position;
        Vector3 Yon = transform.TransformDirection(Vector3.forward);

        if (!Animasyon.GetComponent<Animation>().IsPlaying("Disparar"))
        {
            Efekt.SetActive(true);
            Invoke("EfektIptal", 0.04f);
            if (Physics.Raycast(Konum, Yon, out AtesEtme, MermiMenzili))
            {
                if (AtesEtme.collider.tag == "Fps")
                    AtesEtme.collider.SendMessage("HasarAlma", Hasar, SendMessageOptions.DontRequireReceiver);
            }
            transform.GetComponent<AudioSource>().PlayOneShot(SilahSesi);
        }
    }
    void EfektIptal()
    {
        Efekt.SetActive(false);
    }

    /*void OnDrawGizmos()
    {
        if (YolNoktalari.Length > 0) {
            for (int i = 0; i < YolNoktalari.Length; i++)
            {
                if (i > 0)
                {
                    Gizmos.color = new Vector4(0.4f, 0.8f, 2, 0.8f);
                    Gizmos.DrawLine(YolNoktalari[i].position, YolNoktalari[i - 1].position);
                }
            }
        }
    }*/
}