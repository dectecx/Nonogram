using System;

namespace Nonogram
{
    public class NonogramSolver
    {
        /// <summary>
        /// Columns Rules
        /// </summary>
        int[][] xRequirements;

        /// <summary>
        /// Row Rules
        /// </summary>
        int[][] yRequirements;

        /// <summary>
        /// 寬度
        /// </summary>
        int width;

        /// <summary>
        /// 高度
        /// </summary>
        int height;

        /// <summary>
        /// 二維陣列 - 解圖面板
        /// </summary>
        int[,] board;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="xRequirements">各寬度位置的數組</param>
        /// <param name="yRequirements">各高度位置的數組</param>
        public NonogramSolver(int[][] xRequirements, int[][] yRequirements)
        {
            // 參數注入
            this.xRequirements = xRequirements;
            this.yRequirements = yRequirements;
            this.width = xRequirements.Length;
            this.height = yRequirements.Length;

            // 初始化二維陣列
            board = new int[height, width];
        }

        /// <summary>
        /// 執行求解
        /// </summary>
        public int[,] Solve()
        {
            // 從第一格開始求解
            if (FindSolution(0, 0))
            {
                // 輸出結果
                Print(board);
                return board;
            }
            // 求解失敗
            return null;
        }

        /// <summary>
        /// 尋找解答
        /// </summary>
        /// <param name="h">垂直第幾格</param>
        /// <param name="w">水平第幾格</param>
        bool FindSolution(int h, int w)
        {
            // 目前高度位置已等於最大高度
            if (h == height)
                return true;

            // 下一個求解高度位置
            int nextH = h + (w + 1 == width ? 1 : 0);
            // 下一個求解寬度位置
            int nextW = (w + 1) % width;
            // 先嘗試標記為1
            board[h, w] = 1;
            // 先驗證是否符合行列的規則，若符合才往下遞迴(深度優先DFS)
            if (Verify(h, w) && FindSolution(nextH, nextW))
            {
                return true;
            }
            // 此格子已不可能為1，標記為0
            board[h, w] = 0;
            if (Verify(h, w) && FindSolution(nextH, nextW))
            {
                return true;
            }
            // 目前為止標記的答案(含此格)有錯誤，返回遞迴
            return false;
        }

        /// <summary>
        /// 驗證是否符合行列的規則
        /// </summary>
        /// <param name="h">垂直第幾格</param>
        /// <param name="w">水平第幾格</param>
        bool Verify(int h, int w)
        {
            return (
                // 驗證指定的寬度位置的數組
                VerifyRow(xRequirements[w], height, h, idx => board[idx, w]) &&
                // 驗證指定的高度位置的數組
                VerifyRow(yRequirements[h], width, w, idx => board[h, idx])
            );
        }

        /// <summary>
        /// 驗證「指定的寬度位置的數組 / 指定的高度位置的數組」從第一格到「目前高度位置 / 目前寬度位置」是否都符合行列的規則
        /// </summary>
        /// <param name="requirements">指定的寬度位置的數組 / 指定的高度位置的數組</param>
        /// <param name="maxLength">最大寬度 / 最大高度</param>
        /// <param name="length">目前高度位置 / 目前寬度位置</param>
        /// <param name="getter"></param>
        bool VerifyRow(int[] requirements, int maxLength, int length, Func<int, int> getter)
        {
            // 檢測到的線段數量
            int k = 0;
            // 檢測到的線段長度
            int acc = 0;
            // 是否正在畫線段
            bool isLast = false;
            // 從第一格到「目前高度位置 / 目前寬度位置」依序檢查
            for (int i = 0; i <= length; i++)
            {
                // 當前迴圈檢查到的格子為1，表示是線段
                if (getter(i) == 1)
                {
                    // 線段長度+1
                    acc++;
                    if (!isLast)
                    {
                        // 檢查線段數是否超過規範的組數
                        if (k >= requirements.Length)
                        {
                            return false;
                        }
                    }
                    // 因為已有檢查到1，表示已開始畫線段，標註有線段
                    isLast = true;
                }
                // 當前迴圈檢查到的格子為0，表示線段斷掉or沒有線段
                else
                {
                    // 判斷是線段斷掉(isLast == true) or 沒有線段(isLast == false)
                    if (isLast)
                    {
                        // 檢查當前線段是否等於規範的長度
                        if (acc != requirements[k])
                        {
                            return false;
                        }
                        // 歸零線段長度
                        acc = 0;
                        // 累計線段數+1
                        k++;
                    }
                    // 標註沒有線段
                    isLast = false;
                }
            }

            // 是否已驗證到該高度/寬度最後一格
            if (length == maxLength - 1)
            {
                // 如果是線段的話，要檢查當前線段是否等於規範的長度
                if (isLast)
                {
                    // 檢查線段數是否等於規範的組數 && 檢查當前線段是否等於規範的長度
                    return k == requirements.Length - 1 && acc == requirements[k];
                }
                else
                {
                    // 檢查線段數是否等於規範的組數
                    return k == requirements.Length;
                }
            }
            else
            {
                // 如果是線段的話，要檢查當前線段是否小於規範的長度
                if (isLast)
                {
                    // 檢查當前線段是否小於規範的長度
                    return acc <= requirements[k];
                }
            }
            // 以上檢查全數通過
            return true;
        }

        /// <summary>
        /// 印出二維陣列
        /// </summary>
        /// <param name="array2D">二維陣列</param>
        void Print(int[,] array2D)
        {
            // 垂直迴圈
            for (int h = 0; h < array2D.GetLength(0); h++)
            {
                // 水平迴圈
                for (int w = 0; w < array2D.GetLength(1); w++)
                {
                    Console.Write(array2D[h, w] == 1 ? "\x25A0" : "　");
                }
                Console.WriteLine();
            }
        }
    }
}