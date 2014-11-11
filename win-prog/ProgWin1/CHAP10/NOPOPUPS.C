/* NOPOPUPS.C -- Demonstrates No-Popup Nested Menu */

#include <windows.h>
#include "nopopups.h"

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "NoPopUps" ;
     HWND        hWnd ;
     MSG         msg ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "No-Popup Nested Menu Demonstration",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static HMENU hMenuMain, hMenuEdit, hMenuFile ;
     HANDLE       hInstance ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInstance = GetWindowWord (hWnd, GWW_HINSTANCE) ;

               hMenuMain = LoadMenu (hInstance, "MenuMain") ;
               hMenuFile = LoadMenu (hInstance, "MenuFile") ;
               hMenuEdit = LoadMenu (hInstance, "MenuEdit") ;

               SetMenu (hWnd, hMenuMain) ;
               break ;

          case WM_COMMAND:

               switch (wParam)
                    {
                    case IDM_MAIN:
                         SetMenu (hWnd, hMenuMain) ;
                         break ;

                    case IDM_FILE:
                         SetMenu (hWnd, hMenuFile) ;
                         break ;

                    case IDM_EDIT:
                         SetMenu (hWnd, hMenuEdit) ;
                         break ;

                    case IDM_NEW:
                    case IDM_OPEN:
                    case IDM_SAVE:
                    case IDM_SAVEAS:

                    case IDM_UNDO:
                    case IDM_CUT:
                    case IDM_COPY:
                    case IDM_PASTE:
                    case IDM_CLEAR:

                         MessageBeep (0) ;
                         break ;
                    }
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
