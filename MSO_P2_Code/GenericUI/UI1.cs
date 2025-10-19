using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    public class UI1
    {
        public interface IReceiver
        {
            // Allows the model to make changes in the UI.
            void SetTextBoxProgram(string text);
            void SetTextBoxOutput(string text);
        }

        protected readonly IReceiver receiver;
        public UI1(IReceiver receiver)
        {
            this.receiver = receiver;
        }


        public void SelectProgramBasic()
        {
            //todo
            receiver.SetTextBoxProgram("There's supposed to be code here now.");
        }
        public void SelectProgramAdvanced()
        {
            //todo
            receiver.SetTextBoxProgram("There's supposed to be code here now.");
        }
        public void SelectProgramExpert()
        {
            //todo
            receiver.SetTextBoxProgram("There's supposed to be code here now.");
        }

        public void ClickButtonRun()
        {
            //todo
        }
        public void ClickButtonMetrics()
        {
            //todo
        }
    }
}
