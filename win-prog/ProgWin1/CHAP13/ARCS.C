/* ARCS.C -- Demonstrates Drawing Arcs, Chords, and Pies with GDI */

#include <windows.h>
#include "arcs.h"

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Arcs" ;
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
          wndclass.lpszMenuName  = szAppName ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Arcs, Chords, and Pies",
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
     static short xClient, yClient, x1, x2, x3, x4, y1, y2, y3, y4,
                  nFigure = IDM_ARC ;
     HDC          hDC ;
     HMENU        hMenu ; 
     HPEN         hPen ;
     PAINTSTRUCT  ps ;
     short        x, y ;

     switch (iMessage)
          {
          case WM_SIZE:
               x3 = y3 = 0 ;
               x4 = xClient = LOWORD (lParam) ;
               y4 = yClient = HIWORD (lParam) ;
               x2 = 3 * (x1 = xClient / 4) ;
               y2 = 3 * (y1 = yClient / 4) ;
               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDM_ARC:
                    case IDM_CHORD:
                    case IDM_PIE:
                         hMenu = GetMenu (hWnd) ;
                         CheckMenuItem (hMenu, nFigure, MF_UNCHECKED) ;
                         CheckMenuItem (hMenu, nFigure = wParam, MF_CHECKED) ;
                         InvalidateRect (hWnd, NULL, FALSE) ;
                         break ;
                    }
               break ;

          case WM_LBUTTONDOWN:
               if (!(wParam & MK_SHIFT))
                    {
                    x3 = LOWORD (lParam) ;
                    y3 = HIWORD (lParam) ;
                    InvalidateRect (hWnd, NULL, TRUE) ;
                    break ;
                    }    
                                        /* fall through for MK_SHIFT */
          case WM_RBUTTONDOWN:
               x4 = LOWORD (lParam) ;   
               y4 = HIWORD (lParam) ;
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               hPen = SelectObject (hDC, CreatePen (PS_DOT, 1, 0L)) ;
               Rectangle (hDC, x1, y1, x2, y2) ;
               Ellipse   (hDC, x1, y1, x2, y2) ;

               DeleteObject (SelectObject (hDC, CreatePen (PS_SOLID, 3, 0L))) ;

               switch (nFigure)
                    {
                    case IDM_ARC:
                         Arc (hDC, x1, y1, x2, y2, x3, y3, x4, y4) ;
                         break ;

                    case IDM_CHORD:
                         Chord (hDC, x1, y1, x2, y2, x3, y3, x4, y4) ;
                         break ;

                    case IDM_PIE:
                         Pie (hDC, x1, y1, x2, y2, x3, y3, x4, y4) ;
                         break ;
                    }

               DeleteObject (SelectObject (hDC, hPen)) ;

               MoveTo (hDC, x3, y3) ;
               LineTo (hDC, xClient / 2, yClient / 2) ;
               LineTo (hDC, x4, y4) ;

               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
