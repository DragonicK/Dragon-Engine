#pragma once

#include <Windows.h>

#include "AESSettings.h"

using namespace Crystalshire::Wrapper::Cryptography;

#ifdef __cplusplus
extern "C" {
#endif

	__declspec(dllexport) int WINAPI GetFileHandler(LPCSTR path);
	__declspec(dllexport) bool WINAPI CloseFileHandler(int index);

	__declspec(dllexport) int WINAPI ReadString(int index, BSTR* output);
	__declspec(dllexport) int WINAPI ReadBytes(int index, unsigned char* buffer, int length);
	__declspec(dllexport) unsigned char WINAPI ReadByte(int index);
	__declspec(dllexport) short WINAPI ReadInt16(int index);
	__declspec(dllexport) int WINAPI ReadInt32(int index);
	__declspec(dllexport) float WINAPI ReadSingle(int index);
	__declspec(dllexport) bool WINAPI ReadBoolean(int index);

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

#ifdef __cplusplus
}
#endif