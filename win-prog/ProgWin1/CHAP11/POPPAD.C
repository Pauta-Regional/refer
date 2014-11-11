/* POPPAD.C -- Popup Editor with Dialog Boxes */

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include "winundoc.h"
#include "poppad.h"
#define  EDITID 1

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG);

BOOL ReadFile  (HANDLE, HWND, HWND, POFSTRUCT, char *, BOOL) ;
BOOL WriteFile (HANDLE, HWND, HWND, POFSTRUCT, char *, BOOL) ;
BOOL PrintFile (HANDLE, HWND, HWND, char *) ;

char szAppName  [] = "PopPad" ;
char szFileSpec [] = "*.TXT"  ;
char szUntitled [] = "(untitled)" ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance;
     LPSTR    lpszCmdLine;
     int      nCmdShow;
     {
     MSG      msg;
     HWND     hWnd ;
     HANDLE   hAccel ;
     WNDCLASS wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (hInstance, szAppName) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = szAppName ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE;
          }

     hWnd = CreateWindow (szAppName, NULL,
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         GetSystemMetrics (SM_CXSCREEN) / 2,
                         GetSystemMetrics (SM_CYSCREEN) / 2,
                         NULL, NULL, hInstance, lpszCmdLine) ;

     ShowWindow (hWnd, nCmdShow) ;

     UpdateWindow (hWnd); 

     hAccel = LoadAccelerators (hInstance, szAppName) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          if (!TranslateAccelerator (hWnd, hAccel, &msg))
               {
               TranslateMessage (&msg) ;
               DispatchMessage (&msg) ;
               }
          }
     return msg.wParam ;
     }

BOOL FAR PASCAL AboutDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_INITDIALOG:
               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDOK:
                         EndDialog (hDlg, 0) ;
                         break ;

                    default:
                         return FALSE ;
                    }
               break ;

          default:
               return FALSE ;
          }
     return TRUE ;
     }

void DoCaption (hWnd, szFileName)
     HWND hWnd ;
     char *szFileName ;
     {
     char szCaption [40] ;

     sprintf (szCaption, "%s - %s", szAppName,
               szFileName [0] ? szFileName : szUntitled) ;

     SetWindowText (hWnd, szCaption) ;
     }

