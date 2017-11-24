#include <stdio.h>
#include <math.h>
#include <stdlib.h>

/**
 * Author: Jakub Mifek
 */

void
naive_transpose(size_t size) {
	if (size == 0)
		return;

	size -= 1;
	for (size_t y = 0; y <= size; y++) {
		for (size_t x = y + 1; x <= size; x++) {
			printf("X %d %d %d %d\n", y, x, x, y);
		}
	}
}

void
transpose_and_swap(
	size_t startX1, size_t endX1,
	size_t startY1, size_t endY1) {

	if (endY1 - startY1 == 1 || endX1 - startX1 == 1) {
		if (endX1 - startX1 == 1 && endY1 - startY1 == 1) {
			printf("X %d %d %d %d\n", startX1, startY1, startY1, startX1);
			return; // square
		}

		if (endY1 - startY1 == 1) {
			printf("X %d %d %d %d\n", startX1, startY1, startY1, startX1);
			printf("X %d %d %d %d\n", startX1 + 1, startY1, startY1, startX1 + 1);
		}
		else
		{
			printf("X %d %d %d %d\n", startX1, startY1, startY1, startX1);
			printf("X %d %d %d %d\n", startX1, startY1 + 1, startY1 + 1, startX1);
		}
		return; // rectangle
	}

	size_t sizeX = (endX1 - startX1) / 2;
	size_t sizeY = (endY1 - startY1) / 2;

	transpose_and_swap(
		startX1, startX1 + sizeX, startY1, startY1 + sizeY);

	transpose_and_swap(
		startX1 + sizeX, endX1, startY1 + sizeY, endY1);

	transpose_and_swap(
		startX1 + sizeX, endX1, startY1, startY1 + sizeY);

	transpose_and_swap(
		startX1, startX1 + sizeX, startY1 + sizeY, endY1);
}

void
recurse_transpose(size_t startX, size_t endX, size_t startY, size_t endY) {
	if (endX - startX == 1 && endY - startY == 1)
		return;

	size_t midX = (startX + endX) / 2;
	size_t midY = (startY + endY) / 2;

	recurse_transpose(startX, midX, startY, midY);
	recurse_transpose(midX, endX, midY, endY);

	transpose_and_swap(midX, endX, startY, midY); //, startX, midX, midY, endY);
}

void
r_transpose(size_t n) {
	recurse_transpose(0, n, 0, n);
}

int
main(int argc, char **args) {
	if (argc <= 2)
	{
		printf("Expected at least two argument: -r/-n k.");
		return;
	}

	char *k_s = args[2];
	char *s;
	double k = strtod(k_s, &s);

	s = args[1];
	if (s[0] != '-')
	{
		printf("Unknown modifier: %s", s);
		return;
	}

	int naive = 0;
	if (s[1] == 'n')
	{
		naive = 1;
	}
	else if (s[1] != 'r')
	{
		printf("Unknown modifier: %s", s);
		return;
	}

	size_t n = ceil(pow(2, k / 9.0));
	printf("N %d\n", n);
	if (!naive)
		r_transpose(n);
	else
		naive_transpose(n);
	printf("E\n");
}