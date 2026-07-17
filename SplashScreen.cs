using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, true);
        StartCoroutine("SplashScreenCoroutine");
    }

    private IEnumerator SplashScreenCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}
