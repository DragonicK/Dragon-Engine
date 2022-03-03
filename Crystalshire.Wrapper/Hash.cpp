#include "Hash.h"

namespace Crystalshire::Wrapper::Cryptography {

	array<unsigned char>^ Hash::Compute(System::String^ data) {
		auto sha = SHA256::Create();

		return sha->ComputeHash(Encoding::Unicode->GetBytes(data));
	}

	System::String^ Hash::ConvertToHexadecimal(array<unsigned char>^ buffer) {
		auto sb = gcnew StringBuilder(buffer->Length * 2);

		for (auto i = 0; i < buffer->Length; ++i) {
			sb->Append(buffer[i].ToString("x2"));
		}

		return sb->ToString();
	}

}