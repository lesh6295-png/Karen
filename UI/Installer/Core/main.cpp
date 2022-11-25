#include<iostream>
#include<filesystem>
#include<Windows.h>
namespace fs = std::filesystem;

int APIENTRY wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
	fs::path p = fs::current_path();
	fs::create_directory(p / "temp");
	fs::create_directory(p / "bin");

	//CreateProcess(L"cmd", L"echo q > D:\\qwer", NULL, NULL, NULL, NORMAL_PRIORITY_CLASS, NULL, NULL, )
    //WinExec(LPCSTR("cmd.exe mkdir pidor"), SW_HIDE);
	return 0;
}