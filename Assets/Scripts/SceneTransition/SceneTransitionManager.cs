using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransition {
    public class SceneTransitionManager : MonoBehaviour
    {
        public FadeScreen fadeScreen;

        public void GoToSceneAsync(int sceneIndex) 
        {
            Debug.Log("GoToScene called");
            StartCoroutine(GoToSceneRoutineAsync(sceneIndex));
            Debug.Log("GoToScene done");
        }
        
        IEnumerator GoToSceneRoutineAsync(int sceneIndex)
        {
            fadeScreen.FadeOut();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;

            float timer = 0;
            while(timer <= fadeScreen.fadeDuration && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            operation.allowSceneActivation = true;
        }
    }
}
