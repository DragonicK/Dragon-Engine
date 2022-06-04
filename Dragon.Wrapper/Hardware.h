#pragma once

using namespace System;

ref class Hardware {
public:
	static property Hardware^ Instance {
		Hardware^ get() {
			return m_Instance;
		}
	}

	System::String^ GetCPUId();
	System::String^ GetBIOSId();
	System::String^ GetDiskId();
	System::String^ GetBoardId();
	System::String^ GetVideoId();
	System::String^ GetMacAddressId();
private:
	static Hardware^ m_Instance = gcnew Hardware;

	System::String^ Identifier(System::String^ wmiClass, System::String^ wmiProperty, System::String^ wmiMustBeTrue);
	System::String^ Identifier(System::String^ wmiClass, System::String^ wmiProperty);
};
