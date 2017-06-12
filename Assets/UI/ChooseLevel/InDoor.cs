using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InDoor : MonoBehaviour {

    public string NameOfScene;
    public GameObject locked;

	void OnTriggerEnter2D(Collider2D other)
    {

        if(NameOfScene=="firstScene")
        SceneManager.LoadScene(NameOfScene);

        if (NameOfScene == "secondScene"&&!locked.activeInHierarchy)
        SceneManager.LoadScene(NameOfScene);
    }
}
