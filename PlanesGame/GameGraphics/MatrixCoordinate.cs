namespace PlanesGame.GameGraphics
{
    public class MatrixCoordinate
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public MatrixCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return Row + Column.ToString();
        }
    }
}