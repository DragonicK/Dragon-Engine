#pragma once

#include <wtypes.h>
#include <string>

namespace Dragon::Wrapper::Text {

	std::wstring ConvertBSTRToWideString(BSTR bstr);

	BSTR ConvertWideStringToBSTR(std::wstring ws);
}