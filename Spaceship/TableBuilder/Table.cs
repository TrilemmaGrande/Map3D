using System.Globalization;
using System.Text;

namespace ProjectSpaceship.TableBuilder
{
    internal class Table
    {
        private int cursorPositionInY = 1;
        private int cursorPositionInX = 1;
        private int tableYCount;
        private int tableXCount;
        private int wordLengthMax;
        private char[,] tableBuilder;
        private Queue<string> contentQueue = new Queue<string>();
        StringBuilder finalOutputTable = new StringBuilder();
        private const char lineTopLeft = '\u250C';
        private const char lineTopRight = '\u2510';
        private const char lineBottomLeft = '\u2514';
        private const char lineBottomRight = '\u2518';
        private const char lineVertikal = '\u2502';
        private const char lineHorizontal = '\u2500';
        private const char lineTTop = '\u252c';
        private const char lineTBottom = '\u2534';
        private const char lineTLeft = '\u251c';
        private const char lineTRight = '\u2524';
        private const char lineCross = '\u253c';
        private const char lineSpace = ' ';

        public Table(int rows, int collums, int wordLengthMax)
        {            
            int borderCountY = rows + 1;
            int borderCountX = collums + 1;
            this.wordLengthMax = wordLengthMax;
            this.tableYCount = rows + borderCountY;
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
                contentQueue.Enqueue((string)item.Content);
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
                if (cursorPositionInX > 1 && cursorPositionInY == 1 && item.MergeCellOption is MergeCell.MergeLeft)
                {
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineHorizontal;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTTop;
                }
                else if (cursorPositionInX > 1 && cursorPositionInY == 1 && item.MergeCellOption is not MergeCell.MergeLeft)
                {
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTTop;
                }
                if (cursorPositionInX > 1 && cursorPositionInY == 1 && item.MergeCellOption is not MergeCell.MergeLeft && tableYCount == 3)
                {
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTTop;
                    tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTBottom;
                }
                // Build middle parts borders.
                else if (cursorPositionInY > 1 && cursorPositionInY < tableYCount - 3 && tableYCount > 3)
                {
                    // Left
                    if (cursorPositionInX == 1 && item.MergeCellOption is MergeCell.MergeTop && tableXCount < wordLengthMax - 1)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineVertikal;
                        if (tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    else if (cursorPositionInX == 1 && item.MergeCellOption is not MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        if (tableXCount == wordLengthMax + 2)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTRight;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTLeft;

                    }
                    // Middle                   
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeDefault && tableXCount > wordLengthMax + 2)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop
                            || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTLeft
                            || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
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
                        if (tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                    && item.MergeCellOption is MergeCell.MergeLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTTop;
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
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                    && item.MergeCellOption is MergeCell.MergeTopLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTTop;
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
                        if (tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    // Right
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeDefault)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTRight;
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop
                           || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTLeft
                           || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineVertikal;
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
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                    && item.MergeCellOption is MergeCell.MergeLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTRight;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTTop;
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
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax + 1
                    && item.MergeCellOption is MergeCell.MergeTopLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineVertikal;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTTop;
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
                    }
                }
                // Build last row borders.
                else if (cursorPositionInY == tableYCount - 2 && tableYCount > 3)
                {
                    // Left
                    if (cursorPositionInX == 1 && item.MergeCellOption is MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineVertikal;
                        if (tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    else if (cursorPositionInX == 1 && item.MergeCellOption is not MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        if (tableXCount == wordLengthMax + 2)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTRight;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineTLeft;
                    }
                    // Middle
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeDefault)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop
                            || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTLeft
                            || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTBottom;
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineTBottom;
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
                        if (tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                    && item.MergeCellOption is MergeCell.MergeLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineHorizontal;
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
                    else if (cursorPositionInX > 1 && cursorPositionInX < tableXCount - wordLengthMax - 1
                    && item.MergeCellOption is MergeCell.MergeTopLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY + 1] = lineHorizontal;
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
                        if (tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] == lineTTop)
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTopLeft;
                        }
                        else
                        {
                            tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTLeft;
                        }
                    }
                    // Right
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                       && item.MergeCellOption is MergeCell.MergeDefault)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTRight;

                        if (tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] == lineTTop
                           || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTLeft
                           || tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] != lineTopLeft)
                        {
                            tableBuilder[cursorPositionInX - 1, cursorPositionInY - 1] = lineCross;
                        }
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeTop)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineVertikal;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineVertikal;
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
                    }
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                        && item.MergeCellOption is MergeCell.MergeLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineHorizontal;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineTRight;
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
                    else if (cursorPositionInX > 1 && cursorPositionInX == tableXCount - wordLengthMax - 1
                    && item.MergeCellOption is MergeCell.MergeTopLeft)
                    {
                        for (int i = 0; i < wordLengthMax; i++)
                        {
                            tableBuilder[cursorPositionInX + i, cursorPositionInY - 1] = lineSpace;
                        }
                        tableBuilder[cursorPositionInX - 1, cursorPositionInY] = lineSpace;                  
                        tableBuilder[cursorPositionInX + wordLengthMax, cursorPositionInY - 1] = lineVertikal;
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
                    }
                }
                // Set Cursor
                if (cursorPositionInX >= 1 && cursorPositionInX == tableXCount - wordLengthMax - 1 && cursorPositionInY <= tableYCount)
                {
                    cursorPositionInY += 2;
                    cursorPositionInX = 1;
                }
                else if (cursorPositionInX >= 1 && cursorPositionInX < tableXCount - wordLengthMax - 1 && cursorPositionInY < tableYCount)
                {
                    cursorPositionInX += wordLengthMax + 1;
                }
            }
        }
        public string GetTable()
        {
            for (int i = 0; i < tableYCount; i++)
            {
                for (int j = 0; j < tableXCount; j++)
                {
                    if (tableBuilder[j, i] == 'L')
                    {
                        string outputString = contentQueue.Dequeue().ToString();
                        finalOutputTable.Append($"{outputString.PadRight(wordLengthMax)}");
                    }
                    else if (tableBuilder[j, i] == 'R')
                    {
                        string outputString = contentQueue.Dequeue().ToString();
                        finalOutputTable.Append($"{outputString.PadLeft(wordLengthMax)}");
                    }
                    else if (tableBuilder[j, i] == 'C')
                    {
                        int padding = (wordLengthMax - contentQueue.Peek().Length) / 2;
                        string outputString = contentQueue.Dequeue().ToString().PadLeft(padding);
                        finalOutputTable.Append($"{outputString.PadRight(wordLengthMax)}");
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
