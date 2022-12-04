//if you start installer.exe with -sw argument you can see powershell windows
#include<iostream>
#include<filesystem>
#include<Windows.h>
#include <fstream>
#include <sstream>
#include<shellapi.h>
#define strg std::wstring
#define fndr std::wstring::size_type
#define fdrs std::wstring::npos
int ssp = 0;
namespace fs = std::filesystem;
std::wstring get_release_tag();
void cmd_execute(LPCWSTR command) {
	ShellExecute(NULL, NULL, L"cmd.exe",command, NULL, ssp);
}
void powershell_execute(LPCWSTR command) {
	SHELLEXECUTEINFO ShExecInfo = { 0 };
	ShExecInfo.cbSize = sizeof(SHELLEXECUTEINFO);
	ShExecInfo.fMask = SEE_MASK_NOCLOSEPROCESS;
	ShExecInfo.hwnd = NULL;
	ShExecInfo.lpVerb = NULL;
	ShExecInfo.lpFile = L"powershell.exe";
	ShExecInfo.lpParameters = command;
	ShExecInfo.lpDirectory = NULL;
	ShExecInfo.nShow = ssp;
	ShExecInfo.hInstApp = NULL;
	ShellExecuteEx(&ShExecInfo);
	WaitForSingleObject(ShExecInfo.hProcess, INFINITE);
	CloseHandle(ShExecInfo.hProcess);
}
void download_gui() {
	strg f = L"Invoke-WebRequest -OutFile gui.bin https://github.com/lesh6295-png/Karen/releases/download/";
	strg fn = L"/gui.bin";
	strg c = f + get_release_tag() + fn;
	powershell_execute(c.c_str());
}
void download_7z() {
	strg f = L"Invoke-WebRequest -OutFile 7zr.exe https://7-zip.org/a/7zr.exe";
	powershell_execute(f.c_str());
}
void unpack_gui() {
	strg f = L".\\7zr.exe e -obin gui.bin";
	powershell_execute(f.c_str());
}
void launch_gui() {
	strg f = L".\\bin/GuiInstaller.exe";
	powershell_execute(f.c_str());
}
std::wstring get_release_tag() {
	std::wifstream inFile;
	inFile.open("ReleaseConfig.ini");
	std::wstringstream strStream;
	strStream << inFile.rdbuf();
	return strStream.str();
}
int APIENTRY wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
	fs::path p = fs::current_path();
	fs::create_directory(p / "bin");
	strg pa = lpCmdLine;
	fndr res = pa.find(L"-sw");
	if (res != fdrs) {
		ssp = 5;
	}
	bool localinst = fs::exists(p / "gui.bin")&& !fs::exists(p / "7zr.exe");
	//TODO: CHANGE ASSETS LOCATION
	if (localinst) {
		download_gui();
		download_7z();
	}
	unpack_gui();
	launch_gui();
	return 0;
}