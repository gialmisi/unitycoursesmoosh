using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 1f;
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip victoryTune;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem victoryParticle;

    AudioSource audioSource;

    public bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("Collided with fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashClip);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", crashDelay);
    }

    void StartNextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(victoryTune);
        victoryParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
