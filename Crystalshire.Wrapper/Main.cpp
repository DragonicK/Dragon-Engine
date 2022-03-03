#include "Main.h"
#include "Hash.h"
#include "Hardware.h"
#include "AESManaged.h"
#include "FileManager.h"
#include "WideStringConversion.h"

using namespace Crystalshire::Wrapper::IO;
using namespace Crystalshire::Wrapper::Text;
using namespace Crystalshire::Wrapper::Cryptography;

using namespace System::Runtime::InteropServices;

int ConvertStringToHexadecimal(System::String^ string, BSTR* output);

int WINAPI GetFileHandler(LPCSTR path) {
	auto file = gcnew System::String(path);

	return FileManager::Instance->Open(file);
}

bool WINAPI CloseFileHandler(int index) {
	return FileManager::Instance->Close(index);
}

int WINAPI ReadString(int index, BSTR* output) {
	auto s = FileManager::Instance->ReadString(index);

	std::wstring ws;

	StringToWideString(s, ws);

	*output = ConvertWideStringToBSTR(ws);

	return s->Length;
}

int WINAPI ReadBytes(int index, unsigned char* buffer, int length) {
	auto bytes = FileManager::Instance->ReadBytes(index, length);

	pin_ptr<unsigned char>_buffer = &bytes[0];
	
	memcpy_s(buffer, bytes->Length, _buffer, bytes->Length);

	return bytes->Length;
}

unsigned char WINAPI ReadByte(int index) {
	return FileManager::Instance->ReadByte(index);
}

short WINAPI ReadInt16(int index) {
	return FileManager::Instance->ReadInt16(index);
}

int WINAPI ReadInt32(int index) {
	return FileManager::Instance->ReadInt32(index);
}

float WINAPI ReadSingle(int index) {
	return FileManager::Instance->ReadSingle(index);
}

bool WINAPI ReadBoolean(int index) {
	return FileManager::Instance->ReadBoolean(index);
}

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

	StringToWideString(computed, ws);

	*output = ConvertWideStringToBSTR(ws);

	return computed->Length;
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

int ConvertStringToHexadecimal(System::String^ string, BSTR* output) {
	auto buffer = Hash::Instance->Compute(string);
	auto id = Hash::Instance->ConvertToHexadecimal(buffer);

	std::wstring ws;

	StringToWideString(id, ws);

	*output = ConvertWideStringToBSTR(ws);

	return id->Length;
}