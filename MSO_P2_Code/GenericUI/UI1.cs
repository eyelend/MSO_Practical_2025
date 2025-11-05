using MSO_P2_Code.Applic;
using MSO_P2_Code.GenericUI.Parser;
using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MSO_P2_Code.GenericUI
{
    public class UI1
    {
        public interface IMediator
        {
            // Allows the model's generic UI to somewhat interact with the actual UI-elements.
            void SetTextBoxProgram(string text);
            void SetTextBoxOutput(string text);
            string ReadTextBoxProgram();

            void BlockCell((int x, int y) p);
            void AddGridTraceHorizontal(int y, int x0, int x1);
            void AddGridTraceVertical(int x, int y0, int y1);
            void SetCharacterPos((int x, int y) p);
            void SetDestination((int x, int y) p);
            void ClearExerciseStuff();
            void ClearTrace();
        }

        private readonly ExamplePrograms examplePrograms;
        private readonly ProgramParser programParser;
        private readonly IOutputLanguage outputLanguage;
        protected readonly IMediator mediator;

        private WorldSettings loadedWorld = new();
        public UI1(IMediator mediator)
        {
            this.mediator = mediator;
            examplePrograms = ExamplePrograms.Instance;
            programParser = ProgramParser.Instance;
            outputLanguage = OutputLanguage1.Instance;
        }


        private void SelectHardcodedProgram(InnerProgram program)
        {
            try
            {
                mediator.SetTextBoxProgram(programParser.Unparse(program));
            }
            catch (NotImplementedException e)
            {
                mediator.SetTextBoxProgram("Error: \n" + e.Message);
            }
        }
        public void SelectProgramBasic()
        {
            SelectHardcodedProgram(examplePrograms.basic1);
        }
        public void SelectProgramAdvanced()
        {
            SelectHardcodedProgram(examplePrograms.advanced1);

        }
        public void SelectProgramExpert()
        {
            SelectHardcodedProgram(examplePrograms.expert1);
        }

        private bool TryParseTextBoxProgram(out InnerProgram programFromBox)
        {
            try
            {
                programFromBox = programParser.Parse(mediator.ReadTextBoxProgram(), loadedWorld);
                return true; //success
            }
            catch (ParseFailException e)
            {
                mediator.SetTextBoxOutput("Parse error: " + e.Message);
                programFromBox = null;
                return false; // failure
            }

        }
        public void ClickRun()
        {
            if (!TryParseTextBoxProgram(out InnerProgram program))
                return;
            mediator.ClearTrace();

            World.WorldState endState;
            try { endState = program.Execute(); }
            catch (BlockException e)
            {
                mediator.SetTextBoxOutput(e.Message);
                return;
            }
            catch (LeftGridException e)
            {
                mediator.SetTextBoxOutput(e.Message);
                return;
            }
            mediator.SetTextBoxOutput(outputLanguage.ExecutionResult(endState));
            mediator.SetCharacterPos(endState.playerState.Pos);

            (int x, int y)[] posTrace = endState.PosTrace;
            for (int i = 1; i < posTrace.Length; i++)
                MarkMove(posTrace[i - 1], posTrace[i]);
            void MarkMove((int x, int y) start, (int x, int y) end)
            {
                if (start.x == end.x)
                {
                    (int y0, int y1) = sort(start.y, end.y);
                    mediator.AddGridTraceVertical(start.x, y0, y1);
                }
                else if (start.y == end.y)
                {
                    (int x0, int x1) = sort(start.x, end.x);
                    mediator.AddGridTraceHorizontal(start.y, x0, x1);
                }
                else throw new Exception($"Somehow got a slanted move, from {start} to {end}.");

                (int, int) sort(int x, int y) => x <= y ? (x, y) : (y, x);
            }
        }
        public void ClickMetrics()
        {
            if (!TryParseTextBoxProgram(out InnerProgram program))
                return;
            mediator.SetTextBoxOutput(outputLanguage.ShowMetrics(program));
        }

        public void SelectExercise(string fileContent)
        {
            // todo: strengthen cohesion
            mediator.ClearExerciseStuff();
            mediator.ClearTrace();

            // fill grid based on exercise info
            //char[,] worldGrid;
            (int x, int y) worldSize;
            try
            {
                string[] rows = fileContent.Split("\r\n");
                if (rows.Length == 0 || rows[0].Length == 0)
                    throw new ParseFailException("Empty exercise file");
                worldSize = (rows[0].Length, rows.Length);
                //worldGrid = new char[rows[0].Length, rows.Length];
                loadedWorld = new WorldSettings(worldSize);
                for (int y = 0; y < worldSize.y; y++)
                {
                    string row = rows[y];
                    if (row.Length != worldSize.x)
                        throw new ParseFailException($"Inconsistent grid width, row {y}.  {row.Length} != {worldSize.x}");
                    for (int x = 0; x < worldSize.x; x++)
                    {
                        char cellInfo = row[x];
                        //worldGrid[x, y] = cellInfo;
                        switch (cellInfo)
                        {
                            case 'o': // empty cell
                                break;
                            case '+': // wall
                                mediator.BlockCell((x, y));
                                loadedWorld.TryBlockCell((x, y));
                                break;
                            case 'x': //destination
                                mediator.SetDestination((x, y));
                                if (loadedWorld.Destination != null) throw new ParseFailException("Found a second destination at " + (x, y));
                                else loadedWorld.TrySetDestination((x, y));
                                break;
                            default:
                                throw new ParseFailException($"Found invalid symbol '{cellInfo}' in supposed exercise file. \nAt point ({x},{y}). \nRow = '{row}'.");
                        }
                    }
                }
            }
            catch(ParseFailException e)
            {
                mediator.SetTextBoxOutput(e.Message);
                return;
            }

            //mark edges of grid
            for (int x = -1; x <= worldSize.x; x++)
            {
                mediator.BlockCell((x, -1));
                mediator.BlockCell((x, worldSize.y));
            }
            for (int y = 0; y < worldSize.y; y++)
            {
                mediator.BlockCell((-1, y));
                mediator.BlockCell((worldSize.x, y));
            }

        }
        public void UnselectExercise()
        {
            loadedWorld = new WorldSettings();
        }


        #region export
        private FileStream? TryCreateFile(string pathAndName, string contentAsText)
        {
            FileStream fileStream;

            fileStream = File.Create(pathAndName);

            //convert content
            int length = contentAsText.Length;
            byte[] contentAsBytes = new byte[length];
            for (int i = 0; i < length; i++)
                contentAsBytes[i] = (byte)contentAsText[i];

            fileStream.Write(contentAsBytes, 0, length);
            fileStream.Dispose();
            fileStream.Close();

            return fileStream;
        }
        public bool TryExportTXT(string pathAndName)
        {
            string contentAsText = mediator.ReadTextBoxProgram();

            // check for parse error.
            try { programParser.Parse(contentAsText); }
            catch { return false; }

            FileStream? fileStream = TryCreateFile(pathAndName, contentAsText);
            return fileStream != null;
        }
        public void ExportTXT(string pathAndName)
        {
            if (!TryExportTXT(pathAndName)) mediator.SetTextBoxOutput("txt export fail");
        }
        public bool TryExportHTML(string pathAndName)
        {
            string textBoxText = mediator.ReadTextBoxProgram();
            string contentAsText;

            try
            {
                contentAsText = new ProgramParserHTML().Unparse(programParser.Parse(textBoxText)); //convert
            }
            catch { return false; }

            FileStream? fileStream = TryCreateFile(pathAndName, contentAsText);
            return fileStream != null;
        }
        public void ExportHTML(string pathAndName)
        {
            if (!TryExportHTML(pathAndName)) mediator.SetTextBoxOutput("html export fail");
        }
        #endregion export
    }
}
