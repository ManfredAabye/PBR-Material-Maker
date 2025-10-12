@echo off
setlocal enabledelayedexpansion

echo.
echo ====================================
echo    PBR Material Maker Starter
echo ====================================
echo.

REM Aktuelles Verzeichnis setzen
cd /d "%~dp0"

REM Zuerst nach Release-Build suchen
set "RELEASE_EXE=.\PBR Material Maker\bin\Release\net8.0-windows\PBR Material Maker.exe"
set "DEBUG_EXE=.\PBR Material Maker\bin\Debug\net8.0-windows\PBR Material Maker.exe"

if exist "%RELEASE_EXE%" (
    echo Release-Build gefunden. Starte PBR Material Maker...
    echo Pfad: %RELEASE_EXE%
    echo.
    "%RELEASE_EXE%"
) else if exist "%DEBUG_EXE%" (
    echo Debug-Build gefunden. Starte PBR Material Maker...
    echo Pfad: %DEBUG_EXE%
    echo.
    "%DEBUG_EXE%"
) else (
    echo.
    echo FEHLER: Keine ausfuehrbare Datei gefunden!
    echo.
    echo Bitte erstellen Sie zuerst das Projekt:
    echo 1. Oeffnen Sie die Loesung PBR_Material_Maker.sln in Visual Studio
    echo 2. Erstellen Sie das Projekt (Build ^> Build Solution^)
    echo.
    echo Erwartete Pfade:
    echo - %RELEASE_EXE%
    echo - %DEBUG_EXE%
    echo.
    ::pause
    exit /b 1
)

REM Pruefen ob das Programm erfolgreich gestartet wurde
if !ERRORLEVEL! NEQ 0 (
    echo.
    echo FEHLER: Das Programm konnte nicht gestartet werden (Fehlercode: !ERRORLEVEL!^)
    echo.
    ::pause
    exit /b !ERRORLEVEL!
)

echo.
echo Programm beendet.
::pause