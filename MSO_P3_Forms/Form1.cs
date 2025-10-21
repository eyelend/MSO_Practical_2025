using UI1 = MSO_P2_Code.GenericUI.UI1;

namespace MSO_P3_Forms
{
    public partial class Form1 : Form
    {
        class DataBridge : UI1.IDataBridge
        {
            private readonly Form1 form1;
            public DataBridge(Form1 form1)
            {
                this.form1 = form1;
            }

            public string ReadTextBoxProgram()
            {
                return form1.textBoxProgram.Text;
            }

            public void SetTextBoxOutput(string text)
            {
                //form1.textBoxOutput.Text = text;
                form1.textBoxOutput.Lines = text.Split('\n');
            }

            public void SetTextBoxProgram(string text)
            {
                //form1.textBoxProgram.Text = text;
                form1.textBoxProgram.Lines = text.Split('\n');
            }

        }

        private readonly UI1 model;
        public Form1()
        {
            InitializeComponent();
            model = new(new DataBridge(this));
        }

        private string ManuallyFindAndReadToEnd(OpenFileDialog odf)
        {
            odf.ShowDialog();
            Stream stream = odf.OpenFile();
            StreamReader reader = new(stream);
            string fileContent = reader.ReadToEnd();
            reader.Close();
            return fileContent;
        }


        #region UIEvents
        private void buttonRun_Click(object sender, EventArgs e)
        {
            model.ClickRun();
        }

        private void buttonMetrics_Click(object sender, EventArgs e)
        {
            model.ClickMetrics();
        }

        private void comboBoxLoadProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            // todo: increase cohesion in this method.

            if (comboBoxLoadProgram != (ComboBox)sender) throw new Exception("Misunderstanding 'sender'."); // verifying what 'sender' means
            ComboBox comboBox = (ComboBox)sender; //comboBoxLoadProgram;
            string basic = "Basic", advanced = "Advanced", expert = "Expert";

            // simple test in case someone removed or misspelled a word 
            ComboBox.ObjectCollection collection = comboBox.Items;
            if (!(collection.Contains(basic) && collection.Contains(advanced) && collection.Contains(expert)))
                throw new Exception("Doesn't contain all hard-coded-example elements");

            // actually handle the selection
            string selection = (string)comboBox.SelectedItem;
            if (selection == basic) model.SelectProgramBasic();
            else if (selection == advanced) model.SelectProgramAdvanced();
            else if (selection == expert) model.SelectProgramExpert();
            else if (selection == "From file...") textBoxProgram.Text = ManuallyFindAndReadToEnd(openFileDialog1);
            else throw new NotImplementedException($"Option '{selection}' not implemented");
        }

        private void buttonLoadExercise_Click(object sender, EventArgs e)
        {
            model.SelectExercise(ManuallyFindAndReadToEnd(openFileDialog1));
        }
        #endregion UIEvents
    }
}