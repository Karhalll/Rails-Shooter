using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")]
    [SerializeField] float levelDelay = 1f;
    [Tooltip("FX prefab on player")]
    [SerializeField] GameObject deathFX = null;

    private void OnTriggerEnter(Collider other) 
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        if (deathFX != null)
        {
            deathFX.SetActive(true);
        }
        Invoke("ReloadScene", levelDelay);
    }

    private void ReloadScene() // string reference
    {
        SceneManager.LoadScene(1);
    }
}