short AskAboutSave (hWnd, szFileName)
     HWND  hWnd ;
     char  *szFileName ;
     {
     char  szBuffer [40] ;
     short nReturn ;

     sprintf (szBuffer, "Save current changes: %s",
               szFileName [0] ? szFileName : szUntitled) ;

     if (IDYES == (nReturn = MessageBox (hWnd, szBuffer, szAppName,
                                    MB_YESNOCANCEL | MB_ICONQUESTION)))

          if (!SendMessage (hWnd, WM_COMMAND, IDM_SAVE, 0L))
               return IDCANCEL ;

     return nReturn ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND           hWnd;
     unsigned       iMessage;
     WORD           wParam;
     LONG           lParam;
     {
     static BOOL    bNeedSave = FALSE ;
     static char    szRealFileName [16] ;
     static FARPROC lpfnAboutDlgProc ;
     static HWND    hInst, hWndEdit ;
     char           szFileName [16] ;
     LONG           lSelect ;
     OFSTRUCT       of ;
     WORD           wEnable ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInst = ((LPCREATESTRUCT) lParam)->hInstance ;
               lpfnAboutDlgProc = MakeProcInstance (AboutDlgProc, hInst) ;

               hWndEdit = CreateWindow ("edit", NULL,
                         WS_CHILD | WS_VISIBLE | WS_HSCROLL | WS_VSCROLL |
                              WS_BORDER | ES_LEFT | ES_MULTILINE |
                              ES_AUTOHSCROLL | ES_AUTOVSCROLL,
                         0, 0, 0, 0,
                         hWnd, EDITID, hInst, NULL) ;

               SendMessage (hWndEdit, EM_LIMITTEXT, 32000, 0L) ;

               if (lstrlen (((LPCREATESTRUCT) lParam)->lpCreateParams))
                    {
                    OpenFile (((LPCREATESTRUCT) lParam)->lpCreateParams,
                                        &of, OF_PARSE) ;
                    strcpy (szFileName, (PSTR)
                              AnsiNext (strrchr (of.szPathName, '\\'))) ;

                    if (ReadFile (hInst, hWnd, hWndEdit, &of,
                              szFileName, FALSE))
                         strcpy (szRealFileName, szFileName) ;
                    }
               DoCaption (hWnd, szRealFileName) ;
               break ;

          case WM_SETFOCUS:
               SetFocus (hWndEdit) ;
               break ;

          case WM_SIZE: 
               MoveWindow (hWndEdit, 0, 0, LOWORD (lParam),
                                           HIWORD (lParam), TRUE) ;
               break ;

          case WM_INITMENUPOPUP:

               if (lParam == 1)
                    {
                    EnableMenuItem (wParam, IDM_UNDO,
                         SendMessage (hWndEdit, EM_CANUNDO, 0, 0L) ?
                              MF_ENABLED : MF_GRAYED) ;

                    EnableMenuItem (wParam, IDM_PASTE,
                         IsClipboardFormatAvailable (CF_TEXT) ?
                              MF_ENABLED : MF_GRAYED) ;

                    lSelect = SendMessage (hWndEdit, EM_GETSEL, 0, 0L) ;

                    if (HIWORD (lSelect) == LOWORD (lSelect))
                         wEnable = MF_GRAYED ;
                    else
                         wEnable = MF_ENABLED ;

                    EnableMenuItem (wParam, IDM_CUT,   wEnable) ;
                    EnableMenuItem (wParam, IDM_COPY,  wEnable) ;
                    EnableMenuItem (wParam, IDM_CLEAR, wEnable) ;
                    }
               break ;

          case WM_COMMAND :

               if (LOWORD (lParam) && wParam == EDITID)
                    {
                    switch (HIWORD (lParam))
                         {
                         case EN_UPDATE:
                              bNeedSave = TRUE ;
                              break ;

                         case EN_ERRSPACE:
                              MessageBox (hWnd, "Edit control out of space.",
                                        szAppName, MB_OK | MB_ICONHAND) ;
                              break ;
                         }
                    break ;
                    }

               switch (wParam)
                    {
                    case IDM_NEW:
                         if (bNeedSave && IDCANCEL ==
                                   AskAboutSave (hWnd, szRealFileName))
                              break ;

                         SetWindowText (hWndEdit, "\0") ;
                         szRealFileName [0] = '\0' ;
                         DoCaption (hWnd, szRealFileName) ;
                         bNeedSave = FALSE ;
                         break ;

                    case IDM_OPEN:
                         if (bNeedSave && IDCANCEL ==
                                   AskAboutSave (hWnd, szRealFileName))
                              break ;

                         if (ReadFile (hInst, hWnd, hWndEdit, &of,
                                        szFileName, TRUE))
                              {
                              strcpy (szRealFileName, szFileName) ;
                              DoCaption (hWnd, szRealFileName) ;
                              bNeedSave = FALSE ;
                              }

                         break ;                              

                    case IDM_SAVE:
                         if (szRealFileName [0])
                              {
                              if (WriteFile (hInst, hWnd, hWndEdit, &of,
                                             szRealFileName, FALSE))
                                   {
                                   bNeedSave = FALSE ;
                                   return 1 ;
                                   }
                              break ;
                              }
                                                  /* fall through */
                    case IDM_SAVEAS:
                         if (WriteFile (hInst, hWnd, hWndEdit, &of,
                                        szFileName, TRUE))
                              {
                              strcpy (szRealFileName, szFileName) ;
                              DoCaption (hWnd, szFileName) ;
                              bNeedSave = FALSE ;
                              return 1 ;
                              }
                         break ;

                    case IDM_PRINT:
                         PrintFile (hInst, hWnd, hWndEdit,
                              szRealFileName [0] ? szRealFileName :
                                                   szUntitled) ;
                         break ;

                    case IDM_EXIT:
                         SendMessage (hWnd, WM_CLOSE, 0, 0L) ;
                         break ;

                    case IDM_ABOUT:
                         DialogBox (hInst, "AboutBox", hWnd,
                                   lpfnAboutDlgProc) ;
                         break ;

                    case IDM_UNDO:
                         SendMessage (hWndEdit, WM_UNDO, 0, 0L) ;
                         break ;

                    case IDM_CUT:
                         SendMessage (hWndEdit, WM_CUT, 0, 0L) ;
                         break ;

                    case IDM_COPY:
                         SendMessage (hWndEdit, WM_COPY, 0, 0L) ;
                         break ;

                    case IDM_PASTE:
                         SendMessage (hWndEdit, WM_PASTE, 0, 0L) ;
                         break ;

                    case IDM_CLEAR:
                         SendMessage (hWndEdit, WM_CLEAR, 0, 0L) ;
                         break ;

                    case IDM_SELALL:
                         SendMessage (hWndEdit, EM_SETSEL, 0,
                                        MAKELONG (0, 32767)) ;
                         break ; 
                    }
               break ;

          case WM_CLOSE:
               if (!bNeedSave || IDCANCEL !=
                         AskAboutSave (hWnd, szRealFileName))
                    DestroyWindow (hWnd) ;
               break ;

          case WM_QUERYENDSESSION:
               if (!bNeedSave || IDCANCEL !=
                         AskAboutSave (hWnd, szRealFileName))
                    return 1L ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
