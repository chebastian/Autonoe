namespace HexView
{
    public interface ICellProvider
    {
        byte ReadCellValue(int offset);
        byte[] ReadCellValues(int offset, int length);
    }
}