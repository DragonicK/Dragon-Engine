#pragma once

namespace Crystalshire::Wrapper::Cryptography {
    enum class AESCipherMode : int {
        CBC = 1,
        ECB = 2,
        OFB = 3,
        CFB = 4,
        CTS = 5
    };
}