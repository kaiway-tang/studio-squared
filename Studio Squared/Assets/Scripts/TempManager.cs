using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] string scene;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.spawnPosition = GameManager.playerTrfm.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(enemy, Vector3.zero, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(enemy, Vector3.zero + Vector3.right * (i - 2) * 3, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.ResetGameVariables(false);
            SceneManager.LoadScene(scene);
        }
    }
}
