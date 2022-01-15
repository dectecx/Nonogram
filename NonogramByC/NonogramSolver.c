#include <stdio.h>

typedef int bool;
#define false 0  
#define true  1  

int rows[5][3] = { {3,0,0},{3,0,0},{2,2,0},{2,0,0},{1,0,0} };
int columns[5][3] = { {2,0,0},{3,0,0},{2,0,0},{3,0,0},{3,0,0} };

// Columns Rules
int xRequirements[5][5] = { {2},{3},{2},{3},{3} };

// Row Rules
int yRequirements[5][5] = { {3},{3},{2,2},{2},{1} };

// 寬度
int width = 5;

// 高度
int height = 5;

// 二維陣列 - 解圖面板
int board[5][5];

bool Solve();
bool FindSolution(int h, int w);
bool Verify(int h, int w);
bool VerifyRow(int requirements[], int maxLength, int length, char isColRow, int currentStep);
void Print(int array2D[][5]);

/// <summary>
/// 主程式
/// </summary>
int main()
{
	Solve();
}

/// <summary>
/// 執行求解
/// </summary>
bool Solve()
{
	// 從第一格開始求解
	if (FindSolution(0, 0))
	{
		// 輸出結果
		Print(board);
		return true;
	}
	// 求解失敗
	return false;
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
	board[h][w] = 1;
	// 先驗證是否符合行列的規則，若符合才往下遞迴(深度優先DFS)
	if (Verify(h, w) && FindSolution(nextH, nextW))
	{
		return true;
	}
	// 此格子已不可能為1，標記為0
	board[h][w] = 0;
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
		VerifyRow(xRequirements[w], height, h, 'C', w) &&
		// 驗證指定的高度位置的數組
		VerifyRow(yRequirements[h], width, w, 'R', h)
		);
}

/// <summary>
/// 驗證「指定的寬度位置的數組 / 指定的高度位置的數組」從第一格到「目前高度位置 / 目前寬度位置」是否都符合行列的規則
/// </summary>
/// <param name="requirements">指定的寬度位置的數組 / 指定的高度位置的數組</param>
/// <param name="maxLength">最大寬度 / 最大高度</param>
/// <param name="length">目前高度位置 / 目前寬度位置</param>
/// <param name="isColRow">C:指定的寬度位置的數組 R:指定的高度位置的數組</param>
/// <param name="currentStep">指定的寬度位置 / 指定的高度位置></param>
bool VerifyRow(int requirements[], int maxLength, int length, char isColRow, int currentStep)
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
		int current = isColRow == 'C' ? board[i][currentStep] : board[currentStep][i];
		if (current == 1)
		{
			// 線段長度+1
			acc++;
			if (!isLast)
			{
				// 檢查線段數是否超過規範的組數
				if (k >= sizeof(requirements) / sizeof(*requirements))
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
			return k == sizeof(requirements) / sizeof(*requirements) - 1 && acc == requirements[k];
		}
		else
		{
			// 檢查線段數是否等於規範的組數
			return k == sizeof(requirements) / sizeof(*requirements);
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
void Print(int array2D[][5])
{
	// 垂直迴圈
	for (int h = 0; h < sizeof(array2D); h++)
	{
		// 水平迴圈
		for (int w = 0; w < sizeof(array2D[h]); w++)
		{
			printf(array2D[h, w] == 1 ? "Ｘ" : "　");
			//printf(array2D[h, w] == 1 ? "\x25A0" : "　");
		}
		printf("\n");
	}
}