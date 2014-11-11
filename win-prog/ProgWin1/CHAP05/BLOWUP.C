/* BLOWUP.C -- Screen Capture Mouse Demo Program */

#include <windows.h>

long  FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "BlowUp" ;
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

     hWnd = CreateWindow (szAppName, "Blow-Up Mouse Demo",
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

void InvertBlock (hWnd, beg, end)
     HWND  hWnd ;
     POINT beg, end ;
     {
     HDC   hDC ;

     hDC = CreateDC ("DISPLAY", NULL, NULL, NULL) ;
     ClientToScreen (hWnd, &beg) ;
     ClientToScreen (hWnd, &end) ;
     PatBlt (hDC, beg.x, beg.y, end.x - beg.x, end.y - beg.y, DSTINVERT) ;
     DeleteDC (hDC) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static BOOL  bCapturing, bBlocking ;
     static POINT beg, end ;
     HDC          hDC ;
     RECT         rect ;

     switch (iMessage)
          {
          case WM_LBUTTONDOWN:
               if (!bCapturing)
                    {
                    bCapturing = TRUE ;
                    SetCapture (hWnd) ;
                    SetCursor (LoadCursor (NULL, IDC_CROSS)) ;
                    }
               else if (!bBlocking)
                    {
                    bBlocking = TRUE ;
                    beg = MAKEPOINT (lParam) ;
                    }
               break ;

          case WM_MOUSEMOVE:
               if (bBlocking)
                    {
                    end = MAKEPOINT (lParam) ;
                    InvertBlock (hWnd, beg, end) ;
                    InvertBlock (hWnd, beg, end) ;
                    }
               break ;

          case WM_LBUTTONUP:
               if (bBlocking)
                    {
                    bCapturing = bBlocking = FALSE ;
                    end = MAKEPOINT (lParam) ;
                    SetCursor (LoadCursor (NULL, IDC_WAIT)) ;

                    hDC = GetDC (hWnd) ; 
                    GetClientRect (hWnd, &rect) ;
                    StretchBlt (hDC, 0, 0, rect.right, rect.bottom,
                                hDC, beg.x, beg.y, 
                                end.x - beg.x, end.y - beg.y,
                                SRCCOPY) ;

                    ReleaseDC (hWnd, hDC) ;
                    SetCursor (LoadCursor (NULL, IDC_ARROW)) ;
                    ReleaseCapture () ;
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
