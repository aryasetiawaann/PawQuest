using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroManager : MonoBehaviour
{

	#region Singleton

	public static HeroManager instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public GameObject DogKnight;

	public void KillPlayer ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
