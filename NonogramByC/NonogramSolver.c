#include <stdio.h>
#include <stdlib.h>

typedef int bool;
#define false 0  
#define true  1  

/* 5x5 測資 */
#define mapsize 5
#define rulelength 3
// Columns Rules
int columns[mapsize][rulelength] = { {2,0,0},{3,0,0},{2,0,0},{3,0,0},{3,0,0} };
// Row Rules
int rows[mapsize][rulelength] = { {3,0,0},{3,0,0},{2,2,0},{2,0,0},{1,0,0} };
/* 15x15 測資 */
/*
#define mapsize 15
#define rulelength 5
// Columns Rules
int columns[mapsize][rulelength] = { {4,7,0,0,0},{3,6,0,0,0},{3,0,0,0,0},{2,1,0,0,0},{6,1,0,0,0},{4,3,0,0,0},{4,1,2,0,0},{3,1,1,0,0},{4,3,0,0,0},{4,8,0,0,0},{3,8,0,0,0},{4,1,5,2,0},{1,1,1,1,0},{7,1,0,0,0},{1,1,2,3,0} };
// Row Rules
int rows[mapsize][rulelength]={{2,3,1,0,0},{2,3,0,0,0},{2,4,1,0,0},{1,6,1,1,0},{5,3,0,0,0},{6,1,1,0,0},{4,1,0,0,0},{1,8,0,0,},{1,1,4,2,0},{3,5,0,0,0},{3,1,3,0,0},{3,4,0,0,0},{2,1,2,1,0},{2,2,3,1,0},{2,5,3,2,0}};
*/
/* 20x20 測資 */
/*
#define mapsize 20
#define rulelength 6
// Columns Rules
int columns[mapsize][rulelength] = { {1,2,1,0,0,0},{1,1,0,0,0,0},{7,2,0,0,0,0},{7,1,1,2,0,0},{7,1,1,0,0,0},{6,5,1,0,0,0},{4,1,3,1,1,0},{2,1,2,3,0,0},{1,1,3,2,2,0},{9,2,0,0,0,0},{3,5,0,0,0,0},{1,7,3,0,0,0},{2,8,2,0,0,0},{1,4,1,1,2,0},{3,3,1,1,0,0},{4,2,5,0,0,0},{5,4,7,0,0,0},{1,12,0,0,0,0},{10,1,2,0,0,0},{2,9,1,2,0,0} };
// Row Rules
int rows[mapsize][rulelength] = { {7,1,3,1,0,0},{7,1,5,1,0,0},{5,1,2,3,0,0},{5,2,2,0,0,0},{4,2,1,0,0,0},{8,2,2,0,0,0},{3,2,2,2,0,0},{6,4,0,0,0,0},{11,0,0,0,0,0},{1,3,11,0,0,0},{3,5,4,0,0,0},{1,4,4,3,0,0},{1,1,2,4,0,0},{2,5,0,0,0,0},{1,1,5,0,0,0},{2,3,0,0,0,0},{1,3,2,6,0,0},{1,1,3,0,0,0},{2,3,4,4,0,0},{3,1,3,2,0,0} };
*/

// 寬度
int width = mapsize;
// 高度
int height = mapsize;
// 二維陣列 - 解圖面板
int board[mapsize][mapsize];

bool Solve();
bool FindSolution(int h, int w);
bool Verify(int h, int w);
bool VerifyRow(int* requirements, int maxLineCount, int maxLength, int length, char isColRow, int currentStep);
void PrintBoard();
void WriteBoardToFile();

// 主程式
int main() {
	printf("----------\n");
	// 初始化解圖面板
	int h = 0;
	int w = 0;
	for (h = 0; h < mapsize; h++) {
		for (w = 0; w < mapsize; w++) {
			board[h][w] = 0;
		}
	}
	if (Solve()) {
		printf("Success\n----------\n");
		// 輸出結果
		PrintBoard();
		// 寫檔
		WriteBoardToFile();
	}
	else {
		printf("Fail\n----------\n");
	}
	system("pause");
}

// 執行求解
bool Solve() {
	// 從第一格開始求解
	if (FindSolution(0, 0)) {
		return true;
	}
	// 求解失敗
	return false;
}

// 尋找解答
// h:垂直第幾格
// w:水平第幾格
bool FindSolution(int h, int w) {
	// 目前高度位置已等於最大高度
	if (h == height) {
		return true;
	}

	// 下一個求解高度位置
	int nextH = h + (w + 1 == width ? 1 : 0);
	// 下一個求解寬度位置
	int nextW = (w + 1) % width;
	// 先嘗試標記為1
	board[h][w] = 1;
	// 先驗證是否符合行列的規則，若符合才往下遞迴(深度優先DFS)
	if (Verify(h, w) && FindSolution(nextH, nextW)) {
		return true;
	}
	// 此格子已不可能為1，標記為0
	board[h][w] = 0;
	if (Verify(h, w) && FindSolution(nextH, nextW)) {
		return true;
	}
	// 目前為止標記的答案(含此格)有錯誤，返回遞迴
	return false;
}

