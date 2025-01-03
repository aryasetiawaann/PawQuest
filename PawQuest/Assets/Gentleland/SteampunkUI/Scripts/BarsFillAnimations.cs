using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gentleland.StemapunkUI.DemoAndExample
{
    public class CharacterHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth { get; private set; }

        [SerializeField] private Slider healthBar; // Reference to the health bar UI

        private void Start()
        {
            // Atur currentHealth ke nilai penuh saat game dimulai
            currentHealth = maxHealth;

            // Set health bar ke nilai maksimum
            UpdateHealthBarInstant();
        }

        public void TakeDamage(int damage)
        {
            // Tidak mengurangi health jika damage adalah 0 atau kurang
            if (damage <= 0)
            {
                Debug.LogWarning("Damage harus lebih besar dari 0 untuk mengurangi health.");
                return;
            }

            // Reduce health by damage amount
            currentHealth -= damage;

            // Clamp health to ensure it doesn't go below 0
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            // Update the health bar with animation
            StartCoroutine(UpdateHealthBarSmooth());
        }

        private IEnumerator UpdateHealthBarSmooth()
        {
            float elapsedTime = 0f;
            float duration = 0.5f; // Animation duration
            float startValue = healthBar.value;
            float targetValue = (float)currentHealth / maxHealth;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                healthBar.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
                yield return null;
            }

            // Ensure it reaches the exact target value
            healthBar.value = targetValue;
        }

        private void UpdateHealthBarInstant()
        {
            // Set slider value langsung ke nilai penuh
            healthBar.value = 1.0f;
        }
    }
}
