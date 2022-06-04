#pragma once

using namespace System::Text;
using namespace System::Security::Cryptography;

namespace Dragon::Wrapper::Cryptography {

	ref class Hash {
	public:
		static property Hash^ Instance {
			Hash^ get() {
				return m_Instance;
			}
		}

		array<unsigned char>^ Compute(System::String^ data);
		System::String^ ConvertToHexadecimal(array<unsigned char>^ buffer);

	private:
		static Hash^ m_Instance = gcnew Hash;
	};
}