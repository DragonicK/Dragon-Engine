#include "BlowFishCipher.h"

namespace Dragon::Wrapper::Cryptography {

	void BlowFishCipher::UpdateKey(byte* udpatedKey, int length) {
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

	void BlowFishCipher::InitSBox(byte buffer[], int length,  int sBox[]) {
		for (int j = 0; j < 256; j += 2) {
			Cipher(buffer, 0, length);

			sBox[j] = ByteArrayToInteger(buffer, 0);
			sBox[j + 1] = ByteArrayToInteger(buffer, 4);
		}
	}
		 
	void BlowFishCipher::Cipher(byte* buffer, int offset, int length) {
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

		AppendCheckSum(buffer, 0, length);
	}

	bool BlowFishCipher::Decipher(byte* buffer, int offset, int length) {
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

		return VerifyCheckSum(buffer, 0, length);
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

	int BlowFishCipher::ByteArrayToInteger(byte* buffer, int offset) {
		return (buffer[offset + 3] & 0xFF) << 24 | (buffer[offset + 2] & 0xFF) << 16 | (buffer[offset + 1] & 0xFF) << 8 | (buffer[offset] & 0xFF);
	}

	void BlowFishCipher::IntegerToByteArray(int value, byte* buffer, int offset) {
		buffer[offset] = (unsigned char)(value & 0xFF);
		buffer[offset + 1] = (unsigned char)(value >> 8 & 0xFF);
		buffer[offset + 2] = (unsigned char)(value >> 16 & 0xFF);
		buffer[offset + 3] = (unsigned char)(value >> 24 & 0xFF);
	}

	void BlowFishCipher::AppendCheckSum(byte* raw, int offset, int length) {
		long chksum = 0;
		int count = length - 4;
		long ecx;
		int i;

		for (i = offset; i < count; i += 4) {
			ecx = raw[i] & 0xff;
			ecx |= raw[i + 1] << 8 & 0xff00;
			ecx |= raw[i + 2] << 0x10 & 0xff0000;
			ecx |= raw[i + 3] << 0x18 & 0xff000000;
			chksum ^= ecx;
		}

		ecx = raw[i] & 0xff;
		ecx |= raw[i + 1] << 8 & 0xff00;
		ecx |= raw[i + 2] << 0x10 & 0xff0000;
		ecx |= raw[i + 3] << 0x18 & 0xff000000;
		raw[i] = (byte)(chksum & 0xff);
		raw[i + 1] = (byte)(chksum >> 0x08 & 0xff);
		raw[i + 2] = (byte)(chksum >> 0x10 & 0xff);
		raw[i + 3] = (byte)(chksum >> 0x18 & 0xff);
	}

	bool BlowFishCipher::VerifyCheckSum(byte* data, int offset, int length) {
		if ((length & 3) != 0 || (length <= 4)) {
			return false;
		}

		long chksum = 0;
		int count = length - 4;
		long check;
		int i;

		for (i = offset; i < count; i += 4) {
			check = data[i] & 0xff;
			check |= data[i + 1] << 8 & 0xff00;
			check |= data[i + 2] << 0x10 & 0xff0000;
			check |= data[i + 3] << 0x18 & 0xff000000;
			chksum ^= check;
		}

		check = data[i] & 0xff;
		check |= data[i + 1] << 8 & 0xff00;
		check |= data[i + 2] << 0x10 & 0xff0000;
		check |= data[i + 3] << 0x18 & 0xff000000;
		check = data[i] & 0xff;
		check |= data[i + 1] << 8 & 0xff00;
		check |= data[i + 2] << 0x10 & 0xff0000;
		check |= data[i + 3] << 0x18 & 0xff000000;

		return 0 == chksum;
	}
}