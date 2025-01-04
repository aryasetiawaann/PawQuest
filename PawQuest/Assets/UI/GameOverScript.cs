using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    [SerializeField] private Button restartButton; // Correct attribute name
    [SerializeField] private Button menuButton;
    // Start is called before the first frame update
    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1); // Load scene with index 1
        });

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
