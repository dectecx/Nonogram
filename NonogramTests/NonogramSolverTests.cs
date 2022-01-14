using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Nonogram.Tests
{
    [TestClass()]
    public class NonogramSolverTests
    {
        /// <summary>
        /// 比對二維陣列的值是否一樣
        /// </summary>
        private bool SequenceEquals<T>(T[,] a, T[,] b)
            => a.Rank == b.Rank &&
                Enumerable.Range(0, a.Rank).All(d => a.GetLength(d) == b.GetLength(d)) &&
                a.Cast<T>().SequenceEqual(b.Cast<T>());

        /// <summary>
        /// 二維陣列轉置
        /// </summary>
        private static int[,] Transpose(int[,] array2D)
        {
            int x = array2D.GetUpperBound(0) + 1; // 取得行數
            int y = array2D.GetUpperBound(1) + 1; // 取得列數
            int[,] transarray = new int[y, x];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    transarray[j, i] = array2D[i, j];
                }
            }
            return transarray;
        }

        [TestMethod()]
        public void SolveTest_5x5_A()
        {
            // arrange
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
            var expected = new int[,]
            {
                { 0, 0, 1, 1, 1 },
                { 0, 0, 1, 1, 1 },
                { 1, 1, 0, 1, 1 },
                { 1, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_15x15_A()
        {
            // arrange
            //15x15
            int[][] columns = new int[][]
            {
                new int[] { 4, 7 }, new int[] { 3, 6 }, new int[] { 3 },
                new int[] { 2, 1 }, new int[] { 6, 1 }, new int[] { 4, 3 },
                new int[] { 4, 1, 2 }, new int[] { 3, 1, 1 }, new int[] { 4, 3 },
                new int[] { 4, 8 }, new int[] { 3, 8 }, new int[] { 4, 1, 5, 2 },
                new int[] { 1, 1, 1, 1 }, new int[] { 7, 1 }, new int[] { 1, 1, 2, 3 }
            };
            int[][] rows = new int[][]
            {
                new int[] { 2, 3, 1 }, new int[] { 2, 3 }, new int[] { 2, 4, 1 },
                new int[] { 1, 6, 1, 1 }, new int[] { 5, 3 }, new int[] { 6, 1, 1 },
                new int[] { 4, 1 }, new int[] { 1, 8 }, new int[] { 1, 1, 4, 2 },
                new int[] { 3, 5 }, new int[] { 3, 1, 3 }, new int[] { 3, 4 },
                new int[] { 2, 1, 2, 1 }, new int[] { 2, 2, 3, 1 }, new int[] { 2, 5, 3, 2 }
            };
            var expected = new int[,]
            {
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0 },
                { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 },
                { 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
                { 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1 },
                { 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 },
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_20x20_A()
        {
            // arrange
            // 20x20
            int[][] columns = new int[][]
            {
                new int[] { 1, 2, 1 }, new int[] { 1, 1 }, new int[] { 7, 2 },
                new int[] { 7, 1, 1, 2 }, new int[] { 7, 1, 1 }, new int[] { 6, 5, 1 },
                new int[] { 4, 1, 3, 1, 1 }, new int[] { 2, 1, 2, 3 }, new int[] { 1, 1, 3, 2, 2 },
                new int[] { 9, 2 }, new int[] { 3, 5 }, new int[] { 1, 7, 3 },
                new int[] { 2, 8, 2 }, new int[] { 1, 4, 1, 1, 2 }, new int[] { 3, 3, 1, 1 },
                new int[] { 4, 2, 5 }, new int[] { 5, 4, 7 }, new int[] { 1, 12 },
                new int[] { 10, 1, 2 }, new int[] { 2, 9, 1, 2 }
            };
            int[][] rows = new int[][]
            {
                new int[] { 7, 1, 3, 1 }, new int[] { 7, 1, 5, 1 }, new int[] { 5, 1, 2, 3 },
                new int[] { 5, 2, 2 }, new int[] { 4, 2, 1 }, new int[] { 8, 2, 2 },
                new int[] { 3, 2, 2, 2 }, new int[] { 6, 4 }, new int[] { 11 },
                new int[] { 1, 3, 11 }, new int[] { 3, 5, 4 }, new int[] { 1, 4, 4, 3 },
                new int[] { 1, 1, 2, 4 }, new int[] { 2, 5 }, new int[] { 1, 1, 5 },
                new int[] { 2, 3 }, new int[] { 1, 3, 2, 6 }, new int[] { 1, 1, 3 },
                new int[] { 2, 3, 4, 4 }, new int[] { 3, 1, 3, 2 }
            };
            var expected = new int[,]
            {
                { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, },
                { 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_20x20_B()
        {
            // arrange
            // 20x20
            int[][] columns = new int[][]
            {
                new int[] { 5 }, new int[] { 5, 3, 2 }, new int[] { 7, 3, 6 },
                new int[] { 2, 3, 2, 1 }, new int[] { 1, 1, 2, 1 }, new int[] { 1, 1, 3, 1 },
                new int[] { 2, 4, 3 }, new int[] { 3, 2, 3 }, new int[] { 4, 2, 6 },
                new int[] { 3, 4, 1, 2 }, new int[] { 6, 4, 3, 1 }, new int[] { 4, 4, 2, 1 },
                new int[] { 2, 2, 1, 1 }, new int[] { 4, 1, 3 }, new int[] { 6, 2, 1, 1 },
                new int[] { 3, 3, 1, 1, 1 }, new int[] { 2, 1, 1, 1, 1 }, new int[] { 1, 2, 1 },
                new int[] { 3, 2 }, new int[] { 6, 1 },
            };
            int[][] rows = new int[][]
            {
                new int[] { 3 }, new int[] { 3, 4 }, new int[] { 3, 5, 3 },
                new int[] { 3, 1, 5, 2 }, new int[] { 2, 2, 5 }, new int[] { 2, 3, 2, 2 },
                new int[] { 2, 2, 5 }, new int[] { 3, 1, 2, 2 }, new int[] { 5, 1, 1, 1 },
                new int[] { 1, 2, 2, 1 }, new int[] { 1, 2, 1, 1, 2 }, new int[] { 1, 4, 3 },
                new int[] { 2, 1, 5, 1 }, new int[] { 1, 1, 1, 1, 2, 2 }, new int[] { 4, 1, 1, 2 },
                new int[] { 1, 1, 4, 1 }, new int[] { 1, 1, 2, 2, 2 }, new int[] { 1, 1, 5, 3 },
                new int[] { 3, 1, 2, 1 }, new int[] { 6, 9, 1 }
            };
            var expected = new int[,]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1 },
                { 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0 },
                { 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0 },
                { 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }


        [TestMethod()]
        public void SolveTest_4x4_A()
        {
            // arrange
            // 4x4
            var columns = new int[][]
            {
                new [] { 4 },
                new [] { 3 },
                new [] { 2 },
                new [] { 1, 1 },
            };
            var rows = new int[][]
            {
                new [] { 1, 1 },
                new [] { 2 },
                new [] { 3 },
                new [] { 4 },
            };
            var expected = new int[,]
            {
                { 1, 0, 0, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_5x5_B()
        {
            // arrange
            // 5x5
            var columns = new int[][]
            {
                new [] { 1 },
                new [] { 3 },
                new [] { 2 },
                new [] { 5 },
                new [] { 1 },
            };
            var rows = new int[][]
            {
                new [] { 1 },
                new [] { 1, 3 },
                new [] { 3 },
                new [] { 1, 1 },
                new [] { 1, 1 },
            };
            var expected = new int[,]
            {
                { 0, 0, 0, 1, 0 },
                { 1, 0, 1, 1, 1 },
                { 0, 1, 1, 1, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 1, 0, 1, 0 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_10x10_A()
        {
            // arrange
            // 10x10
            var columns = new int[][]
            {
                new [] { 2 },
                new [] { 4 },
                new [] { 4 },
                new [] { 8 },
                new [] { 1, 1 },
                new [] { 1, 1 },
                new [] { 1, 1, 2 },
                new [] { 1, 1, 4 },
                new [] { 1, 1, 4 },
                new [] { 8 },
            };
            var rows = new int[][]
            {
                new [] { 4 },
                new [] { 3, 1 },
                new [] { 1, 3 },
                new [] { 4, 1 },
                new [] { 1, 1 },
                new [] { 1, 3 },
                new [] { 3, 4 },
                new [] { 4, 4 },
                new [] { 4, 2 },
                new [] { 2 },
            };
            var expected = new int[,]
            {
                { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                { 0, 0, 0, 1, 1, 1, 0, 0, 0, 1 },
                { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1 },
                { 0, 0, 0, 1, 1, 1, 1, 0, 0, 1 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1 },
                { 0, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 0, 0, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_15x15_B()
        {
            // arrange
            // 15x15
            var columns = new int[][]
            {
                new int[] { 3 },
                new int[] { 4 },
                new int[] { 1, 4, 3 },
                new int[] { 1, 4 },
                new int[] { 7, 5 },
                new int[] { 2, 4 },
                new int[] { 4, 4 },
                new int[] { 2, 6 },
                new int[] { 4 },
                new int[] { 1, 4, 3 },
                new int[] { 1, 4 },
                new int[] { 7, 2 },
                new int[] { 2, 4, 1 },
                new int[] { 4, 3, 1 },
                new int[] { 2, 7 },
            };
            var rows = new int[][]
            {
                new [] { 1, 1, 1, 1 },
                new [] { 1, 3, 1, 3 },
                new [] { 5, 5 },
                new [] { 1, 2, 1, 2 },
                new [] { 1, 1 },
                new [] { 2, 2 },
                new [] { 2, 2 },
                new [] { 2, 2 },
                new [] { 2, 2, 1 },
                new [] { 3, 4, 3 },
                new [] { 3, 6, 4 },
                new [] { 2, 5, 4 },
                new [] { 2, 6, 1, 1 },
                new [] { 1, 2, 1, 1, 1 },
                new [] { 1, 1, 1, 1, 3 },
            };
            var expected = new int[,]
            {
                { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0 },
                { 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1 },
                { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
                { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1 },
                { 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 1 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_15x15_C()
        {
            // arrange
            // 15x15
            var columns = new int[][]
            {
                new int[] { 2, 6, 3 },
                new int[] { 4, 6, 1 },
                new int[] { 7, 1 },
                new int[] { 6, 9, 1 },
                new int[] { 6, 12 },
                new int[] { 2, 12, 2 },
                new int[] { 14, 1, 1 },
                new int[] { 4, 7, 2, 2 },
                new int[] { 3, 8, 3 },
                new int[] { 7, 6, 4 },
                new int[] { 3, 2, 4, 1, 2 },
                new int[] { 2, 3, 3, 1, 1 },
                new int[] { 1, 2, 3, 2, 2, 2 },
                new int[] { 1, 8, 1, 1, 3 },
                new int[] { 1, 2, 2, 2, 2 },
            };
            var rows = new int[][]
            {
                new int [] { 1, 1, 1, 1 },
                new int [] { 2, 2, 1, 1 },
                new int [] { 7, 2, 1 },
                new int [] { 8, 3 },
                new int [] { 2, 2, 2, 1 },
                new int [] { 1, 8, 1 },
                new int [] { 2, 3, 2, 3 },
                new int [] { 1, 4, 4 },
                new int [] { 1, 10 },
                new int [] { 2, 9, 1 },
                new int [] { 1, 8, 1 },
                new int [] { 1, 9 },
                new int [] { 11, 3 },
                new int [] { 8, 4, 1 },
                new int [] { 7, 2, 1 },
                new int [] { 7, 2, 1 },
                new int [] { 5, 4, 3 },
                new int [] { 2, 2, 1, 2, 1, 1 },
                new int [] { 1, 1, 2, 4, 2 },
                new int [] { 2, 5, 4 },
            };
            var expected = new int[,]
            {
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 },
                { 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0 },
                { 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0 },
                { 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0 },
                { 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0 },
                { 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_20x20_C()
        {
            // arrange
            // 20x20
            var columns = new int[][]
            {
                new int[] { 1, 1 },
                new int[] { 1, 2, 1 },
                new int[] { 3, 2 },
                new int[] { 1, 1, 1, 6 },
                new int[] { 3, 4 },
                new int[] { 3, 2 },
                new int[] { 3, 1, 2, 2 },
                new int[] { 1, 2, 1, 2, 3 },
                new int[] { 2, 3, 3, 4 },
                new int[] { 4, 2, 9 },
                new int[] { 7, 9 },
                new int[] { 4, 2, 9 },
                new int[] { 3, 1, 9 },
                new int[] { 3, 2, 8 },
                new int[] { 4, 5, 3 },
                new int[] { 10, 1 },
                new int[] { 10 },
                new int[] { 10 },
                new int[] { 9 },
                new int[] { 3 },
            };
            var rows = new int[][]
            {
                new int[] { 1, 6 },
                new int[] { 1, 8 },
                new int[] { 1, 10 },
                new int[] { 3, 3 },
                new int[] { 3, 1, 1, 1, 3 },
                new int[] { 15 },
                new int[] { 8, 5 },
                new int[] { 7 },
                new int[] { 1, 9 },
                new int[] { 12 },
                new int[] { 6, 4 },
                new int[] { 8, 3 },
                new int[] { 2, 7, 3 },
                new int[] { 2, 1, 6, 3 },
                new int[] { 4, 6, 2 },
                new int[] { 3, 6 },
                new int[] { 9 },
                new int[] { 7 },
                new int[] { 3 },
                new int[] { 4 },
            };
            var expected = new int[,]
            {
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1 },
                { 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1 },
                { 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void SolveTest_3x3_A()
        {
            // arrange
            // 5x5
            var columns = new int[][]
            {
                new [] { 1 },
                new [] { 1, 1 },
                new [] { 2 }
            };
            var rows = new int[][]
            {
                new [] { 1 },
                new [] { 1, 1 },
                new [] { 2 }
            };
            var expected = new int[,]
            {
                { 0, 1, 0 },
                { 1, 0, 1 },
                { 0, 1, 1 }
            };

            // act
            var run = new NonogramSolver(columns, rows);
            var actual = run.Solve();

            // assert
            var isEqual = SequenceEquals(expected, actual);
            Assert.IsTrue(isEqual);
        }
    }
}