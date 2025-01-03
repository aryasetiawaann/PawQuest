using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarHero : MonoBehaviour
{
    public Slider healthSlider;
    private HeroAttribute hero;

    void Start()
    {
        hero = FindObjectOfType<HeroAttribute>();

        if (hero != null)
        {
            healthSlider.maxValue = hero.maxHealth;
            healthSlider.value = hero.currentHealth;

            // Daftarkan event OnHealthChanged
            hero.OnHealthChanged += UpdateHealthBar;
        }
    }

    void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    private void OnDestroy()
    {
        if (hero != null)
        {
            // Hapus pendaftaran event saat objek dihancurkan
            hero.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
