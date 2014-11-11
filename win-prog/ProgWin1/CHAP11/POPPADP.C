/* POPPADP.C -- Popup Notepad Printing */

#include <windows.h>

extern char szAppName [] ;              /* in POPPAD.C */

BOOL FAR PASCAL PrintDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     return FALSE ;
     }

BOOL FAR PASCAL AbortProc (hPrinterDC, nCode)
     HDC   hPrinterDC ;
     short nCode ;
     {
     return FALSE ;
     }

BOOL PrintFile (hInstance, hWnd, hWndEdit, szFileName)
     HANDLE    hInstance, hWnd, hWndEdit ;
     char      *szFileName ;
     {
     MessageBox (hWnd, "Printing not yet implemented", szAppName,
                         MB_OK | MB_ICONEXCLAMATION) ;
     return FALSE ;
     }
