#pragma once

#include <wtypes.h>
#include <string>

namespace Crystalshire::Wrapper::Text {

	std::wstring ConvertBSTRToWideString(BSTR bstr);

	BSTR ConvertWideStringToBSTR(std::wstring ws);

	void StringToWideString(System::String const^ source, std::wstring& dest);
}