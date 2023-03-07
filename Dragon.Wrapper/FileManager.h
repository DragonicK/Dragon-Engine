#pragma once

#include <Windows.h>
#include <iostream>
#include <fstream>
#include <vector>
#include <locale>
#include <codecvt>
#include <string>

namespace Dragon::Wrapper::IO {

	class FileManager {
	public:
		static FileManager& Instance() {
			static FileManager instance;

			return instance;
		}

		int Open(LPCSTR file);
		void Close();

		char ReadByte();
		int16_t ReadInt16();
		int32_t ReadInt32();
		float ReadSingle();
		bool ReadBoolean();

		std::wstring ReadString();
		std::vector<unsigned char> ReadBytes(int length);

	private:
		std::ifstream stream;

		FileManager() {} 
		FileManager(const FileManager&) = delete; 
		FileManager& operator=(const FileManager&) = delete; 

		int Read7BitEncodedInt();
	};
}