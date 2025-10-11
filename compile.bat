@echo off
setlocal enabledelayedexpansion

echo.
echo ====================================
echo     PBR Material Maker Compiler
echo ====================================
echo.

REM Aktuelles Verzeichnis setzen
cd /d "%~dp0"

REM Parameter prüfen
set "BUILD_TYPE=%1"
if "%BUILD_TYPE%"=="" set "BUILD_TYPE=both"

echo Verfügbare Optionen:
echo - compile.bat debug    (nur Debug-Version)
echo - compile.bat release  (nur Release-Version)
echo - compile.bat both     (beide Versionen, Standard)
echo - compile.bat clean    (alles bereinigen)
echo.

if /i "%BUILD_TYPE%"=="clean" (
    echo Bereinige Projekt...
    dotnet clean PBR_Material_Maker.sln
    if !ERRORLEVEL! EQU 0 (
        echo ✅ Projekt erfolgreich bereinigt.
    ) else (
        echo ❌ Fehler beim Bereinigen des Projekts.
        ::pause
        exit /b !ERRORLEVEL!
    )
    echo.
    ::pause
    exit /b 0
)

if /i "%BUILD_TYPE%"=="debug" (
    echo 🔨 Kompiliere Debug-Version...
    dotnet build PBR_Material_Maker.sln --configuration Debug
    if !ERRORLEVEL! EQU 0 (
        echo ✅ Debug-Build erfolgreich erstellt.
        echo 📁 Ausgabe: .\PBR Material Maker\bin\Debug\net10.0-windows\
    ) else (
        echo ❌ Fehler beim Debug-Build.
        ::pause
        exit /b !ERRORLEVEL!
    )
    
) else if /i "%BUILD_TYPE%"=="release" (
    echo 🚀 Kompiliere Release-Version...
    dotnet build PBR_Material_Maker.sln --configuration Release
    if !ERRORLEVEL! EQU 0 (
        echo ✅ Release-Build erfolgreich erstellt.
        echo 📁 Ausgabe: .\PBR Material Maker\bin\Release\net10.0-windows\
    ) else (
        echo ❌ Fehler beim Release-Build.
        ::pause
        exit /b !ERRORLEVEL!
    )
    
) else if /i "%BUILD_TYPE%"=="both" (
    echo 🔨 Kompiliere Debug-Version...
    dotnet build PBR_Material_Maker.sln --configuration Debug
    if !ERRORLEVEL! EQU 0 (
        echo ✅ Debug-Build erfolgreich erstellt.
    ) else (
        echo ❌ Fehler beim Debug-Build.
        ::pause
        exit /b !ERRORLEVEL!
    )
    
    echo.
    echo 🚀 Kompiliere Release-Version...
    dotnet build PBR_Material_Maker.sln --configuration Release
    if !ERRORLEVEL! EQU 0 (
        echo ✅ Release-Build erfolgreich erstellt.
    ) else (
        echo ❌ Fehler beim Release-Build.
        ::pause
        exit /b !ERRORLEVEL!
    )
    
    echo.
    echo 🎉 Beide Builds erfolgreich erstellt!
    echo 📁 Debug:   .\PBR Material Maker\bin\Debug\net10.0-windows\
    echo 📁 Release: .\PBR Material Maker\bin\Release\net10.0-windows\
    
) else (
    echo ❌ Unbekannter Parameter: %BUILD_TYPE%
    echo.
    echo Verwendung:
    echo   compile.bat [debug^|release^|both^|clean]
    echo.
    ::pause
    exit /b 1
)

echo.
echo Kompilierung abgeschlossen!

REM Zeige Dateigrößen und Datum
echo.
echo === Build-Informationen ===
if exist ".\PBR Material Maker\bin\Debug\net10.0-windows\PBR Material Maker.exe" (
    for %%F in (".\PBR Material Maker\bin\Debug\net10.0-windows\PBR Material Maker.exe") do (
        echo Debug:   %%~nxF - %%~zF Bytes - %%~tF
    )
)
if exist ".\PBR Material Maker\bin\Release\net10.0-windows\PBR Material Maker.exe" (
    for %%F in (".\PBR Material Maker\bin\Release\net10.0-windows\PBR Material Maker.exe") do (
        echo Release: %%~nxF - %%~zF Bytes - %%~tF
    )
)

echo.
pause