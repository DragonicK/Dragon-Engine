#pragma once

#include <Windows.h>
#include "AESSettings.h"

using namespace Dragon::Wrapper::Cryptography;

#ifdef __cplusplus
extern "C" {
#endif

	__declspec(dllexport) int WINAPI GetFileHandler(LPCSTR path);
	__declspec(dllexport) bool WINAPI CloseFileHandler();

	__declspec(dllexport) int WINAPI ReadString(BSTR* output);
	__declspec(dllexport) int WINAPI ReadBytes(unsigned char* buffer, int length);
	__declspec(dllexport) unsigned char WINAPI ReadByte();
	__declspec(dllexport) short WINAPI ReadInt16();
	__declspec(dllexport) int WINAPI ReadInt32();
	__declspec(dllexport) float WINAPI ReadSingle();
	__declspec(dllexport) bool WINAPI ReadBoolean();

	__declspec(dllexport) bool WINAPI Encrypt(
		AESSettings* settings,
		unsigned char* source,
		int sourceLength,
		unsigned char* dest,
		int* destLength
	);

	__declspec(dllexport) bool WINAPI Decrypt(
		AESSettings* settings,
		unsigned char* source,
		int sourceLength,
		unsigned char* dest,
		int* destLength
	);

	__declspec(dllexport) int WINAPI Compute(LPCSTR input, unsigned char* dest);
	__declspec(dllexport) int WINAPI ConvertToHexadecimal(unsigned char* source, int length, BSTR* output);

	__declspec(dllexport) int WINAPI CreateKey(LPCSTR passphrase, unsigned char* dest);
	__declspec(dllexport) int WINAPI CreateIv(LPCSTR passphrase, unsigned char* dest);

	__declspec(dllexport) int WINAPI GetCPUId(BSTR* output);
	__declspec(dllexport) int WINAPI GetBIOSId(BSTR* output);
	__declspec(dllexport) int WINAPI GetDiskId(BSTR* output);
	__declspec(dllexport) int WINAPI GetBoardId(BSTR* output);
	__declspec(dllexport) int WINAPI GetVideoId(BSTR* output);
	__declspec(dllexport) int WINAPI GetMacAddressId(BSTR* output);

	__declspec(dllexport) void WINAPI Cipher(int instanceIndex, unsigned char* buffer, int length);
	__declspec(dllexport) bool WINAPI Decipher(int instanceIndex, unsigned char* buffer, int length);
	__declspec(dllexport) void WINAPI UpdateKey(int instanceIndex, unsigned char* buffer, int length);
	__declspec(dllexport) void WINAPI AppendCheckSum(int instanceIndex, unsigned char* raw, int offset, int length);

#ifdef __cplusplus
}
#endif