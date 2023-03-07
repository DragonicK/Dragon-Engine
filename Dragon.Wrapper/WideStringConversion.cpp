#include "WideStringConversion.h"

namespace Dragon::Wrapper::Text {

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
			bstr = ::SysAllocStringLen(ws.data(), static_cast<unsigned int>(ws.size()));
		}

		return bstr;
	}
}