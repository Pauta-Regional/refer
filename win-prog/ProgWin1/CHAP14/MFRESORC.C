/* MFRESORC.C -- Metafile Resource Program */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "MFResorc" ;
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

     hWnd = CreateWindow (szAppName, "Metafile Resource Program",
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
     static HANDLE hMF ;
     static short  xClient, yClient ;
     HANDLE        hInstance, hResource ;
     HDC           hDC ;
     PAINTSTRUCT   ps ;
     short         x, y ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInstance = ((LPCREATESTRUCT) lParam) -> hInstance ;

               hResource = LoadResource (hInstance, 
                           FindResource (hInstance, "MyLogo", "METAFILE")) ;

               LockResource (hResource) ;

               hMF = SetMetaFileBits (hResource) ;

               GlobalUnlock (hResource) ;
               break ;

          case WM_SIZE:
               xClient = LOWORD (lParam) ;
               yClient = HIWORD (lParam) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               SetMapMode (hDC, MM_ANISOTROPIC) ;
               SetWindowExt (hDC, 1000, 1000) ;
               SetViewportExt (hDC, xClient, yClient) ;

               for (x = 0 ; x < 10 ; x++)
                    for (y = 0 ; y < 10 ; y++)
                         {
                         SetWindowOrg (hDC, -100 * x, -100 * y) ;
                         PlayMetaFile (hDC, hMF) ;
                         }
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               DeleteMetaFile (hMF) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
