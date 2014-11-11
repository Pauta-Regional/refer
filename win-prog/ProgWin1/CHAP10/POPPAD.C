/* POPPAD.C -- Popup Editor (second version: includes menu) */

#include <windows.h>
#include "poppad.h"

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG);

char szAppName [] = "PopPad" ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance;
     LPSTR    lpszCmdLine;
     int      nCmdShow;
     {
     HANDLE   hAccel ;
     HWND     hWnd ;
     MSG      msg;
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

     hWnd = CreateWindow (szAppName, szAppName,
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         GetSystemMetrics (SM_CXSCREEN) / 2,
                         GetSystemMetrics (SM_CYSCREEN) / 2,
                         NULL, NULL, hInstance, NULL) ;

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

AskConfirmation (hWnd)
     HWND  hWnd ;
     {
     return MessageBox (hWnd, "Really want to close PopPad?",
                            szAppName, MB_YESNO | MB_ICONQUESTION) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd;
     unsigned    iMessage;
     WORD        wParam;
     LONG        lParam;
     {
     static HWND hWndEdit ;
     LONG        lSelect ;
     WORD        wEnable ;

     switch (iMessage)
          {
          case WM_CREATE:
               hWndEdit = CreateWindow ("edit", NULL,
                         WS_CHILD | WS_VISIBLE | WS_HSCROLL | WS_VSCROLL |
                              WS_BORDER | ES_LEFT | ES_MULTILINE |
                              ES_AUTOHSCROLL | ES_AUTOVSCROLL,
                         0, 0, 0, 0,
                         hWnd, 1, ((LPCREATESTRUCT) lParam)->hInstance, NULL) ;
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

               if (LOWORD (lParam))
                    {
                    if (wParam == 1 && HIWORD (lParam) == EN_ERRSPACE)
                         MessageBox (hWnd, "Edit control out of space.",
                              szAppName, MB_OK | MB_ICONHAND) ;
                    }
               else
                    {
                    switch (wParam)
                         {
                         case IDM_NEW:
                         case IDM_OPEN:
                         case IDM_SAVE:
                         case IDM_SAVEAS:
                         case IDM_PRINT:
                              MessageBeep (0) ;
                              break ;

                         case IDM_EXIT:
                              SendMessage (hWnd, WM_CLOSE, 0, 0L) ;
                              break ;

                         case IDM_ABOUT:
                              MessageBox (hWnd,
                                   "POPPAD by Charles Petzold, 1987",
                                   szAppName, MB_OK | MB_ICONASTERISK) ;
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
                    }
               break ;

          case WM_CLOSE:
               if (IDYES == AskConfirmation (hWnd))
                    DestroyWindow (hWnd) ;
               break ;

          case WM_QUERYENDSESSION:
               if (IDYES == AskConfirmation (hWnd))
                    return 1L ;
               break ;

          case WM_DESTROY :
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
