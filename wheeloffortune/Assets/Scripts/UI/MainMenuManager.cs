

using common;
using UnityEngine;
using UnityEngine.Events;


    public class MainMenuManager : MonoBehaviour
    {
      
        public static UnityEvent startGameEvent=new UnityEvent();
        public static UnityEvent resumeGameEvent=new UnityEvent();


        private void Awake()
        {
            
            startGameEvent.AddListener(new UnityAction(()=>SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.game)));
            resumeGameEvent.AddListener(new UnityAction((() => SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.game))));
            
        }

        public void StartGameButtonClick()
        {
            GameManager.Instance.StartNewGame();
            startGameEvent.Invoke();
        }

        public void ResumeGameButtonClick()
        {
            resumeGameEvent.Invoke();
        }

    }

