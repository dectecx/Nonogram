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

// �e��
int width = 5;

// ����
int height = 5;

// �G���}�C - �ѹϭ��O
int board[5][5];

bool Solve();
bool FindSolution(int h, int w);
bool Verify(int h, int w);
bool VerifyRow(int requirements[], int maxLength, int length, char isColRow, int currentStep);
void Print(int array2D[][5]);

/// <summary>
/// �D�{��
/// </summary>
int main()
{
	Solve();
}

/// <summary>
/// ����D��
/// </summary>
bool Solve()
{
	// �q�Ĥ@��}�l�D��
	if (FindSolution(0, 0))
	{
		// ��X���G
		Print(board);
		return true;
	}
	// �D�ѥ���
	return false;
}

/// <summary>
/// �M��ѵ�
/// </summary>
/// <param name="h">�����ĴX��</param>
/// <param name="w">�����ĴX��</param>
bool FindSolution(int h, int w)
{
	// �ثe���צ�m�w����̤j����
	if (h == height)
		return true;

	// �U�@�ӨD�Ѱ��צ�m
	int nextH = h + (w + 1 == width ? 1 : 0);
	// �U�@�ӨD�Ѽe�צ�m
	int nextW = (w + 1) % width;
	// �����ռаO��1
	board[h][w] = 1;
	// �����ҬO�_�ŦX��C���W�h�A�Y�ŦX�~���U���j(�`���u��DFS)
	if (Verify(h, w) && FindSolution(nextH, nextW))
	{
		return true;
	}
	// ����l�w���i�ର1�A�аO��0
	board[h][w] = 0;
	if (Verify(h, w) && FindSolution(nextH, nextW))
	{
		return true;
	}
	// �ثe����аO������(�t����)�����~�A��^���j
	return false;
}

/// <summary>
/// ���ҬO�_�ŦX��C���W�h
/// </summary>
/// <param name="h">�����ĴX��</param>
/// <param name="w">�����ĴX��</param>
bool Verify(int h, int w)
{
	return (
		// ���ҫ��w���e�צ�m���Ʋ�
		VerifyRow(xRequirements[w], height, h, 'C', w) &&
		// ���ҫ��w�����צ�m���Ʋ�
		VerifyRow(yRequirements[h], width, w, 'R', h)
		);
}

/// <summary>
/// ���ҡu���w���e�צ�m���Ʋ� / ���w�����צ�m���Ʋաv�q�Ĥ@���u�ثe���צ�m / �ثe�e�צ�m�v�O�_���ŦX��C���W�h
/// </summary>
/// <param name="requirements">���w���e�צ�m���Ʋ� / ���w�����צ�m���Ʋ�</param>
/// <param name="maxLength">�̤j�e�� / �̤j����</param>
/// <param name="length">�ثe���צ�m / �ثe�e�צ�m</param>
/// <param name="isColRow">C:���w���e�צ�m���Ʋ� R:���w�����צ�m���Ʋ�</param>
/// <param name="currentStep">���w���e�צ�m / ���w�����צ�m></param>
bool VerifyRow(int requirements[], int maxLength, int length, char isColRow, int currentStep)
{
	// �˴��쪺�u�q�ƶq
	int k = 0;
	// �˴��쪺�u�q����
	int acc = 0;
	// �O�_���b�e�u�q
	bool isLast = false;
	// �q�Ĥ@���u�ثe���צ�m / �ثe�e�צ�m�v�̧��ˬd
	for (int i = 0; i <= length; i++)
	{
		// ��e�j���ˬd�쪺��l��1�A��ܬO�u�q
		int current = isColRow == 'C' ? board[i][currentStep] : board[currentStep][i];
		if (current == 1)
		{
			// �u�q����+1
			acc++;
			if (!isLast)
			{
				// �ˬd�u�q�ƬO�_�W�L�W�d���ռ�
				if (k >= sizeof(requirements) / sizeof(*requirements))
				{
					return false;
				}
			}
			// �]���w���ˬd��1�A��ܤw�}�l�e�u�q�A�е����u�q
			isLast = true;
		}
		// ��e�j���ˬd�쪺��l��0�A��ܽu�q�_��or�S���u�q
		else
		{
			// �P�_�O�u�q�_��(isLast == true) or �S���u�q(isLast == false)
			if (isLast)
			{
				// �ˬd��e�u�q�O�_����W�d������
				if (acc != requirements[k])
				{
					return false;
				}
				// �k�s�u�q����
				acc = 0;
				// �֭p�u�q��+1
				k++;
			}
			// �е��S���u�q
			isLast = false;
		}
	}

	// �O�_�w���Ҩ�Ӱ���/�e�׳̫�@��
	if (length == maxLength - 1)
	{
		// �p�G�O�u�q���ܡA�n�ˬd��e�u�q�O�_����W�d������
		if (isLast)
		{
			// �ˬd�u�q�ƬO�_����W�d���ռ� && �ˬd��e�u�q�O�_����W�d������
			return k == sizeof(requirements) / sizeof(*requirements) - 1 && acc == requirements[k];
		}
		else
		{
			// �ˬd�u�q�ƬO�_����W�d���ռ�
			return k == sizeof(requirements) / sizeof(*requirements);
		}
	}
	else
	{
		// �p�G�O�u�q���ܡA�n�ˬd��e�u�q�O�_�p��W�d������
		if (isLast)
		{
			// �ˬd��e�u�q�O�_�p��W�d������
			return acc <= requirements[k];
		}
	}
	// �H�W�ˬd���Ƴq�L
	return true;
}

/// <summary>
/// �L�X�G���}�C
/// </summary>
/// <param name="array2D">�G���}�C</param>
void Print(int array2D[][5])
{
	// �����j��
	for (int h = 0; h < sizeof(array2D); h++)
	{
		// �����j��
		for (int w = 0; w < sizeof(array2D[h]); w++)
		{
			printf(array2D[h, w] == 1 ? "��" : "�@");
			//printf(array2D[h, w] == 1 ? "\x25A0" : "�@");
		}
		printf("\n");
	}
}