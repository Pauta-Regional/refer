/* RANDRECT.C -- Displays Random Rectangles */

#include <windows.h>
#include <stdlib.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
void DrawRectangle (HWND) ;

short xClient, yClient ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "RandRect" ;
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
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Random Rectangles",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd) ;

     while (TRUE)
          {
          if (PeekMessage (&msg, NULL, 0, 0, PM_REMOVE))
               {
               if (msg.message == WM_QUIT)
                    break ;

               TranslateMessage (&msg) ;
               DispatchMessage (&msg) ;
               }
          else
               DrawRectangle (hWnd) ;
          }
     return msg.wParam ;
     }

void DrawRectangle (hWnd)
     HWND   hWnd ;
     {
     HBRUSH hBrush ;
     HDC    hDC ;
     short  xLeft, xRight, yTop, yBottom, nRed, nGreen, nBlue ;

     xLeft   = rand () % xClient ;
     xRight  = rand () % xClient ;
     yTop    = rand () % yClient ;
     yBottom = rand () % yClient ;
     nRed    = rand () & 255 ;
     nGreen  = rand () & 255 ;
     nBlue   = rand () & 255 ;

     hDC = GetDC (hWnd) ;
     hBrush = CreateSolidBrush (RGB (nRed, nGreen, nBlue)) ;
     SelectObject (hDC, hBrush) ;

     Rectangle (hDC, min (xLeft, xRight), min (yTop, yBottom),
                     max (xLeft, xRight), max (yTop, yBottom)) ;

     ReleaseDC (hWnd, hDC) ;
     DeleteObject (hBrush) ;
     }     

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_SIZE:
               xClient = LOWORD (lParam) ;
               yClient = HIWORD (lParam) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
