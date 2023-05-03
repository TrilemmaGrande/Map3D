using System.Text;

namespace ProjectSpaceship.TableBuilder
{
    internal class Table
    {
        private int cursorPositionInY = 1;
        private int cursorPositionInX = 1;
        private int tableYCount;
        private int tableXCount;
        private char[,] tableBuilder;
        private Queue<string> contentQueue = new Queue<string>();
        StringBuilder finalOutputTable = new StringBuilder();
        private char lineTopLeft = '┌';
        private char lineTopRight = '┐';
        private char lineBottomLeft = '└';
        private char lineBottomRight = '┘';
        private char lineVertikal = '│';
        private char lineHorizontal = '─';
        private char lineTTop = '┬';
        private char lineTBottom = '┴';
        private char lineTLeft = '├';
        private char lineTRight = '┤';
        private char lineCross = '┼';
        private char lineSpace = ' ';

        public Table(int rows, int collums)
        {
            int wordLengthMax = 10;
            int borderCountY = rows + 1;
            int borderCountX = collums + 1;

            this.tableYCount = rows + (borderCountY);
            this.tableXCount = (collums * wordLengthMax + borderCountX);
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
        public void AddCells(params Cell[] cellContent)
        {
            foreach (var item in cellContent)
            {
                contentQueue.Enqueue(item.Content);
                if (item.Alignment is Alignment.Left)
                {
                    tableBuilder[cursorPositionInX, cursorPositionInY] = 'L';
                }
                else if (item.Alignment is Alignment.Right)
                {
                    tableBuilder[cursorPositionInX, cursorPositionInY] = 'R';
                }
                else if (item.Alignment is Alignment.Center)
                {
                    tableBuilder[cursorPositionInX, cursorPositionInY] = 'C';
                }
                // Build first row borders.
                if (cursorPositionInX > 1 && cursorPositionInY == 1 && item.MergeCellOption is MergeCellOption.MergeLeft)
                {
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineHorizontal;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTTop;
                }
                else if (cursorPositionInX > 1 && cursorPositionInY == 1 && item.MergeCellOption is not MergeCellOption.MergeLeft)
                {
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTTop;
                }
                // Build middle parts borders.
                else if (cursorPositionInY > 1 && cursorPositionInY < tableYCount - 2)
                {
                    // Left
                    if (cursorPositionInX == 1 && item.MergeCellOption is MergeCellOption.MergeTop)
                    {
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineVertikal;
                        if (tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    else if (cursorPositionInX == 1 && item.MergeCellOption is not MergeCellOption.MergeTop)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTLeft;
                    }
                    // Middle                   
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - 10
                        && item.MergeCellOption is not MergeCellOption.MergeTop
                        && item.MergeCellOption is not MergeCellOption.MergeLeft)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTTop
                            || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTLeft
                            || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - 10
                        && item.MergeCellOption is MergeCellOption.MergeTop
                        && item.MergeCellOption is not MergeCellOption.MergeLeft)
                    {
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTopRight;
                        }
                        else if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineVertikal;
                        }
                        else if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineSpace;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTRight;
                        }
                        if (tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - 10
                    && item.MergeCellOption is not MergeCellOption.MergeTop
                    && item.MergeCellOption is MergeCellOption.MergeLeft)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineHorizontal;
                        }
                        else if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineBottomLeft;
                        }
                        else if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineHorizontal;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTBottom;
                        }                  
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - 10
                    && item.MergeCellOption is MergeCellOption.MergeTop
                    && item.MergeCellOption is MergeCellOption.MergeLeft)
                    {                        
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineSpace;
                        }
                        else if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineSpace;
                        }
                        else if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineSpace;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineBottomRight;
                        }
                        if (tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTLeft;
                        }
                    } //stopped here, move on there:
                    // Right
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTRight;
                    }
                }
                // Build last row borders.
                else if (cursorPositionInY == tableYCount - 1)
                {
                    // Left
                    if (cursorPositionInX == 1)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTLeft;
                    }
                    // Middle
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTBottom;
                    }
                    // Right
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        tableBuilder[cursorPositionInX + 10, cursorPositionInY - 1] = lineTRight;
                    }
                }

                // New Line
                if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - 10 && cursorPositionInY < tableYCount - 2)
                {
                    cursorPositionInY += 2;
                    cursorPositionInY = 1;
                }
                else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - 10 && cursorPositionInY < tableYCount - 2)
                {
                    cursorPositionInX += 11;
                }
            }
        }
        public string GetTable()
        {
            //do this
            return finalOutputTable.ToString();
        }
    }
}
