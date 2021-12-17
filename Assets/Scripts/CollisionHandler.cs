using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelDelay = 0;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update() 
    {
      ResponseToDebug();
    }

    void ResponseToDebug()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        }

        else if(Input.GetKey(KeyCode.C))
        {
          Debug.Log("Cheat was activated!");
          collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
      if(isTransitioning || collisionDisabled) return;
   
        switch(other.gameObject.tag)
        {
            case "Friendly":
              Debug.Log("This is Friednly");
              break;
            case "Finish":
              FinishingLevel();
              break;
            default:
              StartCrashSequence();
              break;

        }
    }
    void FinishingLevel()
    {
      isTransitioning = true;
      audioSource.Stop();
      audioSource.PlayOneShot(success);
      successParticle.Play();
      GetComponent<Movement>().enabled = false;
      Invoke("LoadNextScene",levelDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("StartAgain", levelDelay);
        
      
    }

    void StartAgain()
    {
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene()
    {
      int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
      int nextSceneIndex = currentSceneIndex + 1;
      if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
      {
        nextSceneIndex = 0;
      }
      SceneManager.LoadScene(nextSceneIndex);
    }


}
