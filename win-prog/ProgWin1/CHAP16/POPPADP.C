/* POPPADP.C -- Popup Editor Printing Functions */

#include <windows.h>
#include <string.h>
#include "filedlg.h"                    /* for IDD_FNAME definition */

extern char szAppName [] ;              /* in POPPAD.C */

BOOL bUserAbort ;
HWND hDlgPrint ;

BOOL FAR PASCAL PrintDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_INITDIALOG:
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

BOOL FAR PASCAL AbortProc (hPrinterDC, nCode)
     HDC   hPrinterDC ;
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

HDC GetPrinterDC ()
     {
     char szPrinter [64] ;
     char *szDevice, *szDriver, *szOutput ;

     GetProfileString ("windows", "device", "", szPrinter, 64) ;

     if ((szDevice = strtok (szPrinter, "," )) &&
         (szDriver = strtok (NULL,      ", ")) && 
         (szOutput = strtok (NULL,      ", ")))
          
               return CreateDC (szDriver, szDevice, szOutput, NULL) ;

     return 0 ;
     }

BOOL PrintFile (hInst, hWnd, hWndEdit, szFileName)
     HANDLE     hInst ;
     HWND       hWnd, hWndEdit ;
     char       *szFileName ;
     {
     BOOL       bError = FALSE ;
     char       szMsg [40] ;
     FARPROC    lpfnAbortProc, lpfnPrintDlgProc ;
     HDC        hPrnDC ;
     NPSTR      psBuffer ;
     RECT       rect ;
     short      yChar, nCharsPerLine, nLinesPerPage,
                nTotalLines, nTotalPages, nPage, nLine, nLineNum = 0 ;
     TEXTMETRIC tm ;

     if (0 == (nTotalLines = (short) SendMessage (hWndEdit,
                                             EM_GETLINECOUNT, 0, 0L)))
          return FALSE ;

     if (NULL == (hPrnDC = GetPrinterDC ()))
          return TRUE ;

     GetTextMetrics (hPrnDC, &tm) ;
     yChar = tm.tmHeight + tm.tmExternalLeading ;

     nCharsPerLine = GetDeviceCaps (hPrnDC, HORZRES) / tm.tmAveCharWidth ;
     nLinesPerPage = GetDeviceCaps (hPrnDC, VERTRES) / yChar ;
     nTotalPages   = (nTotalLines + nLinesPerPage - 1) / nLinesPerPage ;

     psBuffer = (NPSTR) LocalAlloc (LPTR, nCharsPerLine) ;

     EnableWindow (hWnd, FALSE) ;

     bUserAbort = FALSE ;
     lpfnPrintDlgProc = MakeProcInstance (PrintDlgProc, hInst) ;
     hDlgPrint = CreateDialog (hInst, "PrintDlgBox", hWnd, lpfnPrintDlgProc) ;
     SetDlgItemText (hDlgPrint, IDD_FNAME, szFileName) ;

     lpfnAbortProc = MakeProcInstance (AbortProc, hInst) ;
     Escape (hPrnDC, SETABORTPROC, 0, (LPSTR) lpfnAbortProc, NULL) ;

     strcat (strcat (strcpy (szMsg, szAppName), " - "), szFileName) ;
                                        
     if (Escape (hPrnDC, STARTDOC, strlen (szMsg), szMsg, NULL) > 0)
          {
          for (nPage = 0 ; nPage < nTotalPages ; nPage++)
               {
               for (nLine = 0 ; nLine < nLinesPerPage &&
                                nLineNum < nTotalLines ; nLine++, nLineNum++)
                    {
                    *(short *) psBuffer = nCharsPerLine ;

                    TextOut (hPrnDC, 0, yChar * nLine, psBuffer,
                         (short) SendMessage (hWndEdit, EM_GETLINE,
                                        nLineNum, (LONG) (LPSTR) psBuffer)) ;
                    }

               if (Escape (hPrnDC, NEWFRAME, 0, NULL, (LPSTR) &rect) < 0)
                    {
                    bError = TRUE ;
                    break ;
                    }

               if (bUserAbort)
                    break ;
               }
          }
     else
          bError = TRUE ;

     if (!bError)
          Escape (hPrnDC, ENDDOC, 0, NULL, NULL) ;

     if (!bUserAbort)
          {
          EnableWindow (hWnd, TRUE) ;
          DestroyWindow (hDlgPrint) ;
          }

     if (bError || bUserAbort)
          {
          strcat (strcpy (szMsg, "Could not print: "), szFileName) ;
          MessageBox (hWnd, szMsg, szAppName, MB_OK | MB_ICONEXCLAMATION) ;
          }

     LocalFree ((LOCALHANDLE) psBuffer) ;
     FreeProcInstance (lpfnPrintDlgProc) ;
     FreeProcInstance (lpfnAbortProc) ;
     DeleteDC (hPrnDC) ;

     return bError || bUserAbort ;
     }
