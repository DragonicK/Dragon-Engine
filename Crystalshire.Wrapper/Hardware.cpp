#include "Hardware.h"

using namespace System;

String^ Hardware::Identifier(System::String^ wmiClass, System::String^ wmiProperty, System::String^ wmiMustBeTrue) {
	auto result = gcnew System::String("Default");
	//auto searcher = gcnew ManagementObjectSearcher("SELECT * FROM " + wmiClass + " WHERE " + wmiMustBeTrue + " = True");
	//auto objects = searcher->Get();

	//for each (ManagementObject ^ object in objects) {
	//	//Only get the first one
	//	if (String::IsNullOrWhiteSpace(result)) {

	//		for each (PropertyData ^ property in object->Properties) {
	//			if (property->Name == wmiProperty) {
	//				if (property->Value != nullptr) {
	//					result = property->Value->ToString();
	//					break;
	//				}
	//			}
	//		}
	//	}
	//}

	return result;
}

String^ Hardware::Identifier(System::String^ wmiClass, System::String^ wmiProperty) {
	auto result = gcnew System::String("Default");
	//auto searcher = gcnew ManagementObjectSearcher("SELECT * FROM " + wmiClass);
	//auto objects = searcher->Get();

	//for each (ManagementObject^ object in objects) {
	//	//Only get the first one
	//	if (String::IsNullOrWhiteSpace(result)) {

	//		for each(PropertyData^ property in object->Properties) {
	//			if (property->Name->CompareTo(wmiProperty) == 0) {
	//				if (property->Value != nullptr) {
	//					result = property->Value->ToString();
	//					break;
	//				}
	//			}
	//		}

	//	}
	//}

	return result;
}

String^ Hardware::GetCPUId() {
	auto retVal = Identifier("Win32_Processor", "UniqueId");

	if (retVal == String::Empty) {
		retVal = Identifier("Win32_Processor", "ProcessorId");

		if (retVal == String::Empty) {
			retVal = Identifier("Win32_Processor", "Name");

			if (retVal == String::Empty) {
				retVal = Identifier("Win32_Processor", "Manufacturer");
			}

			retVal += Identifier("Win32_Processor", "MaxClockSpeed");
		}
	}

	return retVal;
}

String^ Hardware::GetBIOSId() {
	return Identifier("Win32_BIOS", "Manufacturer")
		+ Identifier("Win32_BIOS", "SMBIOSBIOSVersion")
		+ Identifier("Win32_BIOS", "IdentificationCode")
		+ Identifier("Win32_BIOS", "SerialNumber")
		+ Identifier("Win32_BIOS", "ReleaseDate")
		+ Identifier("Win32_BIOS", "Version");
}

String^ Hardware::GetDiskId() {
	return Identifier("Win32_DiskDrive", "Model")
		+ Identifier("Win32_DiskDrive", "Manufacturer")
		+ Identifier("Win32_DiskDrive", "Signature")
		+ Identifier("Win32_DiskDrive", "TotalHeads");
}

String^ Hardware::GetBoardId() {
	return Identifier("Win32_BaseBoard", "Model")
		+ Identifier("Win32_BaseBoard", "Manufacturer")
		+ Identifier("Win32_BaseBoard", "Name")
		+ Identifier("Win32_BaseBoard", "SerialNumber");
}

String^ Hardware::GetVideoId() {
	return Identifier("Win32_VideoController", "DriverVersion") + Identifier("Win32_VideoController", "Name");
}

String^ Hardware::GetMacAddressId() {
	return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
}