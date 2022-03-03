#pragma once

#include "AESSettings.h"
#include "Hash.h"

namespace Crystalshire::Wrapper::Cryptography {

	ref class AESManaged {
	public:

		static property AESManaged^ Instance {
			AESManaged^ get() {
				return m_Instance;
			}
		}

		bool Encrypt(
			AESSettings* settings,
			array<unsigned char>^ source,
			int sourceLength, 
			unsigned char* dest,
			int* destLength
		);

		bool Decrypt(
			AESSettings* settings, 
			array<unsigned char>^ source,
			int sourceLength, 
			unsigned char* dest,
			int* destLength
		);

		array<unsigned char>^ CreateKey(Hash^ hash, System::String^ passphrase);
		array<unsigned char>^ CreateIv(Hash^ hash,System::String^ passphrase);

	private:

		static AESManaged^ m_Instance = gcnew AESManaged;
	};

}