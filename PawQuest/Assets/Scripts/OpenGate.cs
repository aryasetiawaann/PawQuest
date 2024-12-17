using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public int enemyCount = 0;

    // --------- enemyCount dikurangi lewat script Enemy ---------
    void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang bersentuhan memiliki tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Tambahkan 1 ke jumlah musuh yang bersentuhan
            enemyCount++;
        }
    }

    void OnTriggerStay(Collider other) {
        if(enemyCount == 0){
            if(other.CompareTag("Gate")){
                Animator gateAnim = other.GetComponentInChildren<Animator>();
                gateAnim.SetBool("isOpen", true);
            }
        }
    }
}
