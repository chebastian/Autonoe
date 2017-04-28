namespace HexView
{
    public class CellSelectedEvent
    {
        public MatrixCell matrixCell;

        public CellSelectedEvent(MatrixCell matrixCell)
        {
            this.matrixCell = matrixCell;
        }
    }
}