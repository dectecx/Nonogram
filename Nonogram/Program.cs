namespace Nonogram
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 5x5
            var columns = new int[][]
            {
                new int[] { 2 }, new int[] { 3 }, new int[] { 2 },
                new int[] { 3 }, new int[] { 3 }
            };
            var rows = new int[][]
            {
                new int[] { 3 }, new int[] { 3 }, new int[] { 2, 2 },
                new int[] { 2 }, new int[] { 1 }
            };
            var run = new NonogramSolver(columns, rows);
            run.Solve();
        }
    }
}
