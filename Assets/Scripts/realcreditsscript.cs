using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realcreditsscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject targetObject;

    // Update is called once per frame
    private void Start() {
        targetObject.SetActive(false);
    }

    void Update()
    {
        // Sprawdź, czy klawisz C został naciśnięty
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Przełącz widoczność obiektu
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
            else
            {
                Debug.LogWarning("Target object is not assigned!");
            }
        }
    }
}
