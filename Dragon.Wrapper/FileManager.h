#pragma once

using namespace System::IO;
using namespace System::Collections::Generic;

namespace Dragon::Wrapper::IO {

	ref class FileManager {
	public:
		static property FileManager^ Instance {
			FileManager^ get() {
				return m_Instance;
			}
		}

		FileManager();

		int Open(System::String^ file);
		bool Close(int index);

		System::String^ ReadString(int index);
		unsigned char ReadByte(int index);
		array<unsigned char>^ ReadBytes(int index, int length);
		short ReadInt16(int index);
		int ReadInt32(int index);
		float ReadSingle(int index);
		bool ReadBoolean(int index);

	private:
		static FileManager^ m_Instance = gcnew FileManager;

		Dictionary<int, FileStream^> streams;
		Dictionary<int, BinaryReader^> readers;

		bool IsValidIndex(int index);
	};
}