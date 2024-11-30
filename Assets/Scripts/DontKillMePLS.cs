using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillMePLS : MonoBehaviour
{
    private static DontKillMePLS instance;

    void Awake()
    {
        // Sprawdź, czy już istnieje instancja PersistentVolume
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Utrzymaj obiekt między scenami
        }
        else
        {
            Destroy(gameObject); // Usuń duplikaty
        }
    }
}
