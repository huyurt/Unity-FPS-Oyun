using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public string SahneAdi;

    bool CursorLockedVar;
        
    Collider FSP;
        
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
        CursorLockedVar = (true);
    }

    void Update()
    {           
        if (Input.GetKeyDown("escape") && !CursorLockedVar)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
            CursorLockedVar = (true);
        }
        else if (Input.GetKeyDown("escape") && CursorLockedVar)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            CursorLockedVar = (false);
        }
    }

    public void OyunuBaslat()
    {
        
        SceneManager.LoadScene(SahneAdi);
        DusmanCan.DusmanSayisi = 5;
        FpsSaglik.SkorSonuc = 0;
    }

    public void OyunuKapat()
    {
        PlayerPrefs.SetInt("Son Skor", 0);
        Application.Quit();
    }
}
