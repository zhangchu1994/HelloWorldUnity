@echo off

set MyTime=%TIME%

set MyTime=%Mytime:~0,8%
set MyTime=%MyTime::=-%

:START_ERL

set erl_key=HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Ericsson\Erlang
set tempfile=temp

rem Delete the tempfile if it was left over from before.
del %tempfile% 2>nul

rem		Make an ordered list of all installed erlang versions. Start checking for major
rem		version 9 and go down until major 5. For each major, check for minor version 10+
rem		first, and then for minor 0-9. This will make sure that versions are sorted
rem		correctly, with the latest on top. Write all keys to the tempfile.
for /l %%v in (9,-1,5) do (
	reg query %erl_key% | findstr /r "\\%%v\.[1-9][0-9]\." | sort /r >> %tempfile%
	reg query %erl_key% | findstr /r "\\%%v\.[0-9]\." | sort /r >> %tempfile%
)

rem		Uncomment if you need to inspect the contents of the tempfile.
rem pause

rem		Now look up the path value for the listed keys, starting at the top. If an
rem		existing path is found, then we're done.
for /f %%i in (%tempfile%) do (
	for /f "skip=1 tokens=2*" %%a in ('reg query %%i /ve') do (
		if exist "%%b\bin\werl.exe" (
			set CompletePath="%%b\bin\werl.exe"
			goto:found_erl
		)
	)
)

del %tempfile%
echo ERROR: Can't find any Erlang installation with version 5.0.0 or higher.
pause
goto:eof

:found_erl
del %tempfile%
echo Using Erlang at: %CompletePath%

echo -------------------------------

set ERL_MAX_ETS_TABLES=10000
set ERL_MAX_PORTS=4000
set ERLANG_APP_NAME=uzone_server
for /F "usebackq tokens=1,2 delims==" %%i in (`wmic os get LocalDateTime /VALUE 2^>NUL`) do if '.%%i.'=='.LocalDateTime.' set ldt=%%j
set ldt=%ldt:~0,8%_%ldt:~8,6%

start "" %CompletePath% -pa ebin\ -boot start_sasl -sasl sasl_error_logger "{file, \"logs\\server_%ldt%.log\"}" -sname uzone_server -setcookie uzone_cookie -eval "application:start(uz_server)" -K true
