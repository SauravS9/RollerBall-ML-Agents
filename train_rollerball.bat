@echo off
title RollerBall training
call "%USERPROFILE%\miniforge3\condabin\conda.bat" activate mlagents
cd /d "%USERPROFILE%\ml-agents"

REM ml-agents still imports pkg_resources, which new setuptools removed -> pin an older one
python -c "import pkg_resources" 2>nul || (
    echo Fixing setuptools ^(pkg_resources missing^) ...
    python -m pip install "setuptools<70"
)
call mlagents-learn --help >nul || (
    echo mlagents-learn still not working. Check the error above.
    pause
    exit /b 1
)

echo.
echo  Starting trainer. When you see "Listening on port 5004", press PLAY in Unity.
echo.
mlagents-learn config/rollerball_config.yaml --run-id=RollerBall --force --timeout-wait=600
echo.
echo Training finished. Model: %USERPROFILE%\ml-agents\results\RollerBall\RollerBall.onnx
pause
