using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	public void jugar() {
		SceneManager.LoadScene("Nivel 1");
	}

	public void salir() {
		Application.Quit();
	}

	public void volverMenu() {
		SceneManager.LoadScene("Menu");
	}

}
