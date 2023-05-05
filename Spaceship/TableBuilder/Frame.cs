using System.Text;

namespace ProjectSpaceship.TableBuilder
{
    internal class Frame
    {
        private int tableYCount;
        private int tableXCount;
        private string inputString;
        private char[,] tableBuilder;
        StringBuilder finalOutputTable = new StringBuilder();
        private const char lineTopLeft = '\u250C';
        private const char lineTopRight = '\u2510';
        private const char lineBottomLeft = '\u2514';
        private const char lineBottomRight = '\u2518';
        private const char lineVertikal = '\u2502';
        private const char lineHorizontal = '\u2500';

        public Frame(string inputString)
        {
            this.inputString = inputString;
            this.tableXCount = inputString.Length + 2;
            this.tableYCount = 3;
            this.tableBuilder = new char[tableXCount, tableYCount];
            // Build frame borders.
            for (int i = 0; i < tableXCount; i++)
            {
                if (i == 0)
                {
                    tableBuilder[i, 0] = lineTopLeft;
                    tableBuilder[i, tableYCount - 1] = lineBottomLeft;
                }
                else if (i == tableXCount - 1)
                {
                    tableBuilder[i, 0] = lineTopRight;
                    tableBuilder[i, tableYCount - 1] = lineBottomRight;
                }
                else
                {
                    tableBuilder[i, 0] = lineHorizontal;
                    tableBuilder[i, tableYCount - 1] = lineHorizontal;
                }
            }
            for (int j = 0; j < tableYCount; j++)
            {
                if (j != 0 && j != tableYCount - 1)
                {
                    tableBuilder[0, j] = lineVertikal;
                    tableBuilder[tableXCount - 1, j] = lineVertikal;
                }
            }
        }
        public string GetFrame()
        {
            for (int i = 0; i < tableYCount; i++)
            {
                for (int j = 0; j < tableXCount; j++)
                {
                    if (j == 1 && i == 1)
                    {                        
                        finalOutputTable.Append(inputString);
                    }
                    else
                    {
                        finalOutputTable.Append(tableBuilder[j, i]);
                    }
                }
                finalOutputTable.Append('\n');
            }
            return finalOutputTable.ToString();
        }
    }
}
