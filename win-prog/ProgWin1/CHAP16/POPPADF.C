/* POPPADF -- Popup Notepad File I/O */

#include <windows.h>
#include <stdio.h>
#include "winundoc.h"
                                        /* in FILEDLG.C */

int DoFileOpenDlg (HANDLE, WORD, char *, char *, WORD,   char *, POFSTRUCT) ;
int DoFileSaveDlg (HANDLE, WORD, char *, char *, WORD *, char *, POFSTRUCT) ;

extern char szAppName  [] ;             /* in POPPAD.C */
extern char szFileSpec [] ;

long FileLength (hFile)
     HANDLE hFile ;
     {
     long   lCurrentPos = _llseek (hFile, 0L, 1) ;
     long   lFileLength = _llseek (hFile, 0L, 2) ;
     
     _llseek (hFile, lCurrentPos, 0) ;

     return lFileLength ;
     }

void OkMessageBox (hWnd, szString, szFileName)
     HWND hWnd ;
     char *szString, *szFileName ;
     {
     char szBuffer [40] ;

     sprintf (szBuffer, szString, szFileName) ;

     MessageBox (hWnd, szBuffer, szAppName, MB_OK | MB_ICONEXCLAMATION) ;
     }

BOOL ReadFile (hInstance, hWnd, hWndEdit, pof, szFileName, bAskName)
     HANDLE    hInstance, hWnd, hWndEdit ;
     POFSTRUCT pof ;
     char      *szFileName ;
     BOOL      bAskName ;
     {
     DWORD     dwLength ;
     HANDLE    hFile, hTextBuffer ;
     LPSTR     lpTextBuffer ;

     if (bAskName)
          {
          if (!DoFileOpenDlg (hInstance, hWnd, szFileSpec, szFileSpec + 1,
                                        0x4010, szFileName, pof))
               return FALSE ;
          }

     if (-1 == (hFile = OpenFile (szFileName, pof, OF_READ | OF_REOPEN)))
          {
          OkMessageBox (hWnd, "Cannot open file %s", szFileName) ;
          return FALSE ;
          }

     if ((dwLength = FileLength (hFile)) >= 32000)
          {
          _lclose (hFile) ;
          OkMessageBox (hWnd, "File %s too large", szFileName) ;
          return FALSE ;
          }

     if (NULL == (hTextBuffer = GlobalAlloc (GHND, (DWORD) dwLength + 1)))
          {
          _lclose (hFile) ;
          OkMessageBox (hWnd, "Cannot allocate memory for %s", szFileName) ;
          return FALSE ;
          }

     lpTextBuffer = GlobalLock (hTextBuffer) ;
     _lread (hFile, lpTextBuffer, (WORD) dwLength) ;
     _lclose (hFile) ;
     lpTextBuffer [(WORD) dwLength] = '\0' ;

     SetWindowText (hWndEdit, lpTextBuffer) ;
     GlobalUnlock (hTextBuffer) ;
     GlobalFree (hTextBuffer) ;

     return TRUE ;
     }

BOOL WriteFile (hInstance, hWnd, hWndEdit, pof, szFileName, bAskName) 
     HANDLE    hInstance, hWnd, hWndEdit ;
     POFSTRUCT pof ;
     char      *szFileName ;
     BOOL      bAskName ;
     {
     char      szBuffer [40] ;
     HANDLE    hFile, hTextBuffer ;
     NPSTR     npTextBuffer ;
     WORD      wStatus, wLength ;

     if (bAskName)
          {
          if (!DoFileSaveDlg (hInstance, hWnd, szFileSpec, szFileSpec + 1,
                                   &wStatus, szFileName, pof))
               return FALSE ;

          if (wStatus == 1)
               {
               sprintf (szBuffer, "Replace existing %s", szFileName) ;
               if (IDNO == MessageBox (hWnd, szBuffer, szAppName, 
                                             MB_YESNO | MB_ICONQUESTION))
                    return FALSE ;
               }
          }
     else
          OpenFile (szFileName, pof, OF_PARSE) ;

     if (-1 == (hFile = OpenFile (szFileName, pof, OF_CREATE | OF_REOPEN)))
          {
          OkMessageBox (hWnd, "Cannot create file %s", szFileName) ;
          return FALSE ;
          }

     wLength = GetWindowTextLength (hWndEdit) ;
     hTextBuffer = (HANDLE) SendMessage (hWndEdit, EM_GETHANDLE, 0, 0L) ;
     npTextBuffer = LocalLock (hTextBuffer) ;

     if (wLength != _lwrite (hFile, npTextBuffer, wLength))
          {
          _lclose (hFile) ;
          OkMessageBox (hWnd, "Cannot write file %s to disk", szFileName) ;
          return FALSE ;
          }

     _lclose (hFile) ;
     LocalUnlock (hTextBuffer) ;

     return TRUE ;
     }
