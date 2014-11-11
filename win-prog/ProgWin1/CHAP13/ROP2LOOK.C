/* ROP2LOOK.C -- ROP2 Demonstration Program */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Rop2Look" ;
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

     hWnd = CreateWindow (szAppName, "ROP2 Demonstration Program",
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
     HWND          hWnd ;
     unsigned      iMessage ;
     WORD          wParam ;
     LONG          lParam ;
     {
     static LOGPEN lpBlack = { PS_SOLID, 1, 1, RGB (  0,   0,   0) },
                   lpWhite = { PS_SOLID, 1, 1, RGB (255, 255, 255) } ;
     static short  nDrawingMode = R2_COPYPEN ;
     HDC           hDC ;
     HMENU         hMenu ; 
     HPEN          hPenBlack, hPenWhite ;
     PAINTSTRUCT   ps ;
     RECT          rect ;
     short         i ;

     switch (iMessage)
          {
          case WM_COMMAND:
               hMenu = GetMenu (hWnd) ;
               CheckMenuItem (hMenu, nDrawingMode, MF_UNCHECKED) ;
               nDrawingMode = wParam ;
               CheckMenuItem (hMenu, nDrawingMode, MF_CHECKED) ;
               InvalidateRect (hWnd, NULL, FALSE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               hPenBlack = CreatePenIndirect (&lpBlack) ;
               hPenWhite = CreatePenIndirect (&lpWhite) ;

               SetMapMode (hDC, MM_ANISOTROPIC) ;
               GetClientRect (hWnd, &rect) ;
               SetViewportExt (hDC, rect.right, rect.bottom) ;
               SetWindowExt (hDC, 10, 4) ;

               for (i = 0 ; i < 10 ; i += 2)
                    {
                    SetRect (&rect, i, 0, i + 2, 4) ;
                    FillRect (hDC, &rect, GetStockObject (i / 2)) ;
                    }
               SetROP2 (hDC, nDrawingMode) ;

               SelectObject (hDC, hPenWhite) ;
               MoveTo (hDC, 1, 1) ;
               LineTo (hDC, 9, 1) ;

               SelectObject (hDC, hPenBlack) ;
               MoveTo (hDC, 1, 3) ;
               LineTo (hDC, 9, 3) ;

               EndPaint (hWnd, &ps) ;

               DeleteObject (hPenBlack) ;
               DeleteObject (hPenWhite) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
