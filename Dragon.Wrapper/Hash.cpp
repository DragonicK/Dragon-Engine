#include "Hash.h"
#include "AESKeyLength.h"

using namespace System::Security::Cryptography;

namespace Dragon::Wrapper::Cryptography {

	array<unsigned char>^ Hash::Compute(System::String^ data) {
		auto sha = SHA256::Create();

		return sha->ComputeHash(Encoding::Unicode->GetBytes(data));
	}

	array<unsigned char>^ Hash::Compute(array<unsigned char>^ data, int length, bool reverse) {
		auto hash = gcnew array<unsigned char>(KeyLength);
		auto copy = gcnew array<unsigned char>(length);

		System::Array::Copy(data, copy, length);

		if (reverse) {
			System::Array::Reverse(copy);
		}

		auto sha = SHA256::Create();
		auto buffer = sha->ComputeHash(copy);

		System::Array::Copy(buffer, 0, hash, 0, KeyLength);

		return hash;
	}

	System::String^ Hash::ConvertToHexadecimal(array<unsigned char>^ buffer) {
		auto sb = gcnew StringBuilder(buffer->Length * 2);

		for (auto i = 0; i < buffer->Length; ++i) {
			sb->Append(buffer[i].ToString("x2"));
		}

		return sb->ToString();
	}
}