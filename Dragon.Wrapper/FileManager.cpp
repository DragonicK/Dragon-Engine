#include "FileManager.h"

#define BYTE 1
#define INT32 4
#define INT16 2

namespace Dragon::Wrapper::IO {

	int FileManager::Open(LPCSTR file) {
		stream = std::ifstream(file, std::ios::binary);

		if (!stream.is_open()) {
			std::cerr << "Failed to open file" << std::endl;
			return 1;
		}

		return 0;
	}

	void FileManager::Close() {
		stream.close();
	}

	std::wstring FileManager::ReadString() {
		int length = Read7BitEncodedInt();

		std::vector<char> buffer(length);

		stream.read(buffer.data(), length);

		int wstring_length = MultiByteToWideChar(CP_UTF8, 0, buffer.data(), length, NULL, 0);

		std::wstring utf16_data(wstring_length, L'\0');

		MultiByteToWideChar(CP_UTF8, 0, buffer.data(), length, &utf16_data[0], wstring_length);

		return utf16_data;
	}

	char FileManager::ReadByte() {
		char byteReadJustNow;

		stream.read(&byteReadJustNow, BYTE);

		return byteReadJustNow;
	}

	std::vector<unsigned char> FileManager::ReadBytes(int length) {
		std::vector<unsigned char> buffer(length);

		stream.read(reinterpret_cast<char*>(buffer.data()), length);

		return buffer;
	}

	int16_t FileManager::ReadInt16() {
		char charArray[INT16];

		stream.read(charArray, INT16);

		int16_t result = 0;

		char* resultPtr = reinterpret_cast<char*>(&result);

		for (int i = 0; i < INT16; ++i) {
			resultPtr[i] = charArray[i];
		}

		return result;
	}

	int32_t FileManager::ReadInt32() {
		char charArray[INT32];

		stream.read(charArray, INT32);

		int32_t result = 0;

		char* resultPtr = reinterpret_cast<char*>(&result);

		for (int i = 0; i < INT32; ++i) {
			resultPtr[i] = charArray[i];
		}

		return result;
	}

	float FileManager::ReadSingle() {
		char charArray[INT32];

		stream.read(charArray, INT32);

		union {
			char char_array[INT32];
			float float_value;
		} uFloat {};

		for (int i = 0; i < INT32; i++) {
			uFloat.char_array[i] = charArray[i];
		}

		return uFloat.float_value;
	}

	bool FileManager::ReadBoolean() {
		return ReadByte();
	}

	int FileManager::Read7BitEncodedInt() {
		// Unlike writing, we can't delegate to the 64-bit read on
		// 64-bit platforms. The reason for this is that we want to
		// stop consuming bytes if we encounter an integer overflow.

		uint32_t result = 0;
		uint8_t byteReadJustNow;

		// Read the integer 7 bits at a time. The high bit
		// of the byte when on means to continue reading more bytes.
		//
		// There are two failure cases: we've read more than 5 bytes,
		// or the fifth byte is about to cause integer overflow.
		// This means that we can read the first 4 bytes without
		// worrying about integer overflow.

		const int MaxBytesWithoutOverflow = 4;
		for (int shift = 0; shift < MaxBytesWithoutOverflow * 7; shift += 7) {
			// ReadByte handles end of stream cases for us.
			byteReadJustNow = ReadByte();
			result |= (byteReadJustNow & 0x7Fu) << shift;

			if (byteReadJustNow <= 0x7Fu) {
				return static_cast<int>(result); // early exit
			}
		}

		// Read the 5th byte. Since we already read 28 bits,
		// the value of this byte must fit within 4 bits (32 - 28),
		// and it must not have the high bit set.
		byteReadJustNow = ReadByte();
		if (byteReadJustNow > 0b1111u) {
			throw std::runtime_error("Bad 7-bit integer format");
		}

		result |= static_cast<uint32_t>(byteReadJustNow) << (MaxBytesWithoutOverflow * 7);
		return static_cast<int>(result);
	}
}