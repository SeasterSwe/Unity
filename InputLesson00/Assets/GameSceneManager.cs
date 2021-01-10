using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public GameObject animationObj;

    public void LoadSceneWithTransition(string scene)
    {
        StartCoroutine(SwapWithAnimation(scene, 1f));
    }
    IEnumerator SwapWithAnimation(string scene, float transitionSpeed)
    {
        if (animationObj != null)
        {
            animationObj.GetComponent<Animator>().SetTrigger("Start");
            yield return new WaitForSeconds(transitionSpeed);
            SceneManager.LoadScene(scene);
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}