// 驗證是否符合行列的規則
// h: 垂直第幾格
// w: 水平第幾格
bool Verify(int h, int w) {
	// 取得指定的寬度位置的數組中不為0數量
	int colArrLength = 0;
	// 取得指定的高度位置的數組中不為0數量
	int rowArrLength = 0;
	int i = 0;
	for (i = 0; i < rulelength; i++) {
		if (columns[w][i] != 0) {
			colArrLength++;
		}
		if (rows[h][i] != 0) {
			rowArrLength++;
		}
	}
	return (
		// 驗證指定的寬度位置的數組
		VerifyRow(columns[w], colArrLength, height, h, 'C', w) &&
		// 驗證指定的高度位置的數組
		VerifyRow(rows[h], rowArrLength, width, w, 'R', h)
		);
}

// 驗證「指定的寬度位置的數組 / 指定的高度位置的數組」從第一格到「目前高度位置 / 目前寬度位置」是否都符合行列的規則
// requirements: 指定的寬度位置的數組 / 指定的高度位置的數組
// maxLineCount: 指定的寬度位置的數組中不為0數量 / 指定的高度位置的數組中不為0數量
// maxLength: 最大寬度 / 最大高度
// length: 目前高度位置 / 目前寬度位置
// isColRow: C:指定的寬度位置的數組 R:指定的高度位置的數組
// currentStep: 指定的寬度位置 / 指定的高度位置>
bool VerifyRow(int* requirements, int maxLineCount, int maxLength, int length, char isColRow, int currentStep) {
	// 檢測到的線段數量
	int lineCount = 0;
	// 檢測到的線段長度
	int lineLen = 0;
	// 是否正在畫線段
	bool isLast = false;
	// 從第一格到「目前高度位置 / 目前寬度位置」依序檢查
	int i = 0;
	for (i = 0; i <= length; i++) {
		// 當前迴圈檢查到的格子為1，表示是線段
		int current = isColRow == 'C' ? board[i][currentStep] : board[currentStep][i];
		if (current == 1) {
			// 線段長度+1
			lineLen++;
			if (!isLast) {
				// 檢查線段數是否超過規範的組數
				if (lineCount >= maxLineCount) {
					return false;
				}
			}
			// 因為已有檢查到1，表示已開始畫線段，標註有線段
			isLast = true;
		}
		// 當前迴圈檢查到的格子為0，表示線段斷掉or沒有線段
		else {
			// 判斷是線段斷掉(isLast == true) or 沒有線段(isLast == false)
			if (isLast) {
				// 檢查當前線段是否等於規範的長度
				if (lineLen != requirements[lineCount]) {
					return false;
				}
				// 歸零線段長度
				lineLen = 0;
				// 累計線段數+1
				lineCount++;
			}
			// 標註沒有線段
			isLast = false;
		}
	}

	// 是否已驗證到該高度/寬度最後一格
	if (length == maxLength - 1) {
		// 如果是線段的話，要檢查當前線段是否等於規範的長度
		if (isLast) {
			// 檢查線段數是否等於規範的組數 && 檢查當前線段是否等於規範的長度
			return lineCount == maxLineCount - 1 && lineLen == requirements[lineCount];
		}
		else {
			// 檢查線段數是否等於規範的組數
			return lineCount == maxLineCount;
		}
	}
	else {
		// 如果是線段的話，要檢查當前線段是否小於規範的長度
		if (isLast) {
			// 檢查當前線段是否小於規範的長度
			return lineLen <= requirements[lineCount];
		}
	}
	// 以上檢查全數通過
	return true;
}

// 印出二維陣列 - 解圖面板
void PrintBoard() {
	// 垂直迴圈
	int h = 0;
	for (h = 0; h < mapsize; h++) {
		// 水平迴圈
		int w = 0;
		for (w = 0; w < mapsize; w++) {
			printf(board[h][w] == 1 ? "1" : "0");
		}
		printf("\n");
	}
	printf("----------\n");
}

// 寫檔二維陣列 - 解圖面板
void WriteBoardToFile() {
	FILE* output_file = NULL;
	errno_t err;
	char* filename = "output.txt";
	err = fopen_s(&output_file, filename, "w");
	if (err != 0) {
		printf("The file was not opened\n----------\n");
		return;
	}

	int h = 0;
	for (h = 0; h < mapsize; h++) {
		int w = 0;
		for (w = 0; w < mapsize; w++) {
			fprintf(output_file, board[h][w] == 1 ? "1" : "0");
		}
		fprintf(output_file, "\n");
	}
	if (output_file) {
		err = fclose(output_file);
		if (err != 0) {
			printf("The file was not closed\n----------\n");
		}
	}
	printf("Success Write File!\n----------\n");
}