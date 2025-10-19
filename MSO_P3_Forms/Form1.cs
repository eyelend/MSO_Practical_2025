using MSO_P2_Code.GenericUI;

namespace MSO_P3_Forms
{
    public partial class Form1 : Form
    {
        private readonly MSO_P2_Code.GenericUI.UI1 linkToModel;
        public Form1()
        {
            InitializeComponent();
            linkToModel = new(new Receiver(this));
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            linkToModel.ClickButtonRun();
        }

        private void buttonMetrics_Click(object sender, EventArgs e)
        {
            linkToModel.ClickButtonMetrics();
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
            if (selection == basic) linkToModel.SelectProgramBasic();
            else if (selection == advanced) linkToModel.SelectProgramAdvanced();
            else if (selection == expert) linkToModel.SelectProgramExpert();
            else if (selection == "From file...") ;
            else throw new NotImplementedException($"Option '{selection}' not implemented");
        }
    }
}