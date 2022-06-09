using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _Event = null;
    [SerializeField] private bool _OnStart = false;
    [SerializeField] private bool _OnUpdate = false;
    [SerializeField] private bool _OnButtonPressed = false;

    private bool _AsyncLoading = false;

    void Start()
    {
        if (_OnStart)
            DoEvents();
    }

    void Update()
    {
        if (_OnUpdate)
            DoEvents();

        if (_OnButtonPressed)
            if (Input.anyKey)
                DoEvents();
    }

    private void DoEvents()
    {
        _Event.Invoke();
    }

    //Set Object true/false
    public void SetGameobject_InActive(GameObject targetobject)
    {
        targetobject.SetActive(false);
    }
    public void SetGameobject_Active(GameObject targetobject)
    {
        targetobject.SetActive(true);
    }
    public void SetGameObject_Negative(GameObject targetobject)
    {
        if (targetobject.activeSelf)
            targetobject.SetActive(false);
        else
            targetobject.SetActive(true);
    }

    //Load/Reload Scenes
    public void LoadScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AsyncReloadScene()
    {
        if (!_AsyncLoading)
        {
            _AsyncLoading = true;
            StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        }
    }
    public void AsyncLoadScene(int sceneid)
    {
        if (!_AsyncLoading)
        {
            _AsyncLoading = true;
            StartCoroutine(LoadSceneAsync(sceneid));
        }
    }
    public void AsyncLoadScene(string scenename)
    {
        if (!_AsyncLoading)
        {
            _AsyncLoading = true;
            StartCoroutine(LoadSceneAsync(scenename));
        }
    }
    private IEnumerator LoadSceneAsync(string scenename)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private IEnumerator LoadSceneAsync(int sceneid)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneid);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    //Quit
    public void Quit()
    {
        Application.Quit();
    }
}