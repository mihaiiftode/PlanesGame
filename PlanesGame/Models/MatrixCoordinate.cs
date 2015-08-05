namespace PlanesGame.Models
{
    public class MatrixCoordinate
    {
        public MatrixCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public override string ToString()
        {
            return Row + Column.ToString();
        }
    }
}