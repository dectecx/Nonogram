#include <stdio.h>
#include <stdlib.h>

typedef int bool;
#define false 0  
#define true  1  

/* 5x5 ���� */
#define mapsize 5
#define rulelength 3
// Columns Rules
int columns[mapsize][rulelength] = { {2,0,0},{3,0,0},{2,0,0},{3,0,0},{3,0,0} };
// Row Rules
int rows[mapsize][rulelength] = { {3,0,0},{3,0,0},{2,2,0},{2,0,0},{1,0,0} };
/* 15x15 ���� */
/*
#define mapsize 15
#define rulelength 5
// Columns Rules
int columns[mapsize][rulelength] = { {4,7,0,0,0},{3,6,0,0,0},{3,0,0,0,0},{2,1,0,0,0},{6,1,0,0,0},{4,3,0,0,0},{4,1,2,0,0},{3,1,1,0,0},{4,3,0,0,0},{4,8,0,0,0},{3,8,0,0,0},{4,1,5,2,0},{1,1,1,1,0},{7,1,0,0,0},{1,1,2,3,0} };
// Row Rules
int rows[mapsize][rulelength]={{2,3,1,0,0},{2,3,0,0,0},{2,4,1,0,0},{1,6,1,1,0},{5,3,0,0,0},{6,1,1,0,0},{4,1,0,0,0},{1,8,0,0,},{1,1,4,2,0},{3,5,0,0,0},{3,1,3,0,0},{3,4,0,0,0},{2,1,2,1,0},{2,2,3,1,0},{2,5,3,2,0}};
*/
/* 20x20 ���� */
/*
#define mapsize 20
#define rulelength 6
// Columns Rules
int columns[mapsize][rulelength] = { {1,2,1,0,0,0},{1,1,0,0,0,0},{7,2,0,0,0,0},{7,1,1,2,0,0},{7,1,1,0,0,0},{6,5,1,0,0,0},{4,1,3,1,1,0},{2,1,2,3,0,0},{1,1,3,2,2,0},{9,2,0,0,0,0},{3,5,0,0,0,0},{1,7,3,0,0,0},{2,8,2,0,0,0},{1,4,1,1,2,0},{3,3,1,1,0,0},{4,2,5,0,0,0},{5,4,7,0,0,0},{1,12,0,0,0,0},{10,1,2,0,0,0},{2,9,1,2,0,0} };
// Row Rules
int rows[mapsize][rulelength] = { {7,1,3,1,0,0},{7,1,5,1,0,0},{5,1,2,3,0,0},{5,2,2,0,0,0},{4,2,1,0,0,0},{8,2,2,0,0,0},{3,2,2,2,0,0},{6,4,0,0,0,0},{11,0,0,0,0,0},{1,3,11,0,0,0},{3,5,4,0,0,0},{1,4,4,3,0,0},{1,1,2,4,0,0},{2,5,0,0,0,0},{1,1,5,0,0,0},{2,3,0,0,0,0},{1,3,2,6,0,0},{1,1,3,0,0,0},{2,3,4,4,0,0},{3,1,3,2,0,0} };
*/

// �e��
int width = mapsize;
// ����
int height = mapsize;
// �G���}�C - �ѹϭ��O
int board[mapsize][mapsize];

bool Solve();
bool FindSolution(int h, int w);
bool Verify(int h, int w);
bool VerifyRow(int* requirements, int maxLineCount, int maxLength, int length, char isColRow, int currentStep);
void PrintBoard();
void WriteBoardToFile();

// �D�{��
int main() {
	printf("----------\n");
	// ��l�Ƹѹϭ��O
	int h = 0;
	int w = 0;
	for (h = 0; h < mapsize; h++) {
		for (w = 0; w < mapsize; w++) {
			board[h][w] = 0;
		}
	}
	if (Solve()) {
		printf("Success\n----------\n");
		// ��X���G
		PrintBoard();
		// �g��
		WriteBoardToFile();
	}
	else {
		printf("Fail\n----------\n");
	}
	system("pause");
}

// ����D��
bool Solve() {
	// �q�Ĥ@��}�l�D��
	if (FindSolution(0, 0)) {
		return true;
	}
	// �D�ѥ���
	return false;
}

// �M��ѵ�
// h:�����ĴX��
// w:�����ĴX��
bool FindSolution(int h, int w) {
	// �ثe���צ�m�w����̤j����
	if (h == height) {
		return true;
	}

	// �U�@�ӨD�Ѱ��צ�m
	int nextH = h + (w + 1 == width ? 1 : 0);
	// �U�@�ӨD�Ѽe�צ�m
	int nextW = (w + 1) % width;
	// �����ռаO��1
	board[h][w] = 1;
	// �����ҬO�_�ŦX��C���W�h�A�Y�ŦX�~���U���j(�`���u��DFS)
	if (Verify(h, w) && FindSolution(nextH, nextW)) {
		return true;
	}
	// ����l�w���i�ର1�A�аO��0
	board[h][w] = 0;
	if (Verify(h, w) && FindSolution(nextH, nextW)) {
		return true;
	}
	// �ثe����аO������(�t����)�����~�A��^���j
	return false;
}

