#include "FileManager.h"

namespace Dragon::Wrapper::IO {

	FileManager::FileManager() {
		auto streams = gcnew Dictionary<int, FileStream^>();
		auto readers = gcnew Dictionary<int, BinaryReader^>();
	}

	int FileManager::Open(System::String^ file) {
		if (File::Exists(file)) {
			auto stream = gcnew FileStream(file, FileMode::Open, FileAccess::Read);

			if (stream != nullptr) {
				int index;

				for (index = 1; index < System::Int32::MaxValue; ++index) {
					if (!streams.ContainsKey(index)) {
						streams[index] = stream;

						readers[index] = gcnew BinaryReader(stream);

						break;
					}
				}

				return index;
			}
		}

		return 0;
	}

	bool FileManager::Close(int index) {
		if (readers.ContainsKey(index)) {
			auto reader = readers[index];

			reader->Close();

			readers.Remove(index);
		}

		if (streams.ContainsKey(index)) {
			auto stream = streams[index];

			stream->Close();

			streams.Remove(index);

			return true;
		}

		return false;
	}

	System::String^ FileManager::ReadString(int index) {
		if (IsValidIndex(index)) {
			auto reader = readers[index];
			return reader->ReadString();
		}

		return System::String::Empty;
	}

	unsigned char FileManager::ReadByte(int index) {
		if (IsValidIndex(index)) {
			auto reader = readers[index];
			return reader->ReadByte();
		}

		return 0;
	}

	array<unsigned char>^ FileManager::ReadBytes(int index, int length) {
		if (IsValidIndex(index)) {
			auto reader = readers[index];
			return reader->ReadBytes(length);
		}

		return nullptr;
	}

	short FileManager::ReadInt16(int index) {
		if (IsValidIndex(index)) {
			auto reader = readers[index];
			return reader->ReadInt16();
		}

		return 0;
	}

	int FileManager::ReadInt32(int index) {
		if (IsValidIndex(index)) {
			auto reader = readers[index];
			return reader->ReadInt32();
		}

		return 0;
	}

	float FileManager::ReadSingle(int index) {
		if (IsValidIndex(index)) {
			auto reader = readers[index];
			return reader->ReadSingle();
		}

		return 0;
	}

	bool FileManager::ReadBoolean(int index) {
		if (IsValidIndex(index)) {
			bool read;

			try {
				auto reader = readers[index];
				read = reader->ReadBoolean();
			}
			catch (System::Exception^ ex) {
				throw ex;
			}

			return read;
		}

		return false;
	}

	bool FileManager::IsValidIndex(int index) {
		return (streams.ContainsKey(index) && readers.ContainsKey(index));
	}
}