using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    public GameObject UItoDeactivate;

    private void Start()
    {
        //if(UItoDeactivate != null) StartCoroutine(Waiting(0.6f));
    }
    public void PlayGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator Waiting(float waitSeconds)
    {
        UItoDeactivate.SetActive(false);
        yield return new WaitForSeconds(waitSeconds);
        UItoDeactivate.SetActive(true);
    }

}
