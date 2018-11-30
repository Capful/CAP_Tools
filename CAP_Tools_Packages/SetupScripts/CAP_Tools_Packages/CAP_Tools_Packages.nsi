 # ====================== 自定义宏 产品信息==============================
!define PRODUCT_NAME           		"CAP 工具箱"
!define PRODUCT_PATHNAME 			"CAP_Tools"  #安装卸载项用到的KEY
!define INSTALL_APPEND_PATH         "CAP Tools"	  #安装路径追加的名称 
!define INSTALL_DEFALT_SETUPPATH    "CAP Tools"       #默认生成的安装路径  
!define EXE_NAME               		"CAP Tools.exe"
!define PRODUCT_VERSION        		"1.0.0.0"
!define PRODUCT_PUBLISHER      		"Capful"
!define PRODUCT_LEGAL          		"Capful Copyright（c）2018"
!define INSTALL_OUTPUT_NAME    		"CAP_Tools_v${PRODUCT_VERSION}.exe"

# ====================== 自定义宏 安装信息==============================
!define INSTALL_7Z_PATH 	   		"..\app.7z"
!define INSTALL_7Z_NAME 	   		"app.7z"
!define INSTALL_RES_PATH       		"skin.zip"
!define INSTALL_LICENCE_FILENAME    "changelog.txt"
!define INSTALL_ICO 				"Install.ico"


!include "ui_cap_tools_packages.nsh"
# ==================== NSIS属性 ================================

# 针对Vista和win7 的UAC进行权限请求.
# RequestExecutionLevel none|user|highest|admin
RequestExecutionLevel admin

#SetCompressor zlib

; 安装包名字.
Name "${PRODUCT_NAME}"

# 安装程序文件名.

OutFile "..\..\Output\${INSTALL_OUTPUT_NAME}"


InstallDir "1"

# 安装和卸载程序图标
Icon              "${INSTALL_ICO}"
UninstallIcon     "Uninstall.ico"
