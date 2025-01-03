using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedHealth : MonoBehaviour
{

    private HeroAttribute hero;

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.name == "Hero") 
        {
            hero = other.GetComponent<HeroAttribute>();
            if(hero.currentHealth <= hero.maxHealth)
            {
                hero.Healing(5);
                Debug.Log("HERO HEALED : " + hero.currentHealth);
            }

            Destroy(this.gameObject);
        }
    }
}
