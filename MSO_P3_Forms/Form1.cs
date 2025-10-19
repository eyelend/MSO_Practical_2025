using UI1 = MSO_P2_Code.GenericUI.UI1;

namespace MSO_P3_Forms
{
    public partial class Form1 : Form
    {
        private readonly UI1 model;
        public Form1()
        {
            InitializeComponent();
            model = new(new DataBridge(this));
        }

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
            ComboBox uiElement = comboBoxLoadProgram;
            ComboBox.ObjectCollection collection = uiElement.Items;
            string basic = "Basic", advanced = "Advanced", expert = "Expert";

            // simple test in case someone removed or misspelled a word 
            if (!(collection.Contains(basic) && collection.Contains(advanced) && collection.Contains(expert)))
                throw new Exception("Doesn't contain all hard-coded-example elements");

            // actually handle the selection
            string selection = (string)uiElement.SelectedItem;
            if (selection == basic) model.SelectProgramBasic();
            else if (selection == advanced) model.SelectProgramAdvanced();
            else if (selection == expert) model.SelectProgramExpert();
            else if (selection == "From file...") ;
            else throw new NotImplementedException($"Option '{selection}' not implemented");
        }





        internal class DataBridge : UI1.IDataBridge
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
                form1.textBoxOutput.Text = text;
            }

            public void SetTextBoxProgram(string text)
            {
                form1.textBoxProgram.Text = text;
            }

        }

    }
}