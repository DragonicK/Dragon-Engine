#pragma once
namespace Crystalshire::Wrapper::Cryptography {
    enum class AESPaddingMode : int {
        //
        // Summary:
        //     No padding is done.
        None = 1,
        //
        // Summary:
        //     The PKCS #7 padding string consists of a sequence of bytes, each of which is
        //     equal to the total number of padding bytes added.
        PKCS7 = 2,
        //
        // Summary:
        //     The padding string consists of bytes set to zero.
        Zeros = 3,
        //
        // Summary:
        //     The ANSIX923 padding string consists of a sequence of bytes filled with zeros
        //     before the length.
        ANSIX923 = 4,
        //
        // Summary:
        //     The ISO10126 padding string consists of random data before the length.
        ISO10126 = 5
    };
}