/* ENVIRON.C -- Environment List Box */

#include <windows.h>
#include <stdlib.h>
#include <string.h>
#define  MAXENV  256

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     static   char szAppName [] = "Environ" ;
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
          wndclass.hbrBackground = COLOR_WINDOW + 1 ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Environment List Box",
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
     HWND        hWnd ;
     unsigned    iMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     static HWND hWndList, hWndText ;
     char        szBuffer [MAXENV + 1] ;
     HDC         hDC ;
     TEXTMETRIC  tm ;
     WORD        n ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               ReleaseDC (hWnd, hDC) ;

               hWndList = CreateWindow ("listbox", NULL,
                              WS_CHILD | WS_VISIBLE | LBS_STANDARD,
                              tm.tmAveCharWidth, tm.tmHeight * 3,
                              tm.tmAveCharWidth * 16 +
                                   GetSystemMetrics (SM_CXVSCROLL),
                              tm.tmHeight * 5,
                              hWnd, 1,
                              GetWindowWord (hWnd, GWW_HINSTANCE), NULL) ;

               hWndText = CreateWindow ("static", NULL,
                              WS_CHILD | WS_VISIBLE | SS_LEFT,
                              tm.tmAveCharWidth,          tm.tmHeight,
                              tm.tmAveCharWidth * MAXENV, tm.tmHeight,
                              hWnd, 2,
                              GetWindowWord (hWnd, GWW_HINSTANCE), NULL) ;

               for (n = 0 ; environ[n] ; n++)
                    {
                    if (strlen (environ [n]) > MAXENV)
                         continue ;
                    *strchr (strcpy (szBuffer, environ [n]), '=') = '\0' ;
                    SendMessage (hWndList, LB_ADDSTRING, 0, (LONG) szBuffer) ;
                    }
               break ;

          case WM_SETFOCUS:
               SetFocus (hWndList) ;
               break ;

          case WM_COMMAND:

               if (wParam == 1 && HIWORD (lParam) == LBN_SELCHANGE)
                    {
                    n = (WORD) SendMessage (hWndList, LB_GETCURSEL, 0, 0L) ;
                    n = (WORD) SendMessage (hWndList, LB_GETTEXT, n,
                                             (LONG) szBuffer) ;

                    strcpy (szBuffer + n + 1, getenv (szBuffer)) ;
                    *(szBuffer + n) = '=' ;

                    SetWindowText (hWndText, szBuffer) ;
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
