using MSO_P2_Code.Applic;
using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    public class UI1
    {
        public interface IDataBridge
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
        protected readonly IDataBridge dataBridge;

        private WorldSettings loadedWorld = new();
        public UI1(IDataBridge dataBridge)
        {
            this.dataBridge = dataBridge;
            examplePrograms = ExamplePrograms.Instance;
            programParser = ProgramParser.Instance;
            outputLanguage = OutputLanguage1.Instance;
        }


        private void SelectHardcodedProgram(InnerProgram program)
        {
            try
            {
                dataBridge.SetTextBoxProgram(programParser.Unparse(program));
            }
            catch (NotImplementedException e)
            {
                dataBridge.SetTextBoxProgram("Error: \n" + e.Message);
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
                programFromBox = programParser.Parse(dataBridge.ReadTextBoxProgram(), loadedWorld);
                return true; //success
            }
            catch (ParseFailException e)
            {
                dataBridge.SetTextBoxOutput("Parse error: " + e.Message);
                programFromBox = null;
                return false; // failure
            }

        }
        public void ClickRun()
        {
            if (!TryParseTextBoxProgram(out InnerProgram program))
                return;
            dataBridge.ClearTrace();

            World.WorldState endState;
            try { endState = program.Execute(); }
            catch (BlockException e)
            {
                dataBridge.SetTextBoxOutput(e.Message);
                return;
            }
            catch (LeftGridException e)
            {
                dataBridge.SetTextBoxOutput(e.Message);
                return;
            }
            dataBridge.SetTextBoxOutput(outputLanguage.ExecutionResult(endState));
            dataBridge.SetCharacterPos(endState.playerState.Pos);

            (int x, int y)[] posTrace = endState.PosTrace;
            for (int i = 1; i < posTrace.Length; i++)
                MarkMove(posTrace[i - 1], posTrace[i]);
            void MarkMove((int x, int y) start, (int x, int y) end)
            {
                if (start.x == end.x)
                {
                    (int y0, int y1) = sort(start.y, end.y);
                    dataBridge.AddGridTraceVertical(start.x, y0, y1);
                }
                else if (start.y == end.y)
                {
                    (int x0, int x1) = sort(start.x, end.x);
                    dataBridge.AddGridTraceHorizontal(start.y, x0, x1);
                }
                else throw new Exception($"Somehow got a slanted move, from {start} to {end}.");

                (int, int) sort(int x, int y) => x <= y ? (x, y) : (y, x);
            }
        }
        public void ClickMetrics()
        {
            if (!TryParseTextBoxProgram(out InnerProgram program))
                return;
            dataBridge.SetTextBoxOutput(outputLanguage.ShowMetrics(program));
        }

        public void SelectExercise(string fileContent)
        {
            dataBridge.ClearExerciseStuff();
            dataBridge.ClearTrace();

            // fill grid based on exercise info
            char[,] worldGrid;
            try
            {
                string[] rows = fileContent.Split("\r\n");
                if (rows.Length == 0 || rows[0].Length == 0)
                    throw new ParseFailException("Empty exercise file");
                worldGrid = new char[rows[0].Length, rows.Length];
                loadedWorld = new WorldSettings((rows[0].Length, rows.Length));
                for (int y = 0; y < worldGrid.GetLength(1); y++)
                {
                    string row = rows[y];
                    if (row.Length != worldGrid.GetLength(0))
                        throw new ParseFailException($"Inconsistent grid width, row {y}.  {row.Length} != {worldGrid.GetLength(0)}");
                    for (int x = 0; x < worldGrid.GetLength(0); x++)
                    {
                        char cellInfo = row[x];
                        worldGrid[x, y] = cellInfo;
                        switch (cellInfo)
                        {
                            case 'o': // empty cell
                                break;
                            case '+': // wall
                                dataBridge.BlockCell((x, y));
                                loadedWorld.TryBlockCell((x, y));
                                break;
                            case 'x': //destination
                                dataBridge.SetDestination((x, y));
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
                dataBridge.SetTextBoxOutput(e.Message);
                return;
            }

            //mark edges of grid
            for (int x = -1; x <= worldGrid.GetLength(0); x++)
            {
                dataBridge.BlockCell((x, -1));
                dataBridge.BlockCell((x, worldGrid.GetLength(1)));
            }
            for (int y = 0; y < worldGrid.GetLength(1); y++)
            {
                dataBridge.BlockCell((-1, y));
                dataBridge.BlockCell((worldGrid.GetLength(0), y));
            }

        }
        public void UnselectExercise()
        {
            //todo: let the model know there's no exercise at the moment.
            loadedWorld = new WorldSettings();
        }
    }
}
