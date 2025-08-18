using UnityEngine;

namespace Task
{
    // [RequireComponent(typeof(TaskManagerBehaviour))]
    public class InfoBehaviour : TextTaskBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private string _text = string.Empty;

        private NBackBehaviour _nBackBehaviour;
        private void Awake()
        {
            if (_text != "Finished") 
            {
                _nBackBehaviour = GetComponent<NBackBehaviour>();
                _text = UpdateText(_nBackBehaviour.N);
            }
        }
        private void Start() => Task = new InfoTask(_text);

        private void Update()
        {
            if (_text != "Finished") 
            {
                if (Task is InfoTask infoTask)
                {
                    _text = UpdateText(_nBackBehaviour.N);
                    infoTask.Text = _text;
                }
            }
        }

        private string UpdateText(int n){
            return $"{n}-Back Task\nPress Trigger to Start";
        }
    }
}  