// ���ҬO�_�ŦX��C���W�h
// h: �����ĴX��
// w: �����ĴX��
bool Verify(int h, int w) {
	// ���o���w���e�צ�m���Ʋդ�����0�ƶq
	int colArrLength = 0;
	// ���o���w�����צ�m���Ʋդ�����0�ƶq
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
		// ���ҫ��w���e�צ�m���Ʋ�
		VerifyRow(columns[w], colArrLength, height, h, 'C', w) &&
		// ���ҫ��w�����צ�m���Ʋ�
		VerifyRow(rows[h], rowArrLength, width, w, 'R', h)
		);
}

// ���ҡu���w���e�צ�m���Ʋ� / ���w�����צ�m���Ʋաv�q�Ĥ@���u�ثe���צ�m / �ثe�e�צ�m�v�O�_���ŦX��C���W�h
// requirements: ���w���e�צ�m���Ʋ� / ���w�����צ�m���Ʋ�
// maxLineCount: ���w���e�צ�m���Ʋդ�����0�ƶq / ���w�����צ�m���Ʋդ�����0�ƶq
// maxLength: �̤j�e�� / �̤j����
// length: �ثe���צ�m / �ثe�e�צ�m
// isColRow: C:���w���e�צ�m���Ʋ� R:���w�����צ�m���Ʋ�
// currentStep: ���w���e�צ�m / ���w�����צ�m>
bool VerifyRow(int* requirements, int maxLineCount, int maxLength, int length, char isColRow, int currentStep) {
	// �˴��쪺�u�q�ƶq
	int lineCount = 0;
	// �˴��쪺�u�q����
	int lineLen = 0;
	// �O�_���b�e�u�q
	bool isLast = false;
	// �q�Ĥ@���u�ثe���צ�m / �ثe�e�צ�m�v�̧��ˬd
	int i = 0;
	for (i = 0; i <= length; i++) {
		// ��e�j���ˬd�쪺��l��1�A��ܬO�u�q
		int current = isColRow == 'C' ? board[i][currentStep] : board[currentStep][i];
		if (current == 1) {
			// �u�q����+1
			lineLen++;
			if (!isLast) {
				// �ˬd�u�q�ƬO�_�W�L�W�d���ռ�
				if (lineCount >= maxLineCount) {
					return false;
				}
			}
			// �]���w���ˬd��1�A��ܤw�}�l�e�u�q�A�е����u�q
			isLast = true;
		}
		// ��e�j���ˬd�쪺��l��0�A��ܽu�q�_��or�S���u�q
		else {
			// �P�_�O�u�q�_��(isLast == true) or �S���u�q(isLast == false)
			if (isLast) {
				// �ˬd��e�u�q�O�_����W�d������
				if (lineLen != requirements[lineCount]) {
					return false;
				}
				// �k�s�u�q����
				lineLen = 0;
				// �֭p�u�q��+1
				lineCount++;
			}
			// �е��S���u�q
			isLast = false;
		}
	}

	// �O�_�w���Ҩ�Ӱ���/�e�׳̫�@��
	if (length == maxLength - 1) {
		// �p�G�O�u�q���ܡA�n�ˬd��e�u�q�O�_����W�d������
		if (isLast) {
			// �ˬd�u�q�ƬO�_����W�d���ռ� && �ˬd��e�u�q�O�_����W�d������
			return lineCount == maxLineCount - 1 && lineLen == requirements[lineCount];
		}
		else {
			// �ˬd�u�q�ƬO�_����W�d���ռ�
			return lineCount == maxLineCount;
		}
	}
	else {
		// �p�G�O�u�q���ܡA�n�ˬd��e�u�q�O�_�p��W�d������
		if (isLast) {
			// �ˬd��e�u�q�O�_�p��W�d������
			return lineLen <= requirements[lineCount];
		}
	}
	// �H�W�ˬd���Ƴq�L
	return true;
}

// �L�X�G���}�C - �ѹϭ��O
void PrintBoard() {
	// �����j��
	int h = 0;
	for (h = 0; h < mapsize; h++) {
		// �����j��
		int w = 0;
		for (w = 0; w < mapsize; w++) {
			printf(board[h][w] == 1 ? "1" : "0");
		}
		printf("\n");
	}
	printf("----------\n");
}

// �g�ɤG���}�C - �ѹϭ��O
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