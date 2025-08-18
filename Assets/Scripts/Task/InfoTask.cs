// using System;
// using System.Diagnostics;

namespace Task
{
    public class InfoTask : ATask<string>
    {
        public string Text {get; set;} = string.Empty;

        public InfoTask(string text)
        {
            Text = text;
        }

        public override string Run(float deltaTime)
        {
            CheckForInput();
            CheckForN();
            return Text;
        }

        private void CheckForInput()
        {
            if (!IsInputTriggered)
                return;

            // Text = string.Empty;
            OnTaskFinished?.Invoke(this);
        }

        private void CheckForN() {

        }
    }
}
