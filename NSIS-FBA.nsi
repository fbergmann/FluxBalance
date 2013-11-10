; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "SBW Flux Balance"
!define PRODUCT_VERSION "1.9"
!define PRODUCT_PUBLISHER "Frank T. Bergmann"
!define PRODUCT_WEB_SITE "http://fbergmann.github.com/FluxBalance/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\LPsolveSBMLUI.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

SetCompressor bzip2

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\LPsolveSBMLUI.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; Reserve files
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "SetupFBA-${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\KGI\SBW\FluxBalance"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite try
  
  File "bin\AutoLayout.dll"
  File "bin\libsbmlcs.dll"
  File "bin\libsbmlcsP.dll"
  File "bin\libstructural.dll"
  File "bin\LibStructuralCSharp.dll"
  File "bin\lpsolve55.dll"
  File "bin\LPsolveSBML.dll"
  File "bin\OntologyStorage.dll"
  File "bin\OntologyStorage.XmlSerializers.dll"
  File "bin\SBMLExtension.dll"
  File "bin\SBMLSupport.dll"
  File "bin\SBWCSharp.dll"
  
  File "bin\LPsolveSBMLUI.exe"
  CreateDirectory "$SMPROGRAMS\SBW Flux Balance"
  CreateShortCut "$SMPROGRAMS\SBW Flux Balance\SBW Flux Balance.lnk" "$INSTDIR\LPsolveSBMLUI.exe"
  CreateShortCut "$DESKTOP\SBW Flux Balance.lnk" "$INSTDIR\LPsolveSBMLUI.exe"
  
  DetailPrint "Register SBW Flux Balance"
  ExecWait '"$INSTDIR\LPsolveSBMLUI.exe" -sbwregister'
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\SBW Flux Balance\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\LPsolveSBMLUI.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\LPsolveSBMLUI.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\zlib1.dll"
  Delete "$INSTDIR\SBWCSharp.dll"
  Delete "$INSTDIR\SBMLExtension.dll"
  Delete "$INSTDIR\msvcr90.dll"
  Delete "$INSTDIR\msvcr71.dll"
  Delete "$INSTDIR\msvcp90.dll"
  Delete "$INSTDIR\msvcp71.dll"
  Delete "$INSTDIR\msvcm90.dll"
  Delete "$INSTDIR\LPsolveSBMLUI.vshost.exe"
  Delete "$INSTDIR\LPsolveSBMLUI.pdb"
  Delete "$INSTDIR\LPsolveSBMLUI.exe"
  Delete "$INSTDIR\LPsolveSBML.pdb"
  Delete "$INSTDIR\LPsolveSBML.dll"
  Delete "$INSTDIR\lpsolve55.dll"
  Delete "$INSTDIR\LibStructuralCSharp.dll"
  Delete "$INSTDIR\libstructural.dll"
  Delete "$INSTDIR\libsbmlcsP.dll"
  Delete "$INSTDIR\libsbmlcs.dll"
  Delete "$INSTDIR\libsbml3.dll"
  Delete "$INSTDIR\LibLA.dll"
  Delete "$INSTDIR\libexpat.dll"
  Delete "$INSTDIR\LibCLAPACK.dll"
  Delete "$INSTDIR\bzip2.dll"
  Delete "$INSTDIR\AutoLayout.dll"

  Delete "$SMPROGRAMS\SBW Flux Balance\Uninstall.lnk"
  Delete "$DESKTOP\SBW Flux Balance.lnk"
  Delete "$SMPROGRAMS\SBW Flux Balance\SBW Flux Balance.lnk"

  RMDir "$SMPROGRAMS\SBW Flux Balance"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd