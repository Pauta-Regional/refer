/* PRINT2.C -- Printing with Abort Function */

#include <windows.h>

HDC  GetPrinterDC (void) ;              /* in PRINT.C */
void PageGDICalls (HDC, short, short) ;

HANDLE hInst ;
char   szAppName [] = "Print2" ;
char   szCaption [] = "Print Program 2 (Abort Function)" ;

BOOL FAR PASCAL AbortProc (hPrnDC, nCode)
     HDC   hPrnDC ;
     short nCode ;
     {
     MSG   msg ;

     while (PeekMessage (&msg, NULL, 0, 0, PM_REMOVE))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return TRUE ;
     }

BOOL PrintMyPage (hWnd)
     HWND        hWnd ;
     {
     static char szMessage [] = "Print2: Printing" ; 
     BOOL        bError = FALSE ;
     FARPROC     lpfnAbortProc ;
     HDC         hPrnDC ;
     RECT        rect ;
     short       xPage, yPage ;

     if (NULL == (hPrnDC = GetPrinterDC ()))
          return TRUE ;

     xPage = GetDeviceCaps (hPrnDC, HORZRES) ;
     yPage = GetDeviceCaps (hPrnDC, VERTRES) ;

     EnableWindow (hWnd, FALSE) ;

     lpfnAbortProc = MakeProcInstance (AbortProc, hInst) ;
     Escape (hPrnDC, SETABORTPROC, 0, (LPSTR) lpfnAbortProc, NULL) ;

     if (Escape (hPrnDC, STARTDOC, sizeof szMessage - 1, szMessage, NULL) > 0)
          {
          PageGDICalls (hPrnDC, xPage, yPage) ;

          if (Escape (hPrnDC, NEWFRAME, 0, NULL, NULL) > 0)
               Escape (hPrnDC, ENDDOC, 0, NULL, NULL) ;
          else
               bError = TRUE ;
          }
     else
          bError = TRUE ;

     if (!bError)
          Escape (hPrnDC, ENDDOC, 0, NULL, NULL) ;

     FreeProcInstance (lpfnAbortProc) ;
     EnableWindow (hWnd, TRUE) ;
     DeleteDC (hPrnDC) ;
     return bError ;
     }
