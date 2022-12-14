namespace TCD0502BackEndCourse.TestNinja
{
    public class Math
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public int Max(int a, int b)
        {
            if (a > b) return a;
            if (a < b) return b;
            return a;
        }

    }
}
