using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance;
    public GameController gameController;
    public Image spinner;

    [SerializeField]
    private GameObject fadePanel;

    [SerializeField]
    private Animator fadeAnim;

    // Use this for initialization
    void Awake()
    {
        MakeSingleton();
        //fadePanel.SetActive(false);
        spinner.gameObject.SetActive(false);
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
        spinner.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        spinner.gameObject.SetActive(false);
        fadeAnim.Play("FadeOutAnim");
        yield return new WaitForSeconds(2f);
        fadePanel.SetActive(false);
    }





}//SceneFader
