using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFuncs : MonoBehaviour
{
    public GameObject conPan;
    private GameObject _event;
    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _event = GameObject.Find("EventSystem");
    }
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.GetAxis("L_YAxis_1"));
	}


    public void Exit(int doConfirm)
    {
        if (doConfirm == 1)
        {
            Confirm.ConfirmScene = Confirm.This.Exit;
            CloseScreen.ReturnTo = _event.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;
            _event.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(conPan.transform.Find("No").gameObject);
            conPan.SetActive(true);
        }
        else
        {
            Application.Quit();
        }
    }

    public void Restart(int doConfirm)
    {
        if (doConfirm == 1)
        {
            Confirm.ConfirmScene = Confirm.This.Return;
            CloseScreen.ReturnTo = _event.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;
            Checkpoint.ShouldThePlayerBe = Checkpoint.Where.Start;
            _event.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(conPan.transform.Find("No").gameObject);
            conPan.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void StartGame(int doConfirm)
    {
        if (doConfirm == 1)
        {
            Confirm.ConfirmScene = Confirm.This.Restart;
            Checkpoint.ShouldThePlayerBe = Checkpoint.Where.Start;
            CloseScreen.ReturnTo = _event.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;
            _event.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(conPan.transform.Find("No").gameObject);
            conPan.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TestScene");
        }
    }
}
