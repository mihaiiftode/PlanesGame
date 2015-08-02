namespace PlanesGame.GameGraphics
{
    public class MatrixCoordinate
    {
        public int Row { get; set; }

        public int Collumn { get; set; }

        public MatrixCoordinate(int row, int collumn)
        {
            Row = row;
            Collumn = collumn;
        }

        public override string ToString()
        {
            return Row + Collumn.ToString();
        }
    }
}