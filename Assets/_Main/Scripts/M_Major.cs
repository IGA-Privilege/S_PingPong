using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_Major : MonoBehaviour
{
    public static M_Major Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene(3);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SceneManager.LoadScene(0);
    }
}
