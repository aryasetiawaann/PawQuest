using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

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
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop the play mode in the editor
#else
            Application.Quit(); // Quit application for builds
#endif
        });
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
