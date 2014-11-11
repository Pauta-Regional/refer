/* POPPAD.C -- Popup Editor using child window edit box */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG);

char szAppName [] = "PopPad" ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE    hInstance, hPrevInstance;
     LPSTR     lpszCmdLine;
     int       nCmdShow;
     {
     MSG       msg;
     HWND      hWnd ;
     WNDCLASS  wndclass ;

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
               return FALSE;
          }

     hWnd = CreateWindow (szAppName, szAppName,
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         GetSystemMetrics (SM_CXSCREEN) / 2,
                         GetSystemMetrics (SM_CYSCREEN) / 2,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd); 

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd;
     unsigned    iMessage;
     WORD        wParam;
     LONG        lParam;
     {
     static HWND hWndEdit ;

     switch (iMessage)
          {
          case WM_CREATE:
               hWndEdit = CreateWindow ("edit", NULL,
                         WS_CHILD | WS_VISIBLE | WS_HSCROLL | WS_VSCROLL |
                              WS_BORDER | ES_LEFT | ES_MULTILINE |
                              ES_AUTOHSCROLL | ES_AUTOVSCROLL,
                         0, 0, 0, 0,
                         hWnd, 1, 
                         ((LPCREATESTRUCT) lParam) -> hInstance, NULL) ;
               break ;

          case WM_SETFOCUS:
               SetFocus (hWndEdit) ;
               break ;

          case WM_SIZE: 
               MoveWindow (hWndEdit, 0, 0, LOWORD (lParam),
                                           HIWORD (lParam), TRUE) ;
               break ;

          case WM_COMMAND :
               if (wParam == 1 && HIWORD (lParam) == EN_ERRSPACE)
                    MessageBox (hWnd, "Edit control out of space.",
                              szAppName, MB_OK | MB_ICONHAND) ;
               break ;

          case WM_DESTROY :
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
