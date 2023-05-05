namespace ProjectSpaceship.TableBuilder
{
    enum MergeCell
    {
        MergeTop,
        MergeLeft,
        MergeTopLeft,
        MergeDefault
    }
    enum Alignment
    {
        Left,
        Right,
        Center
    }
    internal class Cell
    {
        public Alignment Alignment { get; set; }
        public MergeCell MergeCellOption { get; set; }
        public string Content { get; set; }

        public Cell(string content, Alignment alignment = Alignment.Left, MergeCell mergeCell = MergeCell.MergeDefault)
        {
            MergeCellOption = mergeCellOption;
            Alignment = alignment;
            Content = content;
        }

    }
}
