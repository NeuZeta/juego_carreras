using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance;
    public GameController gameController;

    [SerializeField]
    private GameObject fadePanel;

    [SerializeField]
    private Animator fadeAnim;

    // Use this for initialization
    void Awake()
    {
        MakeSingleton();
        //fadePanel.SetActive(false);
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(FadeInOut(level));
    }

    IEnumerator FadeInOut (string level)
    {
        fadePanel.SetActive(true);
        fadeAnim.Play("FadeInAnim");

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
        yield return new WaitForSeconds(2f);
        fadeAnim.Play("FadeOutAnim");
        yield return new WaitForSeconds(2f);
        fadePanel.SetActive(false);
    }





}//SceneFader
