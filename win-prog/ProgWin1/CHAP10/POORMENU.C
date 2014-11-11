/* POORMENU.C -- The Poor Person's Menu */

#include <windows.h>

#define IDM_ABOUT   1
#define IDM_HELP    2
#define IDM_REMOVE  3

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

static char szAppName [] = "PoorMenu" ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     HMENU    hMenu ;
     HWND     hWnd ;
     MSG      msg ;
     WNDCLASS wndclass ;

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

     hWnd = CreateWindow (szAppName, "The Poor-Person\'s Menu",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     hMenu = GetSystemMenu (hWnd, FALSE);

     ChangeMenu (hMenu, NULL, NULL,               0,          MF_APPEND) ;
     ChangeMenu (hMenu, NULL, "About...",         IDM_ABOUT,  MF_APPEND) ;
     ChangeMenu (hMenu, NULL, "Help...",          IDM_HELP,   MF_APPEND) ;
     ChangeMenu (hMenu, NULL, "Remove Additions", IDM_REMOVE, MF_APPEND) ;

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
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_SYSCOMMAND:
               switch (wParam)
                    {
                    case IDM_ABOUT:

                         MessageBox (hWnd, "A Poor-Person\'s Menu Program.",
                                   szAppName, MB_OK | MB_ICONEXCLAMATION) ;
                         break ;

                    case IDM_HELP:

                         MessageBox (hWnd, "Help not yet implemented.",
                                   szAppName, MB_OK | MB_ICONEXCLAMATION) ;
                         break ;

                    case IDM_REMOVE:

                         GetSystemMenu (hWnd, TRUE) ;
                         break ;

                    default:
                         return DefWindowProc (hWnd, iMessage,
                                                  wParam, lParam) ;
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
