using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        // Tunggu selama 2 detik
        yield return new WaitForSeconds(3f);

        // Ganti scene ke Dungeon Lvl 1
        SceneManager.LoadScene("Dungeon Lvl 1");
    }
}
