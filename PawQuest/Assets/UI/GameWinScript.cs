using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinScript : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    // Start is called before the first frame update
    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }



    void Start()
    {
        // Pastikan kursor terlihat dan tidak terkunci
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
