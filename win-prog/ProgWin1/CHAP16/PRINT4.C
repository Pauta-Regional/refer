/* PRINT4.C -- Printing with Banding */

#include <windows.h>

HDC  GetPrinterDC (void) ;              /* in PRINT.C */

HANDLE hInst ;
char   szAppName [] = "Print4" ;
char   szCaption [] = "Print Program 4 (Banding)" ;

BOOL   bUserAbort ;
HWND   hDlgPrint ;

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
               EnableWindow (GetParent (hDlg), TRUE) ;
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

BOOL PrintMyPage (hWnd)
     HWND        hWnd ;
     {
     static char szSpMsg [] = "Print4: Printing" ; 
     static char szText  [] = "Hello Printer!" ;
     BOOL        bError = FALSE ;
     DWORD       dwExtent ;
     FARPROC     lpfnAbortProc, lpfnPrintDlgProc ;
     HDC         hPrnDC ;
     POINT       ptExtent ;
     RECT        rect ;
     short       xPage, yPage ;

     if (NULL == (hPrnDC = GetPrinterDC ()))
          return TRUE ;

     xPage = GetDeviceCaps (hPrnDC, HORZRES) ;
     yPage = GetDeviceCaps (hPrnDC, VERTRES) ;

     EnableWindow (hWnd, FALSE) ;

     bUserAbort = FALSE ;
     lpfnPrintDlgProc = MakeProcInstance (PrintDlgProc, hInst) ;
     hDlgPrint = CreateDialog (hInst, "PrintDlgBox", hWnd, lpfnPrintDlgProc) ;

     lpfnAbortProc = MakeProcInstance (AbortProc, hInst) ;
     Escape (hPrnDC, SETABORTPROC, 0, (LPSTR) lpfnAbortProc, NULL) ;
                                        
     if (Escape (hPrnDC, STARTDOC, sizeof szSpMsg - 1, szSpMsg, NULL) > 0 &&
         Escape (hPrnDC, NEXTBAND, 0, NULL, (LPSTR) &rect) > 0)
          {
          while (!IsRectEmpty (&rect) && !bUserAbort)
               {
               (*lpfnAbortProc) (hPrnDC, 0) ;

               Rectangle (hPrnDC, rect.left, rect.top, rect.right, 
                                                       rect.bottom) ;
               (*lpfnAbortProc) (hPrnDC, 0) ;
          
               MoveTo (hPrnDC, 0, 0) ;
               LineTo (hPrnDC, xPage, yPage) ;

               (*lpfnAbortProc) (hPrnDC, 0) ;

               MoveTo (hPrnDC, xPage, 0) ;
               LineTo (hPrnDC, 0, yPage) ;

               SaveDC (hPrnDC) ;

               SetMapMode (hPrnDC, MM_ISOTROPIC) ;
               SetWindowExt   (hPrnDC, 1000, 1000) ;
               SetViewportExt (hPrnDC, xPage / 2, -yPage / 2) ;
               SetViewportOrg (hPrnDC, xPage / 2,  yPage / 2) ;

               (*lpfnAbortProc) (hPrnDC, 0) ;

               Ellipse (hPrnDC, -500, 500, 500, -500) ;

               (*lpfnAbortProc) (hPrnDC, 0) ;

               dwExtent = GetTextExtent (hPrnDC, szText, sizeof szText - 1) ;
               ptExtent = MAKEPOINT (dwExtent) ;
               TextOut (hPrnDC, -ptExtent.x / 2, ptExtent.y / 2, szText,
                                                  sizeof szText - 1) ;

               RestoreDC (hPrnDC, -1) ;

               (*lpfnAbortProc) (hPrnDC, 0) ;

               if (Escape (hPrnDC, NEXTBAND, 0, NULL, (LPSTR) &rect) < 0)
                    {
                    bError = TRUE ;
                    break ;
                    }
               }
          }
     else
          bError = TRUE ;

     if (!bError)
          {
          if (bUserAbort)
               Escape (hPrnDC, ABORTDOC, 0, NULL, NULL) ;
          else
               Escape (hPrnDC, ENDDOC, 0, NULL, NULL) ;
          }

     if (!bUserAbort)
          {
          EnableWindow (hWnd, TRUE) ;     
          DestroyWindow (hDlgPrint) ;
          }

     FreeProcInstance (lpfnPrintDlgProc) ;
     FreeProcInstance (lpfnAbortProc) ;
     DeleteDC (hPrnDC) ;

     return bError || bUserAbort ;
     }
