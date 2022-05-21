using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key");
            SceneManager.LoadSceneAsync("Home");
        }
    }
}
