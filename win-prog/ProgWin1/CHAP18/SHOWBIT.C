/* SHOWBIT.C -- Shows bitmaps in BITLIB library */

#include <windows.h>

long  FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     static   char szAppName [] = "ShowBit" ;
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
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Show Bitmaps from BITLIB (Press Key)",
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

void DrawBitmap (hDC, xStart, yStart, hBitmap)
     HDC     hDC ;
     short   xStart, yStart ;
     HBITMAP hBitmap ;
     {
     BITMAP  bm ;
     DWORD   dwSize ;
     HDC     hMemDC ;
     POINT   pt ;

     hMemDC = CreateCompatibleDC (hDC) ;
     SelectObject (hMemDC, hBitmap) ;
     SetMapMode (hMemDC, GetMapMode (hDC)) ;

     GetObject (hBitmap, sizeof (BITMAP), (LPSTR) &bm) ;
     pt.x = bm.bmWidth ;
     pt.y = bm.bmHeight ;
     DPtoLP (hDC, &pt, 1) ;

     BitBlt (hDC, xStart, yStart, pt.x, pt.y, hMemDC, 0, 0, SRCCOPY) ;

     DeleteDC (hMemDC) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND          hWnd ;
     unsigned      iMessage ;
     WORD          wParam ;
     LONG          lParam ;
     {
     static HANDLE hLibrary ;
     static short  nCurrent = 1 ;
     HANDLE        hBitmap ;
     HDC           hDC ;
     PAINTSTRUCT   ps ;

     switch (iMessage)
          {
          case WM_CREATE:
               if ((hLibrary = LoadLibrary ("BITLIB.BML")) < 32)
                    MessageBeep (0) ;

               break ;

          case WM_CHAR:
               if (hLibrary >= 32)
                    {
                    if (NULL == (hBitmap = LoadBitmap (hLibrary,
                                             MAKEINTRESOURCE (nCurrent))))
                         {
                         nCurrent = 1 ;
                         hBitmap = LoadBitmap (hLibrary,
                                             MAKEINTRESOURCE (nCurrent)) ;
                         }
                    if (hBitmap)
                         {
                         OpenClipboard (hWnd) ;
                         EmptyClipboard () ;
                         SetClipboardData (CF_BITMAP, hBitmap) ;
                         CloseClipboard () ;
                         }
                    }
               nCurrent++ ;
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               OpenClipboard (hWnd) ;
               if (hBitmap = GetClipboardData (CF_BITMAP))
                    DrawBitmap (hDC, 0, 0, hBitmap) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               if (hLibrary >= 32)
                    FreeLibrary (hLibrary) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
