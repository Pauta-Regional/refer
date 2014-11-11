/* CLIPVIEW.C -- Simple Clipboard Viewer */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE    hInstance, hPrevInstance ;
     LPSTR     lpszCmdLine ;
     int       nCmdShow ;
     {
     static    char szAppName [] = "ClipView" ;
     HWND      hWnd ;
     MSG       msg ;
     WNDCLASS  wndclass ;

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

     hWnd = CreateWindow (szAppName, "Simple Clipboard Viewer (Text Only)",
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
     static HWND hWndNextViewer ;
     HANDLE      hGMem ;
     HDC         hDC ;
     LPSTR       lpGMem ;
     PAINTSTRUCT ps ;
     RECT        rect ;

     switch (iMessage)
          {
          case WM_CREATE:
               hWndNextViewer = SetClipboardViewer (hWnd) ;
               break ;

          case WM_CHANGECBCHAIN :
               if (wParam == hWndNextViewer)
                    hWndNextViewer = LOWORD (lParam) ;

               else if (hWndNextViewer)
                    SendMessage (hWndNextViewer, iMessage, wParam, lParam) ;

               break ;

          case WM_DRAWCLIPBOARD :
               if (hWndNextViewer)
                    SendMessage (hWndNextViewer, iMessage, wParam, lParam) ;

               InvalidateRect (hWnd, NULL, TRUE) ;
               break;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;
               GetClientRect (hWnd, &rect) ;
               OpenClipboard (hWnd) ;

               if (hGMem = GetClipboardData (CF_TEXT))
                    {
                    lpGMem = GlobalLock (hGMem) ;
                    DrawText (hDC, lpGMem, -1, &rect, DT_EXPANDTABS) ;
                    GlobalUnlock (hGMem) ;
                    }

               CloseClipboard () ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               ChangeClipboardChain (hWnd, hWndNextViewer) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
