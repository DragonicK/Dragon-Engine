#include "WideStringConversion.h"

namespace Crystalshire::Wrapper::Text {

	std::wstring ConvertBSTRToWideString(BSTR bstr) {
		if (bstr != nullptr) {
			std::wstring ws{ bstr, ::SysStringLen(bstr) };
			return ws;
		}

		return std::wstring{ };
	}

	BSTR ConvertWideStringToBSTR(std::wstring ws) {
		BSTR bstr{ };

		if (!ws.empty()) {
			bstr = ::SysAllocStringLen(ws.data(), ws.size());
		}

		return bstr;
	}

	void StringToWideString(System::String const^ source, std::wstring& dest) {
		auto string = const_cast<System::String^>(source);
		auto chars = reinterpret_cast<const wchar_t*>((System::Runtime::InteropServices::Marshal::StringToHGlobalUni(string)).ToPointer());
		dest = chars;
		System::Runtime::InteropServices::Marshal::FreeHGlobal(System::IntPtr((void*)chars));
	}
}