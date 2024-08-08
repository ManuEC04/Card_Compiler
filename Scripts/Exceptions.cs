namespace Compiler
{
    public static class Exceptions
    {
        public static void PointsException(int position)
        {
            throw new Exception(": Was expected at line" + " " + position);
        }
        public static void CommaException(int position)
        {
            throw new Exception(", Was expected at line" + " " + position);
        }
        public static void OpenSquareException(int position)
        {
            throw new Exception("[ Was expected at line" + " " + position);
        }
        public static void ClosedSquareException(int position)
        {
            throw new Exception("] Was expected at line" + " " + position);
        }
        public static void OpenCurlyException(int position)
        {
            throw new Exception("{ Was expected at line" + " " + position);
        }
        public static void ClosedCurlyException(int position)
        {
            throw new Exception("} Was expected at line" + " " + position);
        }
         public static void QuotationMarkException(int position)
        {
            throw new Exception("\" Was expected at line" + " " + position);
        }
         public static void IdentifierException(int position)
        {
            throw new Exception(" An identifier was expected at line" + " " + position);
        }
        
    }
}