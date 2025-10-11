@echo off
echo.
echo ====================================
echo    PBR Material Maker Builder
echo ====================================
echo.

cd /d "%~dp0"

echo Bereinige Projekt...
dotnet clean PBR_Material_Maker.sln

echo.
echo Kompiliere Debug-Version...
dotnet build PBR_Material_Maker.sln --configuration Debug

echo.
echo Kompiliere Release-Version...
dotnet build PBR_Material_Maker.sln --configuration Release

echo.
echo Build abgeschlossen!
echo.
pause