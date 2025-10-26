using System.Drawing;
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
                form1.textBoxOutput.Lines = text.Split('\n');
            }
            public void SetTextBoxProgram(string text)
            {
                form1.textBoxProgram.Lines = text.Split('\n');
            }


            public void AddGridTraceHorizontal(int y, int x0, int x1)
            {
                throw new NotImplementedException();
            }

            public void AddGridTraceVertical(int x, int y0, int y1)
            {
                throw new NotImplementedException();
            }

            public void BlockCell((int x, int y) p)
            {
                form1.BlockWorldCell(p);
            }

            public void SetDestination((int x, int y) p)
            {
                form1.SetDestination(p);
            }

            public void SetCharacterPos((int x, int y) p)
            {
                Control character = form1.player;
                Size size = character.Size;
                (int x, int y) newLoc = form1.worldGridBase.PutRectInMiddle((size.Width, size.Height), p);
                character.Location = new Point(newLoc.x, newLoc.y);
            }

            public void ClearGrid()
            {
                form1.worldGridItems.Clear();
                SetCharacterPos((0, 0));
            }
        }

        private readonly UI1 model;
        private readonly ControlSubset worldGridItems;
        private GridBase2D worldGridBase;
        public Form1()
        {
            InitializeComponent();
            model = new(new DataBridge(this));
            worldGridItems = new(this.Controls, new List<Control>());

            // initialize worldGridBase
            (int x, int y) gridPos = (worldGrid.Left + 1, worldGrid.Top + 1);
            (int x, int y) gridCellCount = (worldGrid.ColumnCount, worldGrid.RowCount);
            (int x, int y) cellSize = (worldGrid.Width / gridCellCount.x, worldGrid.Height / gridCellCount.y);
            worldGridBase = new(gridPos, cellSize, gridCellCount);
        }

        private string? ManuallyFindAndReadToEnd(OpenFileDialog odf)
        {
            switch (odf.ShowDialog())
            {
                case DialogResult.OK:
                    Stream stream = odf.OpenFile();
                    StreamReader reader = new(stream);
                    string fileContent = reader.ReadToEnd();
                    reader.Close();
                    return fileContent;
                case DialogResult.Cancel:
                    return null;
                default:
                    throw new Exception();
            }
        }
        private void ManuallyFindAndUse(OpenFileDialog odf, Action<string> use)
        {
            string? fileContent = ManuallyFindAndReadToEnd(odf);
            if (fileContent != null) use(fileContent);
        }


        #region UIEvents
        private void Form1_Load(object sender, EventArgs e)
        {
            player.Left = this.Right;
            destinationMark.Left = this.Right;
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
            else if (selection == "From file...")
                ManuallyFindAndUse(openFileDialog1, (string text) => textBoxProgram.Text = text);
            else throw new NotImplementedException($"Option '{selection}' not implemented");
        }

        private void buttonLoadExercise_Click(object sender, EventArgs e)
        {
            ManuallyFindAndUse(openFileDialog1, model.SelectExercise);
        }
        #endregion UIEvents

        private Panel NewPanel((int x, int y) windowPos, (int x, int y) size, (int r, int g, int b) color)
        {
            Panel result = new Panel();
            result.Location = new Point(windowPos.x, windowPos.y);
            result.Size = new Size(size.x, size.y);
            result.BackColor = Color.FromArgb(color.r, color.g, color.b);
            result.BringToFront();
            player.BringToFront();
            return result;
        }

        public void BlockWorldCell((int x, int y) p)
        {
            (int x, int y) itemSize = (worldGridBase.cellSize.x - 2, worldGridBase.cellSize.y - 3);
            (int x, int y) windowPos = worldGridBase.PutRectInMiddle(itemSize, p);
            (int r, int g, int b) color = (128, 64, 96);

            worldGridItems.AddItem(NewPanel(windowPos, itemSize, color));
        }

        public void SetDestination((int x, int y) p)
        {
            Control item = destinationMark;
            (int x, int y) windowPos = worldGridBase.PutRectInMiddle((item.Width, item.Height), p);
            item.Location = new Point(windowPos.x, windowPos.y);
        }
    }
}