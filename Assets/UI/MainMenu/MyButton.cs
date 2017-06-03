using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();
    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }



    void Start()
    {
      signalOnClick.AddListener(this.onPlay);
    }
    void onPlay()
    {
        
        SceneManager.LoadScene("ChooseLevel");
    }
}