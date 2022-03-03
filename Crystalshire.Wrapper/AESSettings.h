#pragma once

#include "AESPaddingMode.h"
#include "AESCipherMode.h"
#include "AESKeyLength.h"
#include "AESKeySize.h"

namespace Crystalshire::Wrapper::Cryptography {

	struct AESSettings {
		AESKeySize KeySize;
		AESCipherMode CipherMode;
		AESPaddingMode PaddingMode;
		unsigned char Key[KeyLength];
		unsigned char Iv[KeyLength];
	};

}