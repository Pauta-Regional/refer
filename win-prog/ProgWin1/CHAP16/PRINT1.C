/* PRINT1.C -- Bare Bones Printing */

#include <windows.h>

HDC  GetPrinterDC (void) ;              /* in PRINT.C */
void PageGDICalls (HDC, short, short) ;

HANDLE hInst ;
char   szAppName [] = "Print1" ;
char   szCaption [] = "Print Program 1" ;

BOOL PrintMyPage (hWnd)
     HWND        hWnd ;
     {
     static char szMessage [] = "Print1: Printing" ;
     BOOL        bError = FALSE ;
     HDC         hPrnDC ;
     short       xPage, yPage ;

     if (NULL == (hPrnDC = GetPrinterDC ()))
          return TRUE ;

     xPage = GetDeviceCaps (hPrnDC, HORZRES) ;
     yPage = GetDeviceCaps (hPrnDC, VERTRES) ;

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

     DeleteDC (hPrnDC) ;
     return bError ;
     }
