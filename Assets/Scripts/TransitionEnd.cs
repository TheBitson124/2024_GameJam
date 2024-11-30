using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private OneSceneManager _manager;
    public void OnTriggerEnter2D(Collider2D other)
    {
        _manager.NextScene();
    }
}
