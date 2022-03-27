using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace common
{
    public class SceneMover : Singleton<SceneMover>
    {
        public enum enumScreen
        {
            mainmenu,
            game,
        }

        public static UnityEvent OnMainMenuLoadEvent = new UnityEvent();
        private enumScreen currentScreen;

        public void SetCurrentScreen(enumScreen s)
        {
            currentScreen = s;
            switch (currentScreen)
            {
                case enumScreen.game:
                    StartCoroutine(LoadGameSceneRoutine());
                    break;
                
                case enumScreen.mainmenu:
                    StartCoroutine(LoadMainMenuRoutine());
                    break;
            }
        }

        private IEnumerator LoadMainMenuRoutine()
        {
            var asyncOperation = SceneManager.LoadSceneAsync("MainMenuScene");
            while (!asyncOperation.isDone) yield return null;

            OnMainMenuLoadEvent.Invoke();
        }

        private void Start()
        {
            OnMainMenuLoadEvent.AddListener(() => { Debug.Log("OnMainMenuLoadEvent"); });
        }


        private IEnumerator LoadGameSceneRoutine()
        {
            var asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
            while (!asyncOperation.isDone) yield return null;
        }


        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (currentScreen == enumScreen.mainmenu)
                    Application.Quit();
                else
                    SetCurrentScreen(enumScreen.mainmenu);
            }
        }   
    }
}