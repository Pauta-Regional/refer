/* ABOUT1.C -- About Box Demo Program No. 1 */

#include <windows.h>

#define IDM_ABOUT 1

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG);

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance;
     LPSTR       lpszCmdLine;
     int         nCmdShow;
     {
     static char szAppName [] = "About1" ;
     MSG         msg;
     HWND        hWnd ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (hInstance, szAppName) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE;
          }

     hWnd = CreateWindow (szAppName, "About Box Demo Program",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
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

BOOL FAR PASCAL AboutDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_INITDIALOG:
               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDOK:
                         EndDialog (hDlg, 0) ;
                         break ;

                    default:
                         return FALSE ;
                    }
               break ;

          default:
               return FALSE ;
          }
     return TRUE ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND           hWnd;
     unsigned       iMessage;
     WORD           wParam;
     LONG           lParam;
     {
     static FARPROC lpfnAboutDlgProc ;
     static HWND    hInstance ;
     HMENU          hMenu ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInstance = ((LPCREATESTRUCT) lParam)->hInstance ;

               lpfnAboutDlgProc = MakeProcInstance (AboutDlgProc, hInstance) ;

               hMenu = GetSystemMenu (hWnd, FALSE) ;
               ChangeMenu (hMenu, 0, NULL, 0, MF_APPEND) ;
               ChangeMenu (hMenu, 0, "A&bout About1...", IDM_ABOUT, MF_APPEND);
               break ;

          case WM_SYSCOMMAND:

               switch (wParam)
                    {
                    case IDM_ABOUT:
                         DialogBox (hInstance, "AboutBox", hWnd,
                                        lpfnAboutDlgProc) ;
                         break ;
                    }
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
     
          case WM_DESTROY :
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
