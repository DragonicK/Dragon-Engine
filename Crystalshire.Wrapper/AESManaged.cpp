#include "AESKeyLength.h"
#include "AESManaged.h"
#include "Hash.h"

#include <Windows.h>
#include <string>

using namespace System::IO;
using namespace System::Security::Cryptography;
using namespace System::Runtime::InteropServices;

namespace Crystalshire::Wrapper::Cryptography {

	bool AESManaged::Encrypt(AESSettings* settings, array<unsigned char>^ source, int sourceLength, unsigned char* dest, int* destLength) {

		try {
			auto aes = Aes::Create();
			auto ms = gcnew MemoryStream();

			aes->KeySize = static_cast<int>(settings->KeySize);
			aes->BlockSize = BlockSize;
			aes->Mode = static_cast<CipherMode>(settings->CipherMode);
			aes->Padding = static_cast<PaddingMode>(settings->PaddingMode);

			auto key = gcnew array<unsigned char>(KeyLength);
			auto iv = gcnew array<unsigned char>(KeyLength);

			Marshal::Copy((System::IntPtr)settings->Key, key, 0, KeyLength);
			Marshal::Copy((System::IntPtr)settings->Iv, iv, 0, KeyLength);

			aes->Key = key;
			aes->IV = iv;
			
			auto cs = gcnew CryptoStream(ms, aes->CreateEncryptor(), CryptoStreamMode::Write);
			cs->Write(source, 0, sourceLength);
			cs->Close();
	
			auto _dest = ms->ToArray();
			*destLength = _dest->Length;

			pin_ptr<unsigned char>_buffer = &_dest[0];

			memcpy_s(dest, _dest->Length, _buffer, _dest->Length);

			ms->Close();

			return true;
		}
		catch (System::Exception ^ex) {
			System::IO::File::WriteAllText("EncryptError.txt", ex->Message);
		}

		return false;
	}

	bool AESManaged::Decrypt(AESSettings* settings, array<unsigned char>^ source, int sourceLength, unsigned char* dest, int* destLength) {

		try {
			auto aes = Aes::Create();
			auto ms = gcnew MemoryStream();

			aes->KeySize = static_cast<int>(settings->KeySize);
			aes->BlockSize = BlockSize;
			aes->Mode = static_cast<CipherMode>(settings->CipherMode);
			aes->Padding = static_cast<PaddingMode>(settings->PaddingMode);

			auto key = gcnew array<unsigned char>(KeyLength);
			auto iv = gcnew array<unsigned char>(KeyLength);

			Marshal::Copy((System::IntPtr)settings->Key, key, 0, KeyLength);
			Marshal::Copy((System::IntPtr)settings->Iv, iv, 0, KeyLength);

			aes->Key = key;
			aes->IV = iv;

			auto cs = gcnew CryptoStream(ms, aes->CreateDecryptor(), CryptoStreamMode::Write);
			cs->Write(source, 0, sourceLength);
			cs->Close();

			auto _dest = ms->ToArray();
			*destLength = _dest->Length;

			pin_ptr<unsigned char> _buffer = &_dest[0];

			memcpy_s(dest, _dest->Length, _buffer, _dest->Length);

			ms->Close();

			return true;
		}
		catch (System::Exception^ ex) {
			System::IO::File::WriteAllText("DecryptError.txt", ex->Message);
		}

		return false;
	}

	array<unsigned char>^ AESManaged::CreateKey(Hash^ hash, System::String^ passphrase) {
		auto computed = hash->Compute(passphrase);

		auto key = gcnew array<unsigned char>(KeyLength);

		for (auto i = 0; i < KeyLength; ++i) {
			key[i] = computed[i % computed->Length];
		}

		return key;
	}

	array<unsigned char>^ AESManaged::CreateIv(Hash^ hash, System::String^ passphrase) {
		auto computed = hash->Compute(passphrase);

		auto iv = gcnew array<unsigned char>(KeyLength);

		for (auto i = KeyLength - 1; i >= 0; --i) {
			iv[i] = computed[i % computed->Length];
		}

		return iv;
	}
}