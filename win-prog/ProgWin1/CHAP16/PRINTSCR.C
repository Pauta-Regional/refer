/* PRINTSCR.C -- Printing Functions for WinPrtSc */

#include <windows.h>
#include <string.h>

BOOL   bUserAbort ;
HWND   hDlgPrint ;

extern char   szAppName [] ;
extern char   szSplMsg  [] ;
extern HANDLE hInst ;
extern HWND   hWnd ;

HDC GetPrinterDC ()
     {
     static char szPrinter [64] ;
     char        *szDevice, *szDriver, *szOutput ;

     GetProfileString ("windows", "device", "", szPrinter, 64) ;

     if ((szDevice = strtok (szPrinter, "," )) &&
         (szDriver = strtok (NULL,      ", ")) && 
         (szOutput = strtok (NULL,      ", ")))
          
               return CreateDC (szDriver, szDevice, szOutput, NULL) ;
     return 0 ;
     }

BOOL FAR PASCAL PrintDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     DWORD    lParam ;
     {
     switch (iMessage)
          {
          case WM_INITDIALOG:
               SetWindowText (hDlg, szAppName) ;
               EnableMenuItem (GetSystemMenu (hDlg, FALSE), SC_CLOSE,
                                                            MF_GRAYED) ;
               break ;

          case WM_COMMAND:
               bUserAbort = TRUE ;
               DestroyWindow (hDlg) ;
               hDlgPrint = 0 ;
               break ;

          default:
               return FALSE ;
          }
     return TRUE ;
     }          

BOOL FAR PASCAL AbortProc (hPrnDC, nCode)
     HDC   hPrnDC ;
     short nCode ;
     {
     MSG   msg ;

     while (!bUserAbort && PeekMessage (&msg, NULL, 0, 0, PM_REMOVE))
          {
          if (!hDlgPrint || !IsDialogMessage (hDlgPrint, &msg))
               {
               TranslateMessage (&msg) ;
               DispatchMessage (&msg) ;
               }
          }
     return !bUserAbort ;
     }

int PrintScreen ()
     {
     BOOL    bError = FALSE ;
     FARPROC lpfnAbortProc, lpfnPrintDlgProc ;
     HBITMAP hBitmap ;
     HDC     hScrDC, hPrnDC, hMemDC ;
     int     nReturnCode = 0 ;
     RECT    rect ;
     short   xScreen, yScreen, xPrinter, yPrinter, xBand, yBand ;

                    /*----------------------------------------*/
                    /* Get Printer DC and check for RC_BITBLT */
                    /*----------------------------------------*/

     if (NULL == (hPrnDC = GetPrinterDC ()))
          return 1 ;

     if (!(RC_BITBLT & GetDeviceCaps (hPrnDC, RASTERCAPS)))
          {
          DeleteDC (hPrnDC) ;
          return 2 ;               
          }

     xPrinter = GetDeviceCaps (hPrnDC, HORZRES) ;
     yPrinter = GetDeviceCaps (hPrnDC, VERTRES) ;

                    /*------------------------------------------------*/
                    /* Get Screen DC and bitmap; Blt screen to Mem DC */
                    /*------------------------------------------------*/

     hScrDC = CreateDC ("DISPLAY", NULL, NULL, NULL) ;
     xScreen = GetDeviceCaps (hScrDC, HORZRES) ;
     yScreen = GetDeviceCaps (hScrDC, VERTRES) ;

     if (!(hBitmap = CreateBitmap (xScreen, yScreen, 1, 1, NULL)))
          {
          DeleteDC (hPrnDC) ;
          DeleteDC (hScrDC) ;
          return 3 ;
          }

     hMemDC = CreateCompatibleDC (hScrDC) ;
     SelectObject (hMemDC, hBitmap) ;
     BitBlt (hMemDC, 0, 0, xScreen, yScreen, hScrDC, 0, 0, SRCCOPY) ;
     DeleteDC (hScrDC) ;

                    /*-----------------------------------------------*/
                    /* Create modeless dialog box and set abort proc */
                    /*-----------------------------------------------*/

     EnableWindow (hWnd, FALSE) ;

     bUserAbort = FALSE ;
     lpfnPrintDlgProc = MakeProcInstance (PrintDlgProc, hInst) ;
     hDlgPrint = CreateDialog (hInst, "PrintDlgBox", hWnd, lpfnPrintDlgProc) ;

     lpfnAbortProc = MakeProcInstance (AbortProc, hInst) ;
     Escape (hPrnDC, SETABORTPROC, 0, (LPSTR) lpfnAbortProc, NULL) ;

                    /*------------------------------------------------*/
                    /* Do StretchBlt (mem DC to prn DC) for each band */
                    /*------------------------------------------------*/
                                        
     if (Escape (hPrnDC, STARTDOC, strlen (szSplMsg), szSplMsg, NULL) > 0 &&
         Escape (hPrnDC, NEXTBAND, 0, NULL, (LPSTR) &rect))
          {
          while (!IsRectEmpty (&rect) && !bUserAbort)
               {
               (*lpfnAbortProc) (hPrnDC, 0) ;

               xBand = rect.right - rect.left ;
               yBand = rect.bottom - rect.top ;

               StretchBlt (hPrnDC, rect.left, rect.top, xBand, yBand,
                    hMemDC, (short) ((long) rect.left * xScreen / xPrinter),
                            (short) ((long) rect.top  * yScreen / yPrinter),
                            (short) ((long) xBand     * xScreen / xPrinter),
                            (short) ((long) yBand     * yScreen / yPrinter),
                    SRCCOPY) ;

               (*lpfnAbortProc) (hPrnDC, 0) ;

               if (Escape (hPrnDC, NEXTBAND, 0, NULL, (LPSTR) &rect) < 0)
                    {
                    bError = TRUE ;
                    nReturnCode = 4 ;
                    break ;
                    }
               }
          }
     else
          {
          bError = TRUE ;
          nReturnCode = 4 ;
          }
                    /*----------------------------------------------*/
                    /* Clean up print termination and delete bitmap */
                    /*----------------------------------------------*/
     if (!bError)
          {
          if (!bUserAbort)
               Escape (hPrnDC, ENDDOC, 0, NULL, NULL) ;
          else
               Escape (hPrnDC, ABORTDOC, 0, NULL, NULL) ;
          }

     if (!bUserAbort)
          DestroyWindow (hDlgPrint) ;
     else
          nReturnCode = 5 ;

     EnableWindow (hWnd, TRUE) ;
     FreeProcInstance (lpfnPrintDlgProc) ;
     FreeProcInstance (lpfnAbortProc) ;
     DeleteDC (hPrnDC) ;
     DeleteDC (hMemDC) ;
     DeleteObject (hBitmap) ;

     return nReturnCode ;
     }
