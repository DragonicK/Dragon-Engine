#include "BlowFishCipher.h"
#include <Windows.h>

namespace Dragon::Wrapper::Cryptography {

	void BlowFishCipher::UpdateKey(unsigned char* udpatedKey, int length) {
		memcpy_s(key, length, udpatedKey, length);

		memcpy_s(sBoxes[0], 256, ZeroBoxInit, 256);
		memcpy_s(sBoxes[1], 256, FirstBoxInit, 256);
		memcpy_s(sBoxes[2], 256, SecondBoxInit, 256);
		memcpy_s(sBoxes[3], 256, ThirdBoxInit, 256);

		InitArrays();
	}

	void BlowFishCipher::InitArrays() {
		int keyIndex = 0;

		for (int i = 0; i < 18; i++) {
			int data = 0;

			for (int j = 0; j < 4; j++) {
				data = (data << 8) | (key[keyIndex++] & 0xFF);

				if (keyIndex >= KeyLength) {
					keyIndex = 0;
				}
			}

			pArray[i] = ArrayInit[i] ^ data;
		}

		auto buffer = new unsigned char[8];

		for (int i = 0; i < 18; i += 2) {
			Cipher(buffer, 0, 8);

			pArray[i] = ByteArrayToInteger(buffer, 0);
			pArray[i + 1] = ByteArrayToInteger(buffer, 4);
		}

		for (int i = 0; i < 4; i++) {
			InitSBox(buffer, 8, sBoxes[i]);
		}
	}

	void BlowFishCipher::InitSBox(unsigned char buffer[], int length,  int sBox[]) {
		for (int j = 0; j < 256; j += 2) {
			Cipher(buffer, 0, length);

			sBox[j] = ByteArrayToInteger(buffer, 0);
			sBox[j + 1] = ByteArrayToInteger(buffer, 4);
		}
	}
		 
	void BlowFishCipher::Cipher(unsigned char* buffer, int offset, int length) {
		int blockNumber = length >> 3;
		int p;

		for (int k = 0; k < blockNumber; k++) {
			p = offset + (k << 3);

			int xl = ByteArrayToInteger(buffer, p);
			int xr = ByteArrayToInteger(buffer, p + 4);
			int tmp;

			for (int i = 0; i < 16; i++) {
				xl = xl ^ pArray[i];
				xr = F(xl) ^ xr;
				tmp = xl;
				xl = xr;
				xr = tmp;
			}

			tmp = xl;
			xl = xr;
			xr = tmp;

			xr ^= pArray[16];
			xl ^= pArray[17];

			IntegerToByteArray(xl, buffer, p);
			IntegerToByteArray(xr, buffer, p + 4);
		}
	}

	void BlowFishCipher::Decipher(unsigned char* buffer, int offset, int length) {
		int blocks = length >> 3;
		int p;

		for (int k = 0; k < blocks; k++) {
			p = offset + (k << 3);

			int lb = ByteArrayToInteger(buffer, p);
			int rb = ByteArrayToInteger(buffer, p + 4);
			int tmp;

			for (int i = 17; i > 1; i--) {
				lb ^= pArray[i];
				rb = F(lb) ^ rb;
				tmp = lb;
				lb = rb;
				rb = tmp;
			}

			tmp = lb;
			lb = rb;
			rb = tmp;

			rb ^= pArray[1];
			lb ^= pArray[0];

			IntegerToByteArray(lb, buffer, p);
			IntegerToByteArray(rb, buffer, p + 4);
		}
	}

	int BlowFishCipher::F(int x) {
		int a, b, c, d;

		d = x & 0xFF;
		x >>= 8;
		c = x & 0xFF;
		x >>= 8;
		b = x & 0xFF;
		x >>= 8;
		a = x & 0xFF;

		int y = sBoxes[0][a] + sBoxes[1][b];

		y ^= sBoxes[2][c];
		y += sBoxes[3][d];

		return y;
	}

	int BlowFishCipher::ByteArrayToInteger(unsigned char* buffer, int offset) {
		return (buffer[offset + 3] & 0xFF) << 24 | (buffer[offset + 2] & 0xFF) << 16 | (buffer[offset + 1] & 0xFF) << 8 | (buffer[offset] & 0xFF);
	}

	void BlowFishCipher::IntegerToByteArray(int value, unsigned char* buffer, int offset) {
		buffer[offset] = (unsigned char)(value & 0xFF);
		buffer[offset + 1] = (unsigned char)(value >> 8 & 0xFF);
		buffer[offset + 2] = (unsigned char)(value >> 16 & 0xFF);
		buffer[offset + 3] = (unsigned char)(value >> 24 & 0xFF);
	}
}