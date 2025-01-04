using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton; // Correct attribute name
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1); // Load scene with index 1
        });

        exitButton.onClick.AddListener(() =>
        {
            QuitApplication(); // Memanggil fungsi quit yang universal
        });
    }

    private void QuitApplication()
    {
        // Quit aplikasi, tergantung platform
        if (Application.isEditor)
        {
            Debug.Log("Quit is not supported in editor mode. Stopping play mode instead.");
            // Debug.Log saja jika sedang di editor
        }
        else
        {
            Application.Quit(); // Quit untuk build
        }
    }

    // Remove PlayClick if not needed or add logic if it will be used
    private void PlayClick()
    {
    }

    void Start()
    {
        // Pastikan kursor terlihat dan tidak terkunci
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
    }
}
