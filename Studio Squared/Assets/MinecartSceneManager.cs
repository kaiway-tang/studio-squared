using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartSceneManager : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] float sceneDuration;
    [SerializeField] ParticleSystem[] sparks;
    [SerializeField] int timerMin, timerMax;
    int[] timers;
    // Start is called before the first frame update
    void Start()
    {
        Player.self.SetFrozen(true);
        Player.self.SetFacing(MobileEntity.LEFT);
        Invoke("LoadNextScene", sceneDuration);
        timers = new int[3];

        for (int i = 0; i < timers.Length; i++)
        {

        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < timers.Length; i++) { timers[i]--; }

        if (timers[2] < 1)
        {
            timers[2] = Random.Range(timerMin, timerMax);
            CameraController.SetTrauma(10);
        }

        for (int i = 0; i < 2; i++)
        {
            if (timers[i] < 1)
            {
                timers[i] = Random.Range(timerMin, timerMax);
                sparks[i].Play();
            }
        }
    }

    // Update is called once per frame
    void LoadNextScene()
    {
        GameManager.LoadScene(nextScene);
    }
}
