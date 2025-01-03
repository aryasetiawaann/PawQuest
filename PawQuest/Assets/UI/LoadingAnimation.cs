using System.Collections;
using UnityEngine;
using TMPro; // Untuk TextMeshPro

public class LoadingAnimation : MonoBehaviour
{
    public TextMeshProUGUI loadingText; // Referensi ke TextMeshPro Text
    private string baseText = "Loading"; // Teks dasar
    private int dotCount = 0; // Jumlah titik saat ini
    private const int maxDots = 3; // Maksimal titik (3 titik)

    void Start()
    {
        // Mulai Coroutine animasi loading
        StartCoroutine(AnimateLoading());
    }

    IEnumerator AnimateLoading()
    {
        while (true)
        {
            // Tambahkan titik sesuai jumlah dotCount
            loadingText.text = baseText + new string('.', dotCount);

            // Tambah jumlah titik (max 3)
            dotCount = (dotCount + 1) % (maxDots + 1);

            // Tunggu 0.5 detik sebelum update animasi
            yield return new WaitForSeconds(0.5f);
        }
    }
}
