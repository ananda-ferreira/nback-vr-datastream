using System.Collections;
using System.Linq;
using Assets.Scripts.DataStream;
using UnityEngine;
using UnityEngine.UI;

namespace Task
{
    /// <summary>
    /// Example behaviour that uses the <see cref="TextTaskManager"/>.
    /// </summary>
    public class TaskManagerBehaviour : MonoBehaviour
    {
        private TextTaskManager _manager = new TextTaskManager();
        private Text _text;

        private InGameOutlet _inGameOutlet;
        private FinishedOutlet _finishedOutlet;
        /// <summary>
        /// Input handler.
        /// </summary>
        public void HandleOnInput()
        {
            if (_manager == null)
                return;
            
            _manager.HandleOnInput();

            // example for getting nbacktask properties
            if (_manager.RunningTask is NBackTask nbackTask)
            {
                // signal outlet to build sample
                _inGameOutlet.JustTriggered = true;
                _inGameOutlet.Task = nbackTask; // not most efficient to set it here, but for our purpose ok

                // change ui color for ux
                // StartCoroutine(ChangeTextColor(Color.grey, nbackTask.CharacterRemainingTime));
                if (!nbackTask.IsCorrect)  // possibility to show that nbacktask answer is wrong.
                    StartCoroutine(ChangeTextColor(Color.red, nbackTask.CharacterRemainingTime));
                else 
                    StartCoroutine(ChangeTextColor(Color.green, nbackTask.CharacterRemainingTime));
                
                Debug.Log($"CharacterRemain> {nbackTask.CharacterRemainingTime}");
                Debug.Log("reaction time: " + nbackTask.ReactionTime); // get reaction time
            }

        }

        // back nbacktask available for outlet
        public NBackTask GetNBackTask()
        {
            return _manager.RunningTask as NBackTask;
        }

        #region Unity methods
        private void Start()
        {
            _text = GetComponentInChildren<Text>(); // get UI element for text based tasks

            var taskBehaviours = GetComponentsInChildren<TextTaskBehaviour>(); // get all text based tasks behaviours
            // Debug.Log("Number of TaskBehaviours: " + taskBehaviours.Length);
            // foreach (var behaviour in taskBehaviours){Debug.Log("Task Behaviour: " + behaviour.name + " | Task Data: " + behaviour.Task);}

            _manager.OnTaskFinished += HandleOnTaskFinished; // possibility to save data after tasks are finished
            _manager.EnqueueTasks(taskBehaviours.Select(x => x.Task).ToArray()); // extract tasks
            _manager.PrepareTask(); // begin with first task

            _inGameOutlet = GetComponent<InGameOutlet>();
            _finishedOutlet = GetComponent<FinishedOutlet>();
        }

        private void Update() => _text.text = _manager.Run(Time.deltaTime);
        #endregion

        #region Private methods
        private void HandleOnTaskFinished(ATask<string> task)
        {
            
            if (task is NBackTask nBackTask) 
            {
                _finishedOutlet.MarkerText = "Task finished";
                _finishedOutlet.JustFinished = true;
                _inGameOutlet.JustFinished = true;
                Debug.Log("error rate" + nBackTask.ErrorRate); // for example save error rate
            }

            if (task is not NBackTask && _manager.Tasks.Count != 0)  {
                _finishedOutlet.MarkerText = "Task started";
                _finishedOutlet.JustFinished = true;
                _inGameOutlet.JustStarted = true;
            }

            Debug.Log("TaskManagerBehaviour: " + _finishedOutlet.MarkerText);
        
        }

        private IEnumerator ChangeTextColor(Color color, float wait)
        {
            _text.color = color;
            yield return new WaitForSeconds(wait);
            _text.color = Color.black;
        }
        #endregion
    }
}
