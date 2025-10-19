using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSO_P2_Code.GenericUI;

namespace MSO_P3_Forms
{
    partial class Form1
    {
        internal class Receiver : UI1.IReceiver
        {
            private readonly Form1 form1;
            public Receiver(Form1 form1)
            {
                this.form1 = form1;
            }

            public void SetTextBoxOutput(string text)
            {
                form1.textBoxOutput.Text = text;
            }

            public void SetTextBoxProgram(string text)
            {
                form1.textBoxProgram.Text = text;
            }

        }
    }
}
