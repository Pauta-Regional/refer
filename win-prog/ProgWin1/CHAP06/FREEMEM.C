/*  FREEMEM.C -- Free Memory Display Program */

#include <windows.h>
#include <stdlib.h>
#include <string.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "FreeMem" ;
     HDC         hDC ;
     HWND        hWnd ;
     MSG         msg ;
     TEXTMETRIC  tm ;
     WNDCLASS    wndclass ;

     if (hPrevInstance) 
          return FALSE ;

     wndclass.style         = CS_HREDRAW | CS_VREDRAW;
     wndclass.lpfnWndProc   = WndProc ;
     wndclass.cbClsExtra    = 0 ;
     wndclass.cbWndExtra    = 0 ;
     wndclass.hInstance     = hInstance ;
     wndclass.hIcon         = NULL ;
     wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
     wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
     wndclass.lpszMenuName  = NULL ;
     wndclass.lpszClassName = szAppName ;

     if (!RegisterClass (&wndclass))
          return FALSE ;

     hWnd = CreateWindow (szAppName, szAppName,
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     hDC = GetDC (hWnd) ;
     GetTextMetrics (hDC, &tm) ;
     ReleaseDC (hWnd, hDC) ;

     if (4 * tm.tmAveCharWidth > GetSystemMetrics (SM_CXICON) ||
               2 * tm.tmHeight > GetSystemMetrics (SM_CYICON))
          {
          MessageBox (hWnd, "Icon size too small for display!",
                         szAppName, MB_ICONEXCLAMATION | MB_OK) ;
          return FALSE ;
          }

     if (!SetTimer (hWnd, 1, 1000, NULL))
          {
          MessageBox (hWnd, "Too many clocks or timers!",
                         szAppName, MB_ICONEXCLAMATION | MB_OK) ;
          return FALSE ;
          }

     ShowWindow (hWnd, SW_SHOWMINNOACTIVE) ;
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
     static RECT rect ;
     static WORD wFreeMem, wPrevMem ;
     char        cBuffer [20] ;
     HDC         hDC ;
     PAINTSTRUCT ps ;

     switch (iMessage)
          {
          case WM_TIMER:
               wFreeMem = (WORD) (GlobalCompact (0L) / 1024) ;
               if (wFreeMem != wPrevMem)
                    InvalidateRect (hWnd, NULL, TRUE) ;
               wPrevMem = wFreeMem ;
               break ;

          case WM_SIZE:
               GetClientRect (hWnd, &rect) ;
               break ;               

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;
               DrawText (hDC, cBuffer,
                    strlen (strcat (itoa (wFreeMem, cBuffer, 10), "K Free")),
                    &rect, DT_WORDBREAK) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_QUERYOPEN:
               break ;

          case WM_DESTROY:
               KillTimer (hWnd, 1) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
