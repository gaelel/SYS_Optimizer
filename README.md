# SYS_OPTIMIZER

A Windows maintenance tool designed to free up disk space by cleaning temporary files, disk cache, prefetch data, event logs, and more.

Built with **WPF (.NET)** and designed for Windows 10/11.

![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-blue)
![Framework](https://img.shields.io/badge/framework-.NET%2010-purple)
![Language](https://img.shields.io/badge/language-C%23-green)
![License](https://img.shields.io/badge/license-MIT-cyan)

---

## Features

- 6 cleaning modules organized by access level
- Bilingual interface: English and Spanish
- Detects administrator privileges automatically and locks restricted modules
- Language preference saved between sessions
- No additional dependencies required

---

## Modules

- Temp Files: cleans %TEMP% folder
- Basic Disk: empties recycle bin, thumbnail cache and error reports
- Deep Temp: cleans C:\Windows\Temp (requires admin)
- Prefetch: cleans C:\Windows\Prefetch (requires admin)
- Event Log: clears Windows event logs (requires admin)
- Advanced Disk: removes Windows.old, update cache and system logs (requires admin)

> Modules that require Administrator will appear disabled if the app is not running with elevated privileges.

---

## Requirements

- Windows 10 or 11 (x64)
- No additional installs required, the app is self-contained

---

## Installation

1. Go to the [Releases](../../releases) page
2. Download the latest `SYS_OPTIMIZER_vX.X.X.zip`
3. Extract the ZIP anywhere on your PC
4. Run `OptimizationAPP.exe`

> To access all modules, right-click the executable and select **Run as administrator**.

---

## Disclaimer

The use of this tool is entirely the user's responsibility. The developer is not responsible for data loss, system issues or any damage resulting from the use of this application. It is recommended to create a system restore point before running any cleaning module.

---

## License

MIT — feel free to use, modify and distribute.
