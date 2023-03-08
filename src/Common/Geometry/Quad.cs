namespace LearnDirectX.src.Common.Geometry
{
    public abstract class Quad
    {
        public readonly int I;
        public readonly int J;
        public readonly int K;

        public Quad(int i, int j, int k)
        {
            I = i;
            J = j;
            K = k;
        }
    }
}
