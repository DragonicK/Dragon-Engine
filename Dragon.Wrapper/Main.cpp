#include "Main.h"
#include "Hash.h"
#include "Hardware.h"
#include "AESManaged.h"
#include "FileManager.h"
#include "BlowFishCipher.h"
#include "WideStringConversion.h"

using namespace Dragon::Wrapper::IO;
using namespace Dragon::Wrapper::Text;
using namespace Dragon::Wrapper::Cryptography;

using namespace System::Runtime::InteropServices;

int ConvertStringToHexadecimal(System::String^ string, BSTR* output) {
	auto buffer = Hash::Instance->Compute(string);
	auto id = Hash::Instance->ConvertToHexadecimal(buffer);

	std::wstring ws;

	*output = ConvertWideStringToBSTR(ws);

	return id->Length;
}

#pragma region File Handler

int WINAPI GetFileHandler(LPCSTR path) {
	return FileManager::Instance().Open(path);
}

bool WINAPI CloseFileHandler( ) {
	FileManager::Instance().Close();

	return 0;
}

int WINAPI ReadString(BSTR* output) {
	auto ws = FileManager::Instance().ReadString();

	*output = ConvertWideStringToBSTR(ws);

	return static_cast<int>(ws.length());
}

unsigned char WINAPI ReadByte() {
	return FileManager::Instance().ReadByte();
}

short WINAPI ReadInt16() {
	return FileManager::Instance().ReadInt16();
}

int WINAPI ReadInt32() {
	return FileManager::Instance().ReadInt32();
}

float WINAPI ReadSingle() {
	return FileManager::Instance().ReadSingle();
}

bool WINAPI ReadBoolean() {
	return FileManager::Instance().ReadBoolean();
}

int WINAPI ReadBytes(unsigned char* buffer, int length) {
	auto bytes = FileManager::Instance().ReadBytes(length);

	memcpy_s(buffer, bytes.size(), bytes.data(), bytes.size());

	return static_cast<int>(bytes.size());
}

#pragma endregion

#pragma region AES Managed

bool WINAPI Encrypt(AESSettings* settings, unsigned char* source, int sourceLength, unsigned char* dest, int* destLength) {
	auto _source = gcnew array<unsigned char>(sourceLength);

	pin_ptr<unsigned char> _buffer = &_source[0];

	memcpy_s(_buffer, sourceLength, source, sourceLength);

	auto result = AESManaged::Instance->Encrypt(settings, _source, sourceLength, dest, destLength);

	return result;
}

bool WINAPI Decrypt(AESSettings* settings, unsigned char* source, int sourceLength, unsigned char* dest, int* destLength) {
	auto _source = gcnew array<unsigned char>(sourceLength);

	pin_ptr<unsigned char> _buffer = &_source[0];

	memcpy_s(_buffer, sourceLength, source, sourceLength);

	auto result = AESManaged::Instance->Decrypt(settings, _source, sourceLength, dest, destLength);

	return result;
}

int WINAPI CreateKey(LPCSTR passphrase, unsigned char* dest) {
	auto hash = Hash::Instance;
	auto data = gcnew System::String(passphrase);

	auto computed = AESManaged::Instance->CreateKey(hash, data);

	pin_ptr<unsigned char> buffer = &computed[0];

	memcpy_s(dest, computed->Length, buffer, computed->Length);

	return computed->Length;
}

int WINAPI CreateIv(LPCSTR passphrase, unsigned char* dest) {
	auto hash = Hash::Instance;
	auto data = gcnew System::String(passphrase);

	auto computed = AESManaged::Instance->CreateIv(hash, data);

	pin_ptr<unsigned char> buffer = &computed[0];

	memcpy_s(dest, computed->Length, buffer, computed->Length);

	return computed->Length;
}

#pragma endregion

#pragma region Hash

int WINAPI Compute(LPCSTR input, unsigned char* dest) {
	auto data = gcnew System::String(input);
	auto hash = Hash::Instance;

	auto computed = hash->Compute(data);

	pin_ptr<unsigned char> buffer = &computed[0];

	memcpy_s(dest, computed->Length, buffer, computed->Length);

	return computed->Length;
}

int WINAPI ConvertToHexadecimal(unsigned char* source, int length, BSTR* output) {
	auto data = gcnew array<unsigned char>(length);
	auto hash = Hash::Instance;

	pin_ptr<unsigned char> buffer = &data[0];

	memcpy_s(buffer, length, source, length);

	auto computed = hash->ConvertToHexadecimal(data);

	std::wstring ws;

	*output = ConvertWideStringToBSTR(ws);

	return computed->Length;
}

#pragma endregion

#pragma region Machine Id

int WINAPI GetCPUId(BSTR* output) {
	return ConvertStringToHexadecimal(Hardware::Instance->GetCPUId(), output);
}

int WINAPI GetBIOSId(BSTR* output) {
	return ConvertStringToHexadecimal(Hardware::Instance->GetBIOSId(), output);
}

int WINAPI GetDiskId(BSTR* output) {
	return ConvertStringToHexadecimal(Hardware::Instance->GetDiskId(), output);
}

int WINAPI GetBoardId(BSTR* output) {
	return ConvertStringToHexadecimal(Hardware::Instance->GetBoardId(), output);
}

int WINAPI GetVideoId(BSTR* output) {
	return ConvertStringToHexadecimal(Hardware::Instance->GetVideoId(), output);
}

int WINAPI GetMacAddressId(BSTR* output) {
	return ConvertStringToHexadecimal(Hardware::Instance->GetMacAddressId(), output);
}

#pragma endregion

#pragma region BlowFishCipher 

void WINAPI UpdateKey(unsigned char* buffer, int length) {
	BlowFishCipher::Instance().UpdateKey(buffer, length);
}

void WINAPI Cipher(unsigned char* buffer, int length) {
	BlowFishCipher::Instance().Cipher(buffer, 0, length);
}

bool WINAPI Decipher(unsigned char* buffer, int length) {
	return BlowFishCipher::Instance().Decipher(buffer, 0, length);
}

#pragma endregion